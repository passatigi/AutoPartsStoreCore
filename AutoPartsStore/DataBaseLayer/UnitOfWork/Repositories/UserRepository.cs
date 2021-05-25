using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Repositories
{
    class UserRepository : IRepository<Customer, int>
    {
        AutoPartsStoreContext db;

        public UserRepository(AutoPartsStoreContext db)
        {
            this.db = db;
        }
        public void Add(Customer item)
        {
            db.Customers.Add(item);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAllWithCondition(object condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAs(Customer item)
        {
            return db.Customers.Where(c => c.Mail.Equals(item.Mail));
        }

        public Customer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer item)
        {
            db.Customers.Update(item);
        }
    }
}
