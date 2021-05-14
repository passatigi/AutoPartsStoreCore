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
        public DbSet<VendorCode> VendorCodes { get; set; }
        public DbSet<VendorCodeOEMNumbers> NumbersOEM { get; set; }

        public DbSet<VehicleBrand> Vehicles { get; set; }
        public DbSet<VehicleModification> VehicleModifications { get; set; }
        public DbSet<VehicleEngine> VehicleEngines { get; set; }
        public DbSet<VehiclePart> VehicleParts { get; set; }
        public DbSet<ConcretVehiclePartOemNumber> OEMNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryLevel = 0, Id = 1, Name = "Top category", ParentCategory = null });

            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(pc => pc.Nodes)
                .OnDelete(DeleteBehavior.ClientSetNull);
                
            //modelBuilder.Entity<Category>().Property(c => c.ParentCategory)
            //    .ValueGeneratedOnAddOrUpdate();
            ;
        }

    }
}
