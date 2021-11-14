using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.DataTransferObjects
{
    /// <summary>
    /// ModifyProductRequestDataTransferObject
    /// </summary>
    public class ModifyProductRequestDataTransferObject
    {

        /// <summary>
        /// ProductId
        /// </summary>

        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// ProductName
        /// </summary>

        [Required, MaxLength(256)]
        public string ProductName { get; set; }

        /// <summary>
        /// ProductUnit
        /// </summary>
        [Required, MaxLength(25)]
        public string ProductUnit { get; set; }

        /// <summary>
        /// ProductPrice
        /// </summary>
        [Required]
        public double ProductPrice { get; set; }

        /// <summary>
        /// ProductQuantity
        /// </summary>
        [Required]
        public int ProductQuantity { get; set; }
    }
}
