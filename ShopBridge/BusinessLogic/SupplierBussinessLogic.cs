using ShopBridge.Models;
using ShopBridge.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.BusinessLogic
{
    /// <summary>
    /// Supplier BussinessLogic
    /// </summary>
    public class SupplierBussinessLogic : ISupplierBussinessLogic
    {
        private readonly IGenericRepository<Supplier> _supplierRepositoryBusinessLogic;

        /// <summary>
        /// Supplier BussinessLogic Constuctor
        /// </summary>
        /// <param name="supplierRepositoryBusinessLogic"></param>
        public SupplierBussinessLogic(IGenericRepository<Supplier> supplierRepositoryBusinessLogic)
        {
            _supplierRepositoryBusinessLogic = supplierRepositoryBusinessLogic;
        }

        /// <summary>
        /// GetSupplier
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public async Task<Supplier> GetSupplier(int supplierId)
        {
           return await _supplierRepositoryBusinessLogic.FirstOrDefault(x => x.Id == supplierId);
        }
    }
}
