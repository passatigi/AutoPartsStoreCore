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

        public DbSet<VehicleBrand> Vehicles { get; set; }
        public DbSet<VehicleModification> VehicleModifications { get; set; }
        public DbSet<VehicleEngine> VehicleEngines { get; set; }
        public DbSet<VehiclePart> VehicleParts { get; set; }
        public DbSet<ConcretVehiclePartOemNumber> ConcretVehiclePartOemNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Category parentCategory = new Category { CategoryLevel = 0, Id = 1, Name = "Top category", ParentCategory = null };
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryLevel = 0, Id = 1, Name = "Top category", ParentCategory = null });

            //modelBuilder.Entity<Category>().HasData(
            //    parentCategory,
            //    new Category { CategoryLevel = 1, Id = 2, Name = "Kolbasa", ParentCategory = parentCategory },
            //    new Category { CategoryLevel = 1, Id = 3, Name = "Sir", ParentCategory = parentCategory },
            //    new Category { CategoryLevel = 1, Id = 4, Name = "Ananas", ParentCategory = parentCategory },
            //    new Category { CategoryLevel = 1, Id = 5, Name = "Jele", ParentCategory = parentCategory },
            //    new Category { CategoryLevel = 1, Id = 6, Name = "Sosiski", ParentCategory = parentCategory },
            //    new Category { CategoryLevel = 1, Id = 7, Name = "Sardelki", ParentCategory = parentCategory }
            //    );

            //VehicleBrand[] vehicleBrands = { 
            //    new VehicleBrand { Brand = "ford", Id = 1 },
            //    new VehicleBrand { Brand = "bmw", Id = 2 },
            //    new VehicleBrand { Brand = "volvo", Id = 3 } };
            //modelBuilder.Entity<VehicleBrand>().HasData(
            //    vehicleBrands
            //    );
            //VehicleModification[] vehicleModifications =
            //{
            //    new VehicleModification{ Id = 1, Model = "Sierra", ModelCode = "GBS", ReleaseStart = "1980", ReleaseEnd = "1990", VehicleBrand = vehicleBrands[0]  },
            //    new VehicleModification{ Id = 2, Model = "Sierra", ModelCode = "GBS", ReleaseStart = "1990", ReleaseEnd = "2000", VehicleBrand = vehicleBrands[0]  },
            //    new VehicleModification{ Id = 3, Model = "5 Series", ModelCode = "GG", ReleaseStart = "1980", ReleaseEnd = "1990", VehicleBrand = vehicleBrands[1]  },
            //    new VehicleModification{ Id = 4, Model = "440", ModelCode = "BB", ReleaseStart = "1980", ReleaseEnd = "1990", VehicleBrand = vehicleBrands[2]  }
            //};
            //modelBuilder.Entity<VehicleModification>().HasData(
            //   vehicleModifications
            //   );
            //VehicleEngine[] vehicleEngines =
            //{
            //    new VehicleEngine{Id = 1, Volume = 2, Type = 'b', ModelCode = "n4a", Modification = "i", Power = 1000, ReleaseStart = "2001", ReleaseEnd = "н.в.", VehicleModification = vehicleModifications[0]},
            //    new VehicleEngine{Id = 2, Volume = 2, Type = 'b', ModelCode = "n4b", Modification = "i", Power = 1150, ReleaseStart = "2001", ReleaseEnd = "н.в.", VehicleModification = vehicleModifications[1]},
            //    new VehicleEngine{Id = 3, Volume = 2, Type = 'b', ModelCode = "n5", Modification = "i", Power = 1000, ReleaseStart = "2001", ReleaseEnd = "н.в.", VehicleModification = vehicleModifications[0]},
            //    new VehicleEngine{Id = 4, Volume = 2, Type = 'b', ModelCode = "n4a", Modification = "i", Power = 1000, ReleaseStart = "2001", ReleaseEnd = "н.в.", VehicleModification = vehicleModifications[2]},
            //    new VehicleEngine{Id = 5, Volume = 2, Type = 'b', ModelCode = "n4b", Modification = "i", Power = 1150, ReleaseStart = "2001", ReleaseEnd = "н.в.", VehicleModification = vehicleModifications[3]},
            //    new VehicleEngine{Id = 6, Volume = 2, Type = 'b', ModelCode = "n5", Modification = "i", Power = 1000, ReleaseStart = "2001", ReleaseEnd = "н.в.", VehicleModification = vehicleModifications[1]}
            //};

            //modelBuilder.Entity<VehicleEngine>().HasData(
            //   vehicleEngines
            //   );

            
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(pc => pc.Nodes)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Product>().Ignore(p => p.Image).Ignore(p => p.Features);

            modelBuilder.Entity<Manufacturer>().Ignore(m => m.Image);
            //modelBuilder.Entity<Category>().Property(c => c.ParentCategory)
            //    .ValueGeneratedOnAddOrUpdate();

        }

    }
}
