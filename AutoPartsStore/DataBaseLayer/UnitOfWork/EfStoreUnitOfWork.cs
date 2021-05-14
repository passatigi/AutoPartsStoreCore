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

        private AutoPartsStoreContext db;
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
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            db = new AutoPartsStoreContext(options);
        }

        private VehicleRepository vehicleRepository;
        private VehicleModificationRepository vehicleModificationRepository;
        private VehicleEngineRepository vehicleEngineRepository;
        private CategoryRepository categoryRepository;

        #region Properties

        public IRepository<VehicleBrand> VehicleRepository
        {
            get
            {
                if (vehicleRepository == null)
                    vehicleRepository = new VehicleRepository(db);
                return vehicleRepository;
            }
        }
        public IRepository<VehicleModification> VehicleModificationRepository
        {
            get
            {
                if (vehicleModificationRepository == null)
                    vehicleModificationRepository = new VehicleModificationRepository(db);
                return vehicleModificationRepository;
            }
        }
        public IRepository<VehicleEngine> VehicleEngineRepository
        {
            get
            {
                if (vehicleEngineRepository == null)
                    vehicleEngineRepository = new VehicleEngineRepository(db);
                return vehicleEngineRepository;
            }
        }
        public IRepository<Category> CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(db);
                return categoryRepository;
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
