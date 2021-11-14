using Microsoft.EntityFrameworkCore;
using ShopBridge.Models;

namespace ShopBridge.Database
{
    /// <summary>
    /// Shop Bridge Context uses to pass db context options 
    /// like connection string and others to base class i.e DbContext
    /// which is useful to obtain database connection
    /// </summary>
    public class ShopBridgeContext:DbContext
    {
        /// <summary>
        /// ProductShipContext
        /// </summary>
        /// <param name="options"></param>
        public ShopBridgeContext(DbContextOptions<ShopBridgeContext> options)
       : base(options) { }

        /// <summary>
        /// On Model Creating method to specify
        /// relationship between the model class i.e tables
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
               .HasOne(fk => fk.Category)
               .WithMany(c => c.Products)
               .IsRequired(true);

            modelBuilder.Entity<Product>()
               .HasOne(fk => fk.Supplier)
               .WithMany(c => c.Products)
               .IsRequired(true);
            
        }

        /// <summary>
        /// Suppliers DbSet
        /// </summary>
        public DbSet<Supplier> Suppliers { get; set; }

        /// <summary>
        /// Products DbSet
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Categories DbSet
        /// </summary>
        public DbSet<Category> Categories { get; set; }
    }
}
