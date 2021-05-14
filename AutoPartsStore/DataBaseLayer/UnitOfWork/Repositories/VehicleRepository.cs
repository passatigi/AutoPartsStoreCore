using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.DataBaseLayer.UnitOfWork;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Repositories
{
    public class VehicleRepository : IRepository<VehicleBrand>
    {
        AutoPartsStoreContext db;
        public VehicleRepository(AutoPartsStoreContext db)
        {
            this.db = db;
        }
        public void Add(VehicleBrand item)
        {
            db.Vehicles.Add(item);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VehicleBrand> GetAll()
        {
            return db.Vehicles.AsEnumerable();
        }

        public IEnumerable<VehicleBrand> GetAllWithCondition(object condition)
        {
            throw new NotImplementedException();
        }

        public VehicleBrand GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(VehicleBrand item)
        {
            throw new NotImplementedException();
        }
    }
}
