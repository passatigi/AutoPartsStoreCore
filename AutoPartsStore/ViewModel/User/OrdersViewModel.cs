using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AutoPartsStore.ViewModel.User
{
    class OrdersViewModel : BaseViewModel
    {
        private ObservableCollection<Order> orders;
        public ObservableCollection<Order> Order
        {
            get
            {
                return orders;
            }
            set
            {
                SetProperty(ref orders, value);
            }
        }

    }
}
