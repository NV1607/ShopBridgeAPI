using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Models
{
    /// <summary>
    /// Supplier 
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required, MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// PhoneNumber
        /// </summary>
        [Required, MaxLength(10)]
        public long PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }


        /// <summary>
        /// Address
        /// </summary>
        [Required, MaxLength(256)]
        public string Address { get; set; }

        /// <summary>
        /// Products
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
    }
}
