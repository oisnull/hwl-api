using System;
using System.Collections.Generic;
using System.Linq;

namespace HWL.CollectCore.Filter
{
    public class DefaultFilterHref : IFilterHref
    {
        private Uri uri;
        private string[] uriStructs;

        public DefaultFilterHref(string entranceUrl)
        {
            this.uri = new Uri(entranceUrl);
            this.uriStructs = uri.Host.Split('.');
        }

        public string Supply(string hrefContent)
        {
            if (string.IsNullOrEmpty(hrefContent)) return null;
            if (string.IsNullOrWhiteSpace(hrefContent)) return null;

            if (hrefContent == "/") return null;
            if (hrefContent.StartsWith("#")) return null;
            if (hrefContent.ToLower().StartsWith("javascript")) return null;

            if (hrefContent.StartsWith("/"))
            {
                return string.Format("{0}://{1}{2}", this.uri.Scheme, this.uri.Host, hrefContent);
            }

            if (!hrefContent.ToLower().StartsWith("http"))
            {
                return string.Format("{0}://{1}/{2}", this.uri.Scheme, this.uri.Host, hrefContent);
            }

            if (IsCurrentHost(hrefContent))
            {
                return hrefContent;
            }
            //if (hrefContent.Contains(this.uri.Host))
            //{
            //    return hrefContent;
            //}

            return null;
        }

        private bool IsCurrentHost(string hrefContent)
        {
            Uri currentUri = null;
            try { currentUri = new Uri(hrefContent); }
            catch { }

            if (currentUri == null) return false;

            string[] hostStructs = currentUri.Host.Split('.');

            if (uriStructs != null && uriStructs.Length > 2 && hostStructs != null && hostStructs.Length > 2)
            {
                return hostStructs[hostStructs.Length - 1] == uriStructs[uriStructs.Length - 1] &&
                    hostStructs[hostStructs.Length - 2] == uriStructs[uriStructs.Length - 2];
            }
            return false;
        }

        public List<string> SupplyList(List<string> hrefContents)
        {
            if (hrefContents == null || hrefContents.Count <= 0) return hrefContents;

            List<string> newContents = new List<string>(hrefContents.Count);
            foreach (var item in hrefContents)
            {
                string newItem = Supply(item);
                if (newItem != null && !newContents.Contains(newItem))
                {
                    newContents.Add(newItem);
                }
            }
            return newContents;
        }
    }
}
