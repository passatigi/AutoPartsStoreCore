using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Repositories
{
    public class VehicleModificationRepository : IRepository<VehicleModification, int>
    {
        AutoPartsStoreContext db;
        public VehicleModificationRepository(AutoPartsStoreContext db)
        {
            this.db = db;
        }
        public void Add(VehicleModification item)
        {
            db.VehicleModifications.Add(item);
        }

        public void Delete(int id)
        {
            db.VehicleModifications.Remove(GetById(id));
        }

        public IEnumerable<VehicleModification> GetAll()
        {
            return db.VehicleModifications.AsEnumerable();
        }

        public IEnumerable<VehicleModification> GetAllWithCondition(object condition)
        {
            if (condition is VehicleBrand)
            {
                VehicleBrand vehicle = condition as VehicleBrand;
                return db.VehicleModifications.Where(wm => wm.VehicleBrand.Brand.Equals(vehicle.Brand)).AsEnumerable();
            }
            else
                throw new Exception("Condition should be VehicleBrand");
            
        }

        public IEnumerable<VehicleModification> GetAs(VehicleModification item)
        {
            throw new NotImplementedException();
        }

        public VehicleModification GetById(int id)
        {
            return db.VehicleModifications.Where(vm => vm.Id == id).FirstOrDefault();
        }

        public void Update(VehicleModification item)
        {
            db.Update(item);
        }
    }
}
