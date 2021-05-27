using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Windows;

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
        public IEnumerable<Order> GetAllOrders()
        {
            return unitOfWork.OrderRepository.GetAll();
        }
        public void UpdateOrderInfo(Order order)
        {
            unitOfWork.OrderRepository.Update(order);
            unitOfWork.Save();
        }

        public void SendEmailDone(Order order)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("kashaed.ivan@mail.ru");
                    mail.To.Add(order.Customer.Mail);
                    mail.Subject = "Магазин автозапчастей";
                    mail.Body = $"Заказ N{order.Id} выполнен";
                    mail.IsBodyHtml = true;
                    //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("ivankasaed41@gmail.com", "Kyrsach1919");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
