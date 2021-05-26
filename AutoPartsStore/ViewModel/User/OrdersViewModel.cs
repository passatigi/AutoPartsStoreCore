﻿using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AutoPartsStore.ViewModel
{
    public class OrdersViewModel : BaseViewModel
    {
        private ObservableCollection<Order> orders;
        public ObservableCollection<Order> Orders
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
        public void UpdateOrders()
        {
            if(userConfiguration.Customer != null)
            {
                Orders.Clear();
                foreach(Order order in storeService.OrderService.GetUserOrders(userConfiguration.Customer).OrderByDescending(o => o.DateTime))
                {
                    Orders.Add(order);
                }
            }
        }


        private RelayCommand updateOrdersCommand;
        public RelayCommand UpdateOrdersCommand
        {
            get
            {
                return updateOrdersCommand ?? (updateOrdersCommand = new RelayCommand(action =>
                {
                    UpdateOrders();
                }, func =>
                {
                    return true;
                }));
            }
        }

        MainViewModel mainViewModel;

        IStoreService storeService;

        UserConfiguration userConfiguration;


        public OrdersViewModel()
        {
            storeService = StoreService.GetStoreService();
            mainViewModel = MainViewModel.GetMainViewModel();
            mainViewModel.OrdersViewModel = this;
            userConfiguration = UserConfiguration.GetUserConfiguration();

            Orders = new ObservableCollection<Order>();

            UpdateOrders();



        }
    }
}
