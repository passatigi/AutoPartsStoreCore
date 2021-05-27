using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPartsStore.DataBaseLayer.UnitOfWork.Repositories
{
    class OrderRepository : IRepository<Order, long>
    {

        AutoPartsStoreContext db;

        public OrderRepository(AutoPartsStoreContext db)
        {
            this.db = db;
        }
        public void Add(Order item)
        {
            db.Orders.Add(item);
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            return db.Orders.Include(o => o.Customer).Include(o => o.OrderParts).ThenInclude(p => p.Product).ThenInclude(p => p.Manufacturer);
        }

        public IEnumerable<Order> GetAllWithCondition(object condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAs(Order item)
        {
            return db.Orders.Where(o => o.Customer.Id == item.Customer.Id)
                .Include(o => o.OrderParts).ThenInclude(p => p.Product).ThenInclude(p => p.Manufacturer)
                .AsEnumerable();
        }

        public Order GetById(long id)
        {
            return db.Orders.Where(o => o.Id == id).FirstOrDefault();
        }

        public void Update(Order item)
        {
            db.Orders.Update(item);
        }
    }
}
