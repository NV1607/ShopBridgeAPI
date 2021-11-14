using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBridge.Models
{
    /// <summary>
    /// Product Inventory
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// ProductName
        /// </summary>

        [Required, MaxLength(256)]
        public string ProductName { get; set; }

        /// <summary>
        /// ProductDescription
        /// </summary>
        [Required, MaxLength(256)]
        public string ProductDescription { get; set; }

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
        /// ProductStatus
        /// </summary>

        [Required, MaxLength(25)]
        public string ProductStatus { get; set; }

        /// <summary>
        /// SupplierId
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        /// CategoryId
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Supplier
        /// </summary>

        [ForeignKey(nameof(SupplierId))]
        public virtual Supplier Supplier { get; set; }

        /// <summary>
        /// Category
        /// </summary>

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }


    }
}
