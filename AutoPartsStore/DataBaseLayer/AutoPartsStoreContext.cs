using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

using AutoPartsStore.Model;
using AutoPartsStore.Model.Vehicle;
using Microsoft.EntityFrameworkCore;

namespace AutoPartsStore.DataBaseLayer
{
    public class AutoPartsStoreContext : DbContext
    {
        
        public AutoPartsStoreContext(DbContextOptions<AutoPartsStoreContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ProductOEMNumber> ProductOEMNumbers { get; set; }

        public DbSet<VehicleBrand> VehicleBrands { get; set; }
        public DbSet<VehicleModification> VehicleModifications { get; set; }
        public DbSet<VehicleEngine> VehicleEngines { get; set; }
        public DbSet<VehiclePart> VehicleParts { get; set; }
        public DbSet<ConcretVehiclePartOemNumber> ConcretVehiclePartOemNumbers { get; set; }


        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPart> OrderParts { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Category parentCategory = new Category { CategoryLevel = 0, Id = 1, Name = "Top category", ParentCategory = null };
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryLevel = 0, Id = 1, Name = "Top category", ParentCategory = null });

            

            
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(pc => pc.Nodes)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Product>().Ignore(p => p.Image).Ignore(p => p.Features);

            modelBuilder.Entity<Manufacturer>().Ignore(m => m.Image);


            modelBuilder.Entity<Review>().Ignore(r => r.Image);
            
            modelBuilder.Entity<Order>().Ignore(r => r.TotalPrice);

            modelBuilder.Entity<OrderPart>().Ignore(r => r.TotalPrice);
        }

    }
}
