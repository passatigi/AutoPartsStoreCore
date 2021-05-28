
using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Repositories
{
    public class VehicleEngineRepository : IRepository<VehicleEngine, long>
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

        public void Delete(long id)
        {
            db.VehicleEngines.Remove(GetById(id));
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

        public IEnumerable<VehicleEngine> GetAs(VehicleEngine item)
        {
            throw new NotImplementedException();
        }

        public VehicleEngine GetById(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(VehicleEngine item)
        {
            db.VehicleEngines.Update(item);
        }
    }
}
