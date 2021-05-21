using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Repositories
{
    class ManufacturerRepository : IRepository<Manufacturer>
    {
        AutoPartsStoreContext db;

        public ManufacturerRepository(AutoPartsStoreContext db)
        {
            this.db = db;
        }

        public void Add(Manufacturer item)
        {
            db.Manufacturers.Add(item);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Manufacturer> GetAll()
        {
            return db.Manufacturers.AsEnumerable();
        }

        public IEnumerable<Manufacturer> GetAllWithCondition(object condition)
        {
            throw new NotImplementedException();
        }

        public Manufacturer GetAs(Manufacturer item)
        {
            throw new NotImplementedException();
        }

        public Manufacturer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Manufacturer item)
        {
            throw new NotImplementedException();
        }
    }
}
