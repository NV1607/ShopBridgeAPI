using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShopBridge.Constants.ConstantsEnum;

namespace ShopBridge.Database
{
    /// <summary>
    /// ShopBridgeDatabaseInitializer
    /// </summary>
    public static class ShopBridgeDatabaseInitializer
    {
        /// <summary>
        /// Initialize Tables
        /// </summary>
        /// <param name="context"></param>
        public static void Initialize(this ShopBridgeContext context)
        {

            #region Supplier
            if (!context.Suppliers.Any())
            {
                context.Suppliers.AddRange(
                    new Supplier { Name = "RahulVentureLtd", PhoneNumber = 9765266633, Email = "RahulVenture@Gmail.com", Address = "BTM 2nd Stage" },
                    new Supplier { Name = "KrishnaEnterprise", PhoneNumber = 9765266633, Email = "Krishna@Gmail.com", Address = "#24 Laxshmi Nagar" });
                context.SaveChanges();
            }
            #endregion

            #region ProductCategory
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { CategoryName = ProductCategory.Electronics.ToString(), Description = "Electonics Product" },
                    new Category { CategoryName = ProductCategory.Wodden.ToString(), Description = "Wodden Products" },
                    new Category { CategoryName = ProductCategory.Food.ToString(), Description = "Food Products" },
                    new Category { CategoryName = ProductCategory.Plastic.ToString(), Description = "Platic Products" });
                context.SaveChanges();
            }
            #endregion
        }
    }
}
