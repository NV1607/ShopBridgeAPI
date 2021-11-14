using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.DataTransferObjects
{
    /// <summary>
    /// ProductResponseDataTransferObject
    /// </summary>
    public class ProductResponseDataTransferObject
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ProductName
        /// </summary>

        public string ProductName { get; set; }

        /// <summary>
        /// ProductDescription
        /// </summary>
        public string ProductDescription { get; set; }

        /// <summary>
        /// ProductUnit
        /// </summary>
        public string ProductUnit { get; set; }

        /// <summary>
        /// ProductPrice
        /// </summary>
        public double ProductPrice { get; set; }

        /// <summary>
        /// ProductQuantity
        /// </summary>
        public int ProductQuantity { get; set; }

        /// <summary>
        /// ProductStatus
        /// </summary>
        public string ProductStatus { get; set; }

        /// <summary>
        /// SupplierId
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        /// SupplierName
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// CategoryId
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// CategoryName
        /// </summary>
        public string CategoryName { get; set; }
    }
}
