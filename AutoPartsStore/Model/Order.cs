using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoPartsStore.Model
{
    public class Order : BasicModel
    {

        public const string InProcessing = "В обработке";
        public const string Confirmed = "Подтвержден";
        public const string Completed = "Выполнен";
        public const string Rejected = "Отклонен";

        public long Id { get; set; }

        public  Customer Customer { get; set; }
       
        public DateTime DateTime { get; set; }

        private string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                SetProperty(ref status, value);
            }
        }

        private Decimal totalPrice;
        [NotMapped]
        public Decimal TotalPrice
        {
            get
            {
                _UpdateTotalPriceNoNotify();
                return totalPrice;
            }
        }

        private ObservableCollection<OrderPart> orderParts;
        public ObservableCollection<OrderPart> OrderParts
        {
            get
            {
                return orderParts;
            }
            set
            {
                SetProperty(ref orderParts, value);
            }
        }


        public Order()
        {
            OrderParts = new ObservableCollection<OrderPart>();
        }

        public void AddOrderPart(OrderPart orderPart)
        {
            OrderPart tempOrderPart = OrderParts.Where(p => p.Product.Id == orderPart.Product.Id).FirstOrDefault();
            if(tempOrderPart == null)
            {
                OrderParts.Add(orderPart);
            }
            else{
                try
                {
                    tempOrderPart.ProductCount += orderPart.ProductCount;
                    if (tempOrderPart.ProductCount > tempOrderPart.Product.Availability)
                    {
                        throw new Exception("nedostatochno");
                    }
                }
                catch (Exception e)
                {
                    tempOrderPart.ProductCount = tempOrderPart.Product.Availability;
                }
            }
            UpdateTotalPrice();
            NotifyPropertyChanged(nameof(TotalPrice));
        }
        public void RemoveOrderPart(long productId)
        {
            OrderPart tempOrderPart = OrderParts.Where(p => p.Product.Id == productId).FirstOrDefault();
            if (tempOrderPart != null)
            {
                OrderParts.Remove(tempOrderPart);
            }
            else
            {
                throw new Exception("che"); 
            }
            UpdateTotalPrice();     
        }
        private void _UpdateTotalPriceNoNotify()
        {
            totalPrice = 0;
            foreach (OrderPart op in orderParts)
            {
                totalPrice += op.Product.Price * op.ProductCount;
            }
        }
        public void UpdateTotalPrice()
        {
            _UpdateTotalPriceNoNotify();
            NotifyPropertyChanged(nameof(TotalPrice));
        }
    }
}
