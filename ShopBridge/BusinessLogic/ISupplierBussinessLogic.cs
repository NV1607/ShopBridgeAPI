using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.BusinessLogic
{
    /// <summary>
    /// ISupplierBussinessLogic
    /// </summary>
    public interface ISupplierBussinessLogic
    {
        /// <summary>
        /// GetSupplierDetails
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        Task<Supplier> GetSupplier(int supplierId);
    }
}
