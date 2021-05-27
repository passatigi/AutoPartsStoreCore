using AutoPartsStore.Command;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AutoPartsStore.ViewModel
{
    class AdminOrdersViewModel : OrdersViewModel
    {
        public override void UpdateOrders()
        {
                Orders.Clear();
                foreach (Order order in storeService.OrderService.GetAllOrders().OrderByDescending(o => o.DateTime))
                {
                    Orders.Add(order);
                }
        }
        private ObservableCollection<string> orderCommands;
        public ObservableCollection<string> OrderCommands
        {
            get
            {
                return orderCommands;
            }
            set
            {
                SetProperty(ref orderCommands, value);
            }
        }

        public AdminOrdersViewModel() : base()
        {
            OrderCommands = new ObservableCollection<string>();
            //OrderCommands.Add();
            //OrderCommands.Add();
            //OrderCommands.Add();
        }
        private void UpdateStatus(long id, string status)
        {
            Order order = Orders.Where(o => o.Id == id).FirstOrDefault();
            if (order != null)
            {
                order.Status = status;
                storeService.OrderService.UpdateOrderInfo(order);
            }
        }

        private RelayCommand confirmOrderCommand;
        public RelayCommand ConfirmOrderCommand
        {
            get
            {
                return confirmOrderCommand ?? (confirmOrderCommand = new RelayCommand(action =>
                {
                    if(action is long)
                    {
                        UpdateStatus((long)action, Order.Confirmed);
                    }
                }, func =>
                {
                    return true;
                }));
            }
        }
        private RelayCommand revokeOrderCommand;
        public RelayCommand RevokeOrderCommand
        {
            get
            {
                return revokeOrderCommand ?? (revokeOrderCommand = new RelayCommand(action =>
                {
                    if (action is long)
                    {
                        UpdateStatus((long)action, Order.Rejected);
                    }
                }, func =>
                {
                    return true;
                }));
            }
        }
        private RelayCommand notifyDoneOrderCommand;
        public RelayCommand NotifyDoneOrderCommand
        {
            get
            {
                return notifyDoneOrderCommand ?? (notifyDoneOrderCommand = new RelayCommand(action =>
                {
                    if (action is long)
                    {
                        UpdateStatus((long)action, Order.Completed);
                        storeService.OrderService.SendEmailDone(Orders.Where(o => o.Id == (long)action).FirstOrDefault());
                    }
                }, func =>
                {
                    return true;
                }));
            }
        }
        private RelayCommand notifyOrderPickedCommand;
        public RelayCommand NotifyOrderPickedCommand
        {
            get
            {
                return notifyOrderPickedCommand ?? (notifyOrderPickedCommand = new RelayCommand(action =>
                {
                    if (action is long)
                    {
                        UpdateStatus((long)action, Order.Picked);
                    }
                }, func =>
                {
                    return true;
                }));
            }
        }

    }
}
