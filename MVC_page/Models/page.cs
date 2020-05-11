using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_page.Models
{
    public class Page<T>
    {
        public Page() { }
        public Page(int pageIndex, int pageSize, int dataCount, List<T> infoList)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.DataCount = dataCount;
            this.InfoList = infoList;
            this.PageCount = dataCount / pageSize;
            if (dataCount % pageSize != 0)
            {
                PageCount++;
            }
            this.InfoList = infoList;
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int DataCount { get; set; }
        public int PageCount { get; set; }
        public List<T> InfoList { get; set; }
       
    }
} 