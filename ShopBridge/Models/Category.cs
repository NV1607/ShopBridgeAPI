using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBridge.Models
{
    /// <summary>
    /// Product Category 
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// CategoryName
        /// </summary>

        [Required, MaxLength(256)]
        public string CategoryName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [Required, MaxLength(256)]
        public string Description { get; set; }

        /// <summary>
        /// Products Collection
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
    }
}
