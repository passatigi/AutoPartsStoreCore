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
    /// Логика взаимодействия для EditDataBasePage.xaml
    /// </summary>
    public partial class EditDataBasePage : Page
    {
        public EditDataBasePage()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;
                if (button == AddCarButton)
                {
                    WindowProvider.AdminOpenEditEntityPage("EditCar");
                }
                else if (button == AddProductButton)
                {
                    WindowProvider.AdminOpenEditEntityPage("AddProduct");
                }
                else if (button == manufacturerButton)
                {
                    WindowProvider.AdminOpenEditEntityPage("EditManufacturer");
                }
            }
        }
    }
}
