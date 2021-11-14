using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.DataTransferObjects
{
    /// <summary>
    /// PagedParameters
    /// </summary>
    public class PagedParameters
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        private bool? orderByACS { get; set; } = true;
        public bool OrderByACS
        {
            get
            {
                return null == this.orderByACS || this.orderByACS.Value;
            }
            set
            {
                this.orderByACS = value;
            }
        }
        public PagedParameters()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PagedParameters(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
