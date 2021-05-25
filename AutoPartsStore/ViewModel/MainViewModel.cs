using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.ViewModel
{
    class MainViewModel
    {
        private static MainViewModel mainViewModel;
        private static object syncRoot = new Object();

        public CategoriesViewModel CategoriesViewModel { get; set; }
        public ProductsViewModel ProductsViewModel { get; set; }
        public ProductViewModel ProductViewModel { get; set; }
        public AddEntityPanelViewModel AddEntityPanelViewModel { get; set; }


        public NewCarViewModel NewCarViewModel { get; set; }

        public ChooseCarViewModel ChooseCarViewModel { get; set; }

        public AddOemToCarCategoryViewModel AddOemToCarCategoryViewModel { get; set; }
        public UserViewModel UserViewModel { get; set; }

        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }

        public MainViewModel()
        {
            userConfiguration =  UserConfiguration.GetUserConfiguration();
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

        UserConfiguration userConfiguration;


        public void AddCarPartOemNumberIntoCategory(int categoryId)
        {

        }

        public void AddProductToShoppingCart(Product product, short productCount)
        {
            OrderPart orderPart = new OrderPart();
            orderPart.Product = product;
            orderPart.ProductCount = productCount;
            userConfiguration.ShoppingCart.AddOrderPart(orderPart);
        }
        public void MakeNewOrder()
        {
            Order order = userConfiguration.ShoppingCart;
            userConfiguration.UpdateShopingCart();


        }
    }
}
