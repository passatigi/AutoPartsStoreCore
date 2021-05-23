using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model.Vehicle;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Repositories
{
    class VehiclePartRepository : IRepository<VehiclePart, int>
    {
        AutoPartsStoreContext db;

        public VehiclePartRepository(AutoPartsStoreContext db)
        {
            this.db = db;
        }

        public void Add(VehiclePart item)
        {
            db.VehicleParts.Add(item);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VehiclePart> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VehiclePart> GetAllWithCondition(object condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VehiclePart> GetAs(VehiclePart item)
        {
            return db.VehicleParts
                .Where(p => p.Category.Id == item.Category.Id && p.VehicleEngine.Id == item.VehicleEngine.Id)
                .Include(p => p.ConcretVehiclePartOemNumbers);
        }

        public VehiclePart GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(VehiclePart item)
        {
            throw new NotImplementedException();
        }
    }
}
