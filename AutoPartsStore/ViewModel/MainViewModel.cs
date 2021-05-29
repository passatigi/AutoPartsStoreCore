using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoPartsStore.ViewModel
{
    public class MainViewModel
    {
        private static MainViewModel mainViewModel;
        private static object syncRoot = new Object();

        public HeaderViewModel HeaderViewModel { get; set; }

        public CategoriesViewModel CategoriesViewModel { get; set; }
        public ProductsViewModel ProductsViewModel { get; set; }
        public ProductViewModel ProductViewModel { get; set; }
        public AddEntityPanelViewModel AddEntityPanelViewModel { get; set; }


        public NewCarViewModel NewCarViewModel { get; set; }

        public ChooseCarViewModel ChooseCarViewModel { get; set; }

        public AddOemToCarCategoryViewModel AddOemToCarCategoryViewModel { get; set; }
        public UserViewModel UserViewModel { get; set; }

        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }
        public OrdersViewModel OrdersViewModel { get; set; }
        public EditCategoryViewModel EditCategoryViewModel { get; set; }

        private IStoreService storeService;
        public IStoreService StoreService
        {
            get
            {
                if (storeService == null)
                {
                    storeService = BusinessLogicLayer.Service.StoreService.GetStoreService();
                }
                return storeService;
            }
        }
        public MainViewModel()
        {
            UserConfiguration =  UserConfiguration.GetUserConfiguration();
            
        }
        public static MainViewModel GetMainViewModel()
        {
            if (mainViewModel == null)
            {
                lock (syncRoot)
                {
                    if (mainViewModel == null)
                        mainViewModel = new MainViewModel();
                }
            }
            return mainViewModel;
        }

        public UserConfiguration UserConfiguration;


        public void AddCarPartOemNumberIntoCategory(int categoryId)
        {

        }

        public void AddProductToShoppingCart(Product product, short productCount)
        {
            OrderPart orderPart = new OrderPart();
            orderPart.Product = product;
            orderPart.ProductCount = productCount;
            UserConfiguration.ShoppingCart.AddOrderPart(orderPart);
        }
        public void MakeNewOrder()
        {
            Order order = UserConfiguration.ShoppingCart;
            order.Customer = UserConfiguration.Customer;
            if(order.Customer == null)
            {
                WindowProvider.NotifynWindow("В начале войдите или зарегестрируйтесь");
            }
            else
            {
                try
                {
                    StoreService.OrderService.AddOrder(order);
                }
                catch(Exception e)
                {

                }
                UserConfiguration.UpdateShopingCart();
                
            }    
        }

        public void SearchString(string str)
        {
            try
            {
                WindowProvider.OpenFirstPage();
                WindowProvider.OpenProductsList();
                if (mainViewModel.ProductsViewModel != null)
                {
                    mainViewModel.ProductsViewModel.UpdateProductsList(StoreService.ProductService.SearchProductByString(str));
                }
            }
            catch(Exception e)
            {
                WindowProvider.NotifynWindow(e.Message);
            }
        }
    }
}
