using AutoPartsStore.DataBaseLayer;
using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.DataBaseLayer.UnitOfWork.Repositories;
using AutoPartsStore.Model;
using AutoPartsStore.Model.Vehicle;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.DataBaseConnector
{
    public class EfStoreUnitOfWork : IUnitOfWork
    {
        #region singleton
        
        private static EfStoreUnitOfWork storeUnitOfWork;
        private static object syncRoot = new Object(); public static EfStoreUnitOfWork GetStoreUnitOfWork()
        {
            if (storeUnitOfWork == null)
            {
                lock (syncRoot)
                {
                    if (storeUnitOfWork == null)
                        storeUnitOfWork = new EfStoreUnitOfWork();
                }
            }
            return storeUnitOfWork;
        }
        #endregion
        // private
        private readonly StreamWriter logStream = new StreamWriter("mylog.txt", true);
        public AutoPartsStoreContext db;
        protected EfStoreUnitOfWork()
        {

            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            //builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AutoPartsStoreContext>();
            optionsBuilder.LogTo(logStream.WriteLine);
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            db = new AutoPartsStoreContext(options);
        }

        private VehicleRepository vehicleRepository;
        private VehicleModificationRepository vehicleModificationRepository;
        private VehicleEngineRepository vehicleEngineRepository;
        private CategoryRepository categoryRepository;
        private ManufacturerRepository manufacturerRepository;
        private ProductRepository productRepository; 
        private VehiclePartRepository vehiclePartRepository;
        private ProductOEMNumberRepository productOEMNumberRepository;
        private UserRepository userRepository;
        private OrderRepository orderRepository;
        private ReviewRepository reviewRepository;
        private AdminRepository adminRepository;

        #region Properties

        public IRepository<Administrator, int> AdminRepository
        {
            get
            {
                if (adminRepository == null)
                    adminRepository = new AdminRepository(db);
                return adminRepository;
            }
        }

        public IRepository<Customer, int> UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepository<Order, long> OrderRepository
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }
        public IRepository<Review, long> ReviewRepository
        {
            get
            {
                if (reviewRepository == null)
                    reviewRepository = new ReviewRepository(db);
                return reviewRepository;
            }
        }


        public IRepository<VehicleBrand, int> VehicleRepository
        {
            get
            {
                if (vehicleRepository == null)
                    vehicleRepository = new VehicleRepository(db);
                return vehicleRepository;
            }
        }

        public IRepository<VehicleModification, int> VehicleModificationRepository
        {
            get
            {
                if (vehicleModificationRepository == null)
                    vehicleModificationRepository = new VehicleModificationRepository(db);
                return vehicleModificationRepository;
            }
        }
        public IRepository<VehicleEngine, int> VehicleEngineRepository
        {
            get
            {
                if (vehicleEngineRepository == null)
                    vehicleEngineRepository = new VehicleEngineRepository(db);
                return vehicleEngineRepository;
            }
        }
        public IRepository<Category, int> CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(db);
                return categoryRepository;
            }
        }
        public IRepository<Manufacturer, int> ManufacturerRepository
        {
            get
            {
                if (manufacturerRepository == null)
                    manufacturerRepository = new ManufacturerRepository(db);
                return manufacturerRepository;
            }
        }
        public IRepository<Product, long> ProductRepository
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(db);
                return productRepository;
            }
        }
        public IRepository<VehiclePart, int> VehiclePartRepository
        {
            get
            {
                if (vehiclePartRepository == null)
                    vehiclePartRepository = new VehiclePartRepository(db);
                return vehiclePartRepository;
            }
        }
         public IRepository<ProductOEMNumber, int> ProductOEMNumberRepository
        {
            get
            {
                if (productOEMNumberRepository == null)
                    productOEMNumberRepository = new ProductOEMNumberRepository(db);
                return productOEMNumberRepository;
            }
        }

        #endregion

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
