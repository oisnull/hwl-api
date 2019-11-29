using HWL.CollectCore.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.CollectCore.Filter
{
    public interface IFilterHref
    {
        string Supply(string hrefContent);
        List<string> SupplyList(List<string> hrefContents);
    }
}
