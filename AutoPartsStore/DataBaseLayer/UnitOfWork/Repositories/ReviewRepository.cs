using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Repositories
{
    class ReviewRepository : IRepository<Review, long>
    {
        AutoPartsStoreContext db;

        public ReviewRepository(AutoPartsStoreContext db)
        {
            this.db = db;
        }

        public void Add(Review item)
        {
            db.Reviews.Add(item);
        }

        public void Delete(long id)
        {
            db.Reviews.Remove(db.Reviews.Where(r => r.Id == id).FirstOrDefault());
        }
        public IEnumerable<Review> GetAll()
        {
            return db.Reviews.AsEnumerable();
        }

        public IEnumerable<Review> GetAllWithCondition(object condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> GetAs(Review item)
        {
            return db.Reviews.Where(r => r.Product.Id == item.Product.Id);
        }

        public Review GetById(long id)
        {
            return db.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public void Update(Review item)
        {
            db.Reviews.Update(item);
        }
    }
}
