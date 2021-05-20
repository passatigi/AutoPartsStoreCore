using AutoPartsStore.DataBaseConnector;
using AutoPartsStore.ViewModel;
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
            WindowProvider.WorkSpacePage = WorkSpacePage;

            Closing += OnWindowClosing;

            List<string> styles = new List<string> { "Dark", "White" };
            styleBox.SelectionChanged += ThemeChange;
            styleBox.ItemsSource = styles;
            styleBox.SelectedItem = "White";
        }
        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            string style = styleBox.SelectedItem as string;
            var uri = new Uri("View/Resources/Dictionary" + style + ".xaml", UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
             uri = new Uri("View/Resources/DictionaryDefault.xaml", UriKind.Relative);
            resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            uri = new Uri("View/Resources/RusDictionary.xaml", UriKind.Relative);
            resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            //AutoPartsStoreContext.GetStoreContext().Dispose();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button)
            {
                Button button = sender as Button;
                if(button == AddCarButton) {
                    WorkSpacePage.Source = new Uri("Vehicle/AddNewCarPage.xaml", UriKind.Relative);
                }
                else if(button == AddProductButton)
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
