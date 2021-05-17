using AutoPartsStore.Model;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<VehicleBrand> VehicleRepository { get; }
        IRepository<VehicleModification> VehicleModificationRepository { get; }
        IRepository<VehicleEngine> VehicleEngineRepository { get; }
        IRepository<Category> CategoryRepository { get; }

        IRepository<Manufacturer> ManufacturerRepository { get; }
        IRepository<Product> ProductRepository { get; }
        
        void Save();
    }
}
