using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWL.ShareConfig
{
    public class IMConfigManager : ShareConfiguration
    {
        public static int IMPort
        {
            get
            {
                return Convert.ToInt32(IMSettings["IMPort"]);
            }
        }

        public static string IMHost
        {
            get
            {
                return IMSettings["IMHost"];
            }
        }
    }
}
