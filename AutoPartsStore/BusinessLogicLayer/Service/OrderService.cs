using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.BusinessLogicLayer.Service
{
    public class OrderService
    {
        IUnitOfWork unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddOrder(Order order)
        {
            //foreach(OrderPart orderPart in order.OrderParts)
            //{
            //    orderPart.Product.Id
            //}
            order.Status = Order.InProcessing;
            order.DateTime = DateTime.Now;
            unitOfWork.OrderRepository.Add(order);
            unitOfWork.Save();
        }
        public IEnumerable<Order> GetUserOrders(Customer customer)
        {
            Order order = new Order();
            order.Customer = customer;
            return unitOfWork.OrderRepository.GetAs(order);
        }

    }
}
