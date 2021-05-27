using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPartsStore.ViewModel
{
    public class ShoppingCartViewModel : BaseViewModel
    {
        public int ProductsCount
        {
            get
            {
                if(ShoppingCart != null)
                {
                    return ShoppingCart.OrderParts.Count();
                }
                return 0;
            }
            set
            {
                NotifyPropertyChanged(nameof(ProductsCount));
            }
        }
        private Order shoppingCart;
        public Order ShoppingCart
        {
            get
            {
                return shoppingCart;
            }
            set
            {
                SetProperty(ref shoppingCart, value);
            }
        }

        MainViewModel mainViewModel;

        IStoreService storeService;

        public ShoppingCartViewModel()
        {
            mainViewModel = MainViewModel.GetMainViewModel();
            mainViewModel.ShoppingCartViewModel = this;

            storeService = StoreService.GetStoreService();

            ShoppingCart = UserConfiguration.GetUserConfiguration().ShoppingCart;

        }

        public void UpdateShoppingCart()
        {
            ShoppingCart = UserConfiguration.GetUserConfiguration().ShoppingCart;
            NotifyPropertyChanged(nameof(ProductsCount));
        }

        private RelayCommand placeOrderCommand;
        public RelayCommand PlaceOrderCommand
        {
            get
            {
                return placeOrderCommand ?? (placeOrderCommand = new RelayCommand(action =>
                {
                    mainViewModel.MakeNewOrder();
                    UpdateShoppingCart();

                }, func =>
                {
                    return true;
                }));
            }
        }
        private RelayCommand deleteOrderPartCommand;
        public RelayCommand DeleteOrderPartCommand
        {
            get
            {
                return deleteOrderPartCommand ?? (deleteOrderPartCommand = new RelayCommand(action =>
                {

                    if(action is long)
                    {
                        long id = (long)action;
                        shoppingCart.RemoveOrderPart(id);
                        NotifyPropertyChanged(nameof(ProductsCount));
                    }
                }, func =>
                {
                    return true;
                }));
            }
        }

        private RelayCommand addProductCountCommand;
        public RelayCommand AddProductCountCommand
        {
            get
            {
                return addProductCountCommand ?? (addProductCountCommand = new RelayCommand(action =>
                {
                    if (action is long)
                    {
                        long id = (long)action;
                        OrderPart orderPart = shoppingCart.OrderParts.Where(p => p.Product.Id == id).FirstOrDefault();
                        if(orderPart!= null)
                        {
                            orderPart.ProductCount++;
                            ShoppingCart.UpdateTotalPrice();
                        }
                    }
                }, func =>
                {
                    return true;
                }));
            }

        }

        private RelayCommand reduceProductCountCommand;
        public RelayCommand ReduceProductCountCommand
        {
            get
            {
                return reduceProductCountCommand ?? (reduceProductCountCommand = new RelayCommand(action =>
                {
                    if (action is long)
                    {
                        long id = (long)action;
                        OrderPart orderPart = shoppingCart.OrderParts.Where(p => p.Product.Id == id).FirstOrDefault();
                        if (orderPart != null)
                        {
                            orderPart.ProductCount--;
                            ShoppingCart.UpdateTotalPrice();
                        }
                    }
                }, func =>
                {
                    return true;
                }));
            }

        }


    }
}
