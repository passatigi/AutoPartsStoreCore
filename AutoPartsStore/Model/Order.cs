using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AutoPartsStore.Model
{
    public class Order : BasicModel
    {
        public long Id { get; set; }

        public  Customer Customer { get; set; }
       
        public DateTime dateTime { get; set; }

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

        public ObservableCollection<OrderPart> OrderParts { get; set; }


        public Order()
        {
            OrderParts = new ObservableCollection<OrderPart>();
        }
    }
}
