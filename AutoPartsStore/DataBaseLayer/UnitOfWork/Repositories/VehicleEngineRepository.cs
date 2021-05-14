
using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Repositories
{
    public class VehicleEngineRepository : IRepository<VehicleEngine>
    {
        AutoPartsStoreContext db;
        public VehicleEngineRepository(AutoPartsStoreContext db)
        {
            this.db = db;
        }

        public void Add(VehicleEngine item)
        {
            db.VehicleEngines.Add(item);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VehicleEngine> GetAll()
        {
            return db.VehicleEngines.AsEnumerable();

        }

        public IEnumerable<VehicleEngine> GetAllWithCondition(object condition)
        {
            if (condition is VehicleModification)
            {
                VehicleModification vehicleModification = condition as VehicleModification;
                return db.VehicleEngines.Where(we => we.VehicleModification.Model.Equals(vehicleModification.Model)).AsEnumerable();
            }
            else
                throw new Exception("Condition should be VehicleModification");
        }

        public VehicleEngine GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(VehicleEngine item)
        {
            throw new NotImplementedException();
        }
    }
}
