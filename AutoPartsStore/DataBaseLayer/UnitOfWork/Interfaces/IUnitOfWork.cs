using AutoPartsStore.Model;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<VehicleBrand, int> VehicleRepository { get; }
        IRepository<VehicleModification, int> VehicleModificationRepository { get; }
        IRepository<VehicleEngine, int> VehicleEngineRepository { get; }
        IRepository<VehiclePart, int> VehiclePartRepository { get; }
        IRepository<Category, int> CategoryRepository { get; }

        IRepository<Manufacturer, int> ManufacturerRepository { get; }
        IRepository<Product, long> ProductRepository { get; }
        IRepository<ProductOEMNumber, int> ProductOEMNumberRepository { get; }

        IRepository<Customer, int> UserRepository { get; }
        IRepository<Order, long> OrderRepository { get; }
        
        void Save();
    }
}
