using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Repositories
{
    class ProductOEMNumberRepository : IRepository<ProductOEMNumber, int>
    {
        AutoPartsStoreContext db;

        public ProductOEMNumberRepository(AutoPartsStoreContext db)
        {
            this.db = db;
        }

        public void Add(ProductOEMNumber item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductOEMNumber> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductOEMNumber> GetAllWithCondition(object condition)
        {
            throw new NotImplementedException();
        }


        public ProductOEMNumber GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductOEMNumber item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductOEMNumber> GetAs(ProductOEMNumber item)
        {
            return db.ProductOEMNumbers.Where(o => o.OEM.Equals(item.OEM) && o.VehicleBrand.Id == item.VehicleBrand.Id)
                .Include(o => o.Product).ThenInclude(p => p.Manufacturer);
              
        }
    }
}
