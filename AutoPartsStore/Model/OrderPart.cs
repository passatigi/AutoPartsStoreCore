using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AutoPartsStore.Model
{
    public class OrderPart : BasicModel
    {
        public long Id { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }

        private short productCount;

        public short ProductCount
        {
            get
            {
                return productCount;
            }
            set
            {
                if(value > Product.Availability)
                {
                    productCount = Product.Availability;
                }
                else if(value < 0)
                {
                    productCount = 0;
                }
                else
                {
                    productCount = value;
                }
                NotifyPropertyChanged(nameof(ProductCount));
                TotalPrice = TotalPrice;
            }
        }
        [NotMapped]
        public Decimal TotalPrice
        {
            get
            {
                if (Product == null)
                {
                    return 0;
                }
                else
                {
                    return productCount * Product.Price;
                }
                
            }
            private set
            {
                NotifyPropertyChanged(nameof(TotalPrice));
            }
        }
    }
}
