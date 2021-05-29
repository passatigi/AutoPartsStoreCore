using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Repositories
{
    class ProductRepository : IRepository<Product, long>
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

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products.Include(p => p.Manufacturer).Include(p => p.ProductOEMNumbers);
        }

        public IEnumerable<Product> GetAllWithCondition(object condition)
        {
            throw new NotImplementedException();
        }


        public Product GetById(long id)
        {
            return db.Products.Where(p=> p.Id == id).Include(p => p.Manufacturer).Include(p => p.ProductOEMNumbers).FirstOrDefault();

        }

        public void Update(Product item)
        {
            db.Products.Update(item);
            db.SaveChanges();
            db.ProductOEMNumbers.RemoveRange(db.ProductOEMNumbers.Where(n => n.Product == null));
        }

        public IEnumerable<Product> GetAs(Product item)
        {
            throw new NotImplementedException();
        }
    }
}
