using AutoPartsStore.View.Admin;
using AutoPartsStore.View.Manufacturer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AutoPartsStore.ViewModel
{
    public static class WindowProvider
    {
        
        private static string _ChoiceCarPagePath = "Vehicle/ChoiceCarPage.xaml";
        private static string _ProductsShowPagePath = "Product/ProductsShowPage.xaml";

        private static string _UserProductPagePath = "User/Product/ProductPage.xaml";
        private static string _UserInfoPagePath = "User/UserPage.xaml";
        private static string _UserShoppingCartPagePath = "User/ShoppingCartPage.xaml";
        private static string _UserOrdersPagePath = "User/OrdersPage.xaml";
        private static string _UserCategoryPagePath = "User/Category/CategoryPage.xaml";


        private static string _AdminProductPagePath = "Admin/Product/AdminProductPage.xaml";
        private static string _AdminOrdersPagePath = "Admin/AdminOrdersPage.xaml";

        private static string _AdminCategoryPagePath = "Admin/Category/AdminCategoryPage.xaml";
        private static string _AdminEditDataBasePagePath = "Admin/EditDataBasePage.xaml";

        private static string _AdminAddOemToCarCategoryPagePath = "Admin/Vehicle/AddOemToCarCategoryPage.xaml";
        private static string _AdminAddNewCarPagePath = "Admin/Vehicle/AddNewCarPage.xaml";
        private static string _AdminEditCategoryPagePath = "Admin/Category/EditCategoryPage.xaml";
        private static string _AdminNewProductPagePath = "Admin/Product/NewProductPage.xaml";
        private static string _AdminEditManufacturerPagePath = "Admin/Manufacturer/EditManufacturerPage.xaml";
       
        


        private static string _FirstPagePath = "FirstPage.xaml";


        private static UserConfiguration _userConfiguration = UserConfiguration.GetUserConfiguration();

        public static void AdminOpenEditEntityPage(string pageName)
        {
            if (WorkSpacePage != null)
            {
                if (_userConfiguration.IsAdmin)
                {
                    if (pageName.Equals("EditCar"))
                    {
                        WorkSpacePage.Source = new Uri(_AdminAddNewCarPagePath, UriKind.Relative);
                    }
                    else if (pageName.Equals("AddProduct"))
                    {
                        WorkSpacePage.Source = new Uri(_AdminNewProductPagePath, UriKind.Relative);
                    }
                    else if (pageName.Equals("EditCategory"))
                    {
                        WorkSpacePage.Source = new Uri(_AdminEditCategoryPagePath, UriKind.Relative);
                    }
                    else if (pageName.Equals("EditManufacturer"))
                    {
                        WorkSpacePage.Source = new Uri(_AdminEditManufacturerPagePath, UriKind.Relative);
                    }
                }
                else
                {
                    MessageBox.Show("Недостаточно прав");
                }
            }
            else
                throw new Exception("Chto nado");
        }

        public static ConfirmAdminWindow confirmAdminWindow;
        public static void OpenConfirmAdminWindow()
        {
            confirmAdminWindow = new ConfirmAdminWindow();
            confirmAdminWindow.Show();
        }
        public static void CloseConfirmAdminWindow()
        {
            if(confirmAdminWindow != null)
            {
                confirmAdminWindow.Close();
                confirmAdminWindow = null;
            }
        }

        public static void OpenProductPage()
        {
            if (WorkSpacePage != null)
            {
                if (_userConfiguration.IsAdmin)
                {
                    WorkSpacePage.Source = new Uri(_AdminProductPagePath, UriKind.Relative);
                }
                else
                {
                    WorkSpacePage.Source = new Uri(_ProductsShowPagePath, UriKind.Relative);
                }
            }
            else
                throw new Exception("Chto nado");
        }

        public static Frame MainFrame { get; set; }
        public static Frame WorkSpacePage { get; set; }
        public static Frame EditDataBaseFrame { get; set; }
        public static Frame CategoryFrame { get; set; }
        public static Frame ChoiceCarFrame { get; set; }

        public static void OpenFirstPage()
        {
            if (MainFrame != null)
            {
                MainFrame.Source = new Uri(_FirstPagePath, UriKind.Relative);
            }
            else
                throw new Exception("Chto nado");
        }

        public static void FillUserPage()
        {
            ChoiceCarFrame.Source = new Uri(_ChoiceCarPagePath, UriKind.Relative);
            if (_userConfiguration.IsAdmin)
            {
                CategoryFrame.Source = new Uri(_AdminCategoryPagePath, UriKind.Relative);
                EditDataBaseFrame.Source = new Uri(_AdminEditDataBasePagePath, UriKind.Relative);
            }
            else
            {
                EditDataBaseFrame.Source = null;
                CategoryFrame.Source = new Uri(_UserCategoryPagePath, UriKind.Relative);
            }
            //WorkSpacePage.Source = new Uri(_ProductsShowPagePath, UriKind.Relative);
        }


        public static void OpenAddOemToVehicleCategoryWindow()
        {
            if (WorkSpacePage != null)
            {
                if (_userConfiguration.IsAdmin)
                {
                    WorkSpacePage.Source = new Uri(_AdminAddOemToCarCategoryPagePath, UriKind.Relative);
                    AddOemToCarCategoryViewModel addOemToCarCategoryViewModel = MainViewModel.GetMainViewModel().AddOemToCarCategoryViewModel;
                    if (addOemToCarCategoryViewModel != null)
                    {
                        addOemToCarCategoryViewModel.UpdateOemToCarCategoryPage();
                    }
                }
                else
                {
                    MessageBox.Show("Недостаточно прав");
                }
                
            }
            else
                throw new Exception("Chto nado");
        }

        public static void OpenCategoryProductListWindow()
        {
            if (WorkSpacePage != null)
            {
                WorkSpacePage.Source = new Uri(_ProductsShowPagePath, UriKind.Relative);
            }
            else
                throw new Exception("Chto nado");
        }

        public static void OpenProductWindow()
        {
            if (WorkSpacePage != null)
            {
                if (_userConfiguration.IsAdmin)
                {
                    MainFrame.Source = new Uri(_AdminProductPagePath, UriKind.Relative);
                }
                else
                {
                    MainFrame.Source = new Uri(_UserProductPagePath, UriKind.Relative);
                }
            }
            else
                throw new Exception("Chto nado");
        }

        public static void OpenUserWindow()
        {
            if (WorkSpacePage != null)
            {
                MainFrame.Source = new Uri(_UserInfoPagePath, UriKind.Relative);
            }
            else
                throw new Exception("Chto nado");
        }

        private static Uri uri;
        public static void OpenProductsList()
        {
            if (WorkSpacePage != null)
            {
                    WorkSpacePage.Source = uri ?? (uri = new Uri(_ProductsShowPagePath, UriKind.Relative));
            }
            else
                throw new Exception("Chto nado");
        }
        public static void OpenShoppingCartWindow()
        {
            if (WorkSpacePage != null)
            {
                MainFrame.Source = new Uri(_UserShoppingCartPagePath, UriKind.Relative);
                ShoppingCartViewModel shoppingCartViewModel = MainViewModel.GetMainViewModel().ShoppingCartViewModel;
                if (shoppingCartViewModel != null)
                {
                    shoppingCartViewModel.UpdateShoppingCart();
                }
            }
            else
                throw new Exception("Chto nado");
        }


        public static void OpenAddManufacturerWindow()
        {
            AddManufacturerWindow addManufacturerWindow = new AddManufacturerWindow();
            addManufacturerWindow.Show();
        }

        public static void OpenOrderWindow()
        {
            if (WorkSpacePage != null)
            {
                if (_userConfiguration.IsAdmin)
                {
                    MainFrame.Source = new Uri(_AdminOrdersPagePath, UriKind.Relative);
                }
                else
                {
                    MainFrame.Source = new Uri(_UserOrdersPagePath, UriKind.Relative);
                }

            }
            else
                throw new Exception("Chto nado");
        }

       
        public static void AdminOpenEditCategoryWindow() {
            if (WorkSpacePage != null)
            {
                if (_userConfiguration.IsAdmin)
                {
                    WorkSpacePage.Source = new Uri(_AdminEditCategoryPagePath, UriKind.Relative);
                }
                else
                {
                    
                }

            }
            else
                throw new Exception("Chto nado");
        }
    }
}
