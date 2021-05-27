using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Repositories
{
    class AdminRepository : IRepository<Administrator, int>
    {
        AutoPartsStoreContext db;

        public AdminRepository(AutoPartsStoreContext db)
        {
            this.db = db;
        }

        public void Add(Administrator item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Administrator> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Administrator> GetAllWithCondition(object condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Administrator> GetAs(Administrator item)
        {
            return db.Administrator.Where(a => a.Customer.Id == item.Customer.Id).Include(a => a.Customer);
        }

        public Administrator GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Administrator item)
        {
            throw new NotImplementedException();
        }
    }
}
