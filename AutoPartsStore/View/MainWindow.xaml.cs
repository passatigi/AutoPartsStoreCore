using AutoPartsStore.DataBaseConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoPartsStore
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //AutoPartsStoreContext aut = AutoPartsStoreContext.GetStoreContext();
            //aut.Database.Delete();
            //aut.Database.Initialize(false);
            Closing += OnWindowClosing;
        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            //AutoPartsStoreContext.GetStoreContext().Dispose();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //NewCarButtonText
            //if (AddCarButton.Content.Equals())
            //AddCarButton.Content = "Вернуться к продуктам"
            //if ()
            ProductOrAddCar.Source = new Uri("Vehicle/AddNewCarPage.xaml", UriKind.Relative);

        }
    }
}
