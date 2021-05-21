using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Repositories
{
    class ProductRepository : IRepository<Product>
    {

        AutoPartsStoreContext db;

        public ProductRepository(AutoPartsStoreContext db)
        {
            this.db = db;
        }
        public void Add(Product item)
        {
            //foreach (ProductOEMNumber productOEMNumber in item.VendorCode.VendorCodeOEMNumbers)
            //{
            //    db.VendorCodeOEMNumbers.Add(productOEMNumber);
            //}
            //db.VendorCodes.Add(item.VendorCode);
            db.Products.Add(item);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products.Include(p => p.Manufacturer);
        }

        public IEnumerable<Product> GetAllWithCondition(object condition)
        {
            throw new NotImplementedException();
        }


        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAs(Product item)
        {
            throw new NotImplementedException();
        }
    }
}
