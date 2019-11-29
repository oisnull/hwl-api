using HWL.CollectCore.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.CollectCore.Filter
{
    public interface IFilterText
    {
        string GetFirst(string content, FilterModel filter);

        List<string> GetList(List<string> contents, FilterModel filter);
    }
}
