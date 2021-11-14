using System.ComponentModel.DataAnnotations;

namespace ShopBridge.DataTransferObjects
{
    /// <summary>
    /// AddProductRequestDataTransferObject
    /// </summary>
    public class AddProductRequestDataTransferObject
    {
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

        /// <summary>
        /// ProductQuantity
        /// </summary>
        [Required]
        public string ProductDescription { get; set; }

        /// <summary>
        /// SupplierId
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        /// CategoryId
        /// </summary>
        public int CategoryId { get; set; }
    }
}
