using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Article.Models
{
    public class FilterModel
    {
        public FilterModel()
        {
            Keyword = "";
            PageSize = 10;
            PageIndex = 1;
        }

        public string Keyword { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}