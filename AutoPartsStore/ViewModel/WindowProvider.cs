using AutoPartsStore.View.Manufacturer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace AutoPartsStore.ViewModel
{
    public static class WindowProvider
    {

        public static Frame MainFrame { get; set; }
        public static Frame WorkSpacePage { get; set; }

        public static void OpenAddOemToVehicleCategoryWindow()
        {
            if (WorkSpacePage != null)
            {
                WorkSpacePage.Source = new Uri("Vehicle/AddOemToCarCategoryPage.xaml", UriKind.Relative);
                AddOemToCarCategoryViewModel addOemToCarCategoryViewModel = MainViewModel.GetMainViewModel().AddOemToCarCategoryViewModel;
                if (addOemToCarCategoryViewModel != null)
                {
                    addOemToCarCategoryViewModel.UpdateOemToCarCategoryPage();
                }
            }
            else
                throw new Exception("Chto nado");
        }

        public static void OpenCategoryProductListWindow()
        {
            if (WorkSpacePage != null)
            {
                WorkSpacePage.Source = new Uri("Product/ProductsShowPage.xaml", UriKind.Relative);
            }
            else
                throw new Exception("Chto nado");
        }

        public static void OpenProductWindow()
        {
            if (WorkSpacePage != null)
            {
                MainFrame.Source = new Uri("Product/ProductPage.xaml", UriKind.Relative);
            }
            else
                throw new Exception("Chto nado");
        }

        public static void OpenUserWindow()
        {
            if (WorkSpacePage != null)
            {
                MainFrame.Source = new Uri("User/UserPage.xaml", UriKind.Relative);
            }
            else
                throw new Exception("Chto nado");
        }


        public static void OpenAddManufacturerWindow()
        {
            AddManufacturerWindow addManufacturerWindow = new AddManufacturerWindow();
            addManufacturerWindow.Show();
        }


       

    }
}
