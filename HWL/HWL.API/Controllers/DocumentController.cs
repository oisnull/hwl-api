using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HWL.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        string[] dataType = { "boolean", "string", "int32", "int64", "decimal", "doublue", "float", "datetime" };

        readonly static List<Assembly> serviceAssembly = null;

        static DocumentController()
        {
            serviceAssembly = new List<Assembly>();
            serviceAssembly.Add(Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "HWL.Service.dll"));
            serviceAssembly.Add(Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "HWL.Entity.dll"));
            serviceAssembly.Add(Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "GMSF.dll"));
        }

        private static Type GetTypeFromAssembly(string fullName)
        {
            foreach (var item in serviceAssembly)
            {
                Type type = item.GetType(fullName);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }

        private string getPropertyStr(PropertyInfo pinfo)
        {
            if (pinfo == null) return null;

            string currType = pinfo.PropertyType.Name.ToLower();
            if (currType == "list`1")
            {
                Type[] ts = pinfo.PropertyType.GenericTypeArguments;
                foreach (var tsItem in ts)
                {
                    Type type = GetTypeFromAssembly(tsItem.FullName);
                    if (type == null) continue;
                    object obj = Activator.CreateInstance(type);

                    return string.Format("[{0}]", JsonConvert.SerializeObject(obj));
                }
            }
            else if (!dataType.Contains(currType))
            {
                Type selfType = GetTypeFromAssembly(pinfo.PropertyType.FullName);
                if (selfType == null)
                {
                    if (pinfo.PropertyType.GenericTypeArguments.Length > 0)
                    {
                        selfType = GetTypeFromAssembly(pinfo.PropertyType.GenericTypeArguments[0].FullName);
                    }
                    if (selfType == null)
                        return null;
                }
                Object self = Activator.CreateInstance(selfType);
                return JsonConvert.SerializeObject(self);
            }
            return null;
        }

        // GET: Doc
        public ActionResult Index(String key)
        {
            Type rootType = typeof(DefaultController);

            MethodInfo[] methods = rootType.GetMethods();

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.Append("<meta http-equiv=\"content-type\" content=\"text/html;charset=utf-8\" />");
            sb.Append("<title>Web api list</title>");
            sb.Append("<script src=\"/js/api_test.js\"></script>");
            sb.Append("<body>");
            int idx = 1;
            foreach (MethodInfo method in methods)
            {
                if (method.DeclaringType.Name != rootType.Name) continue;

                string requestStr = string.Empty;
                var paramArray = method.GetParameters();
                foreach (var paramItem in paramArray)
                {
                    Type[] ts = paramItem.ParameterType.GenericTypeArguments;
                    foreach (var tsItem in ts)
                    {
                        Type type = GetTypeFromAssembly(tsItem.FullName);
                        if (type == null) continue;

                        object obj = Activator.CreateInstance(type);
                        requestStr += JsonConvert.SerializeObject(obj);

                        foreach (var proItem in type.GetProperties())
                        {
                            string proJson = this.getPropertyStr(proItem);
                            if (!string.IsNullOrEmpty(proJson))
                            {
                                requestStr = requestStr.Replace(string.Format("\"{0}\":null", proItem.Name), string.Format("\"{0}\":{1}", proItem.Name, proJson));
                            }
                        }
                    }
                }

                string returnStr = string.Empty;
                Type[] rtp = method.ReturnParameter.ParameterType.GenericTypeArguments;
                foreach (var tsItem in rtp)
                {
                    Type type = GetTypeFromAssembly(tsItem.FullName);
                    if (type == null) continue;

                    object obj = Activator.CreateInstance(type);
                    returnStr += JsonConvert.SerializeObject(obj);

                    foreach (var proItem in type.GetProperties())
                    {
                        string proJson = this.getPropertyStr(proItem);
                        if (!string.IsNullOrEmpty(proJson))
                        {
                            //returnStr = returnStr.Replace("\"" + proItem.Name + "\":null", proJson);
                            returnStr = returnStr.Replace(string.Format("\"{0}\":null", proItem.Name), string.Format("\"{0}\":{1}", proItem.Name, proJson));
                        }
                    }
                }

                object[] obs = method.GetCustomAttributes(typeof(DescriptionAttribute), false);
                string desc = "";
                foreach (DescriptionAttribute record in obs)
                {
                    desc += record.Description;
                }

                if (!string.IsNullOrEmpty(key))
                {
                    if (desc.Contains(key))
                    {
                    }
                    else
                    {
                        continue;
                    }
                }

                sb.AppendFormat("<h1 style='display:inline'>{4}</h1> {0} - {1} <a href='/test.html' onclick=addReqV2(\'{0}\',\'{2}\') target='_blank'>测试</a><br /> request:{2} <br /> <div style='width:100%;word-wrap:break-word;'>response:{3}</div> <br /><br /><br />", method.Name, desc, requestStr, returnStr, idx);
                idx++;
            }
            sb.Append("</body>");
            sb.Append("</html>");

            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = sb.ToString()
            };
        }
    }
}