using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.DataTransferObjects
{
    public class PagedModel<T> where T :class
    {

        private int _pageSize;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize =  value;
                TotalPages = GetTotalItems();
            }
        }
        public int CurrentPage { get; set; }
        private int _totalItems;
        public int TotalItems
        {
            get => _totalItems;
            set
            {
                _totalItems = value;
                TotalPages = GetTotalItems();
            }
        }
        public int TotalPages { get; private set; }
        public IEnumerable<T> Items { get; set; } = new List<T>();

        private int GetTotalItems() => default == _totalItems || default == _pageSize ? 0 : (int)Math.Ceiling(_totalItems / (double)PageSize);
    }
}
