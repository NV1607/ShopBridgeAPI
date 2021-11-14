using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.BusinessLogic
{
    /// <summary>
    /// ICategoryBusinessLogic
    /// </summary>
    public interface ICategoryBusinessLogic
    {
        /// <summary>
        /// GetSupplierDetails
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<Category> GetProductCategory(int categoryId);
    }
}
