using AutoPartsStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoPartsStore.View
{
    /// <summary>
    /// Логика взаимодействия для FirstPage.xaml
    /// </summary>
    public partial class FirstPage : Page
    {
        public FirstPage()
        {
            InitializeComponent();
            WindowProvider.WorkSpacePage = WorkSpacePage;
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;
                if (button == AddCarButton)
                {
                    WorkSpacePage.Source = new Uri("Vehicle/AddNewCarPage.xaml", UriKind.Relative);
                }
                else if (button == AddProductButton)
                {
                    WorkSpacePage.Source = new Uri("Product/NewProductPage.xaml", UriKind.Relative);
                }
                else if (button == ProductsListButton)
                {
                    WorkSpacePage.Source = new Uri("Product/ProductsShowPage.xaml", UriKind.Relative);
                }
            }
        }
    }

}
