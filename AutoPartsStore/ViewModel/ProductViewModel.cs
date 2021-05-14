using AutoPartsStore.Model;
using AutoPartsStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsStore.ViewModel
{
    class ProductViewModel : INotifyPropertyChanged
    {
        private static ProductViewModel _productViewModel;
        public static ProductViewModel ProductViewModelObject {
            get
            {
                if(_productViewModel != null)
                {
                    return _productViewModel;
                }
                throw new Exception("_productViewModel is null");
            }
            private set
            {

            }
        }

        private Product product;
        public Product Product
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
                NotifyPropertyChanged("Product");
            }
        }
        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get
            {
                return products;
            }
            set
            {
                products = value;
                NotifyPropertyChanged("Products");
            }
        }
        public ProductViewModel()
        {
            _productViewModel = this;
            product = new Product();
            Category category = new Category();
            product.Availability = 10;
            product.Category = category;
            product.CategoryString = "Диск сцепления";
            product.Manufacturer = new Manufacturer();
            product.Manufacturer.Name = "stellox";
            product.VendorCode = new VendorCode();
            product.VendorCode.VendorCodeString = "07-00026-sx";
            product.ImagePath = "http://stellox.com/i/logo_ru.gif";
            product.Price = (decimal)(10.12);
            product.FeaturesString = "диаметр[мм]:215;профиль ступицы:23x26-23n;число зубцов:23;вес[кг]:1,07;";
            products = new ObservableCollection<Product>();
            products.Add(product);

            Product product2 = new Product();
            product2.Availability = 10;
            product2.Category = category;

            product2.CategoryString = "Диск сцепления";
            product2.Manufacturer = new Manufacturer();
            product2.Manufacturer.Name = "stellox";
            product2.VendorCode = new VendorCode();
            product2.VendorCode.VendorCodeString = "07-00026-sx";
            product2.ImagePath = "http://stellox.com/i/logo_ru.gif";
            product2.Price = (decimal)(10.12);
            product2.FeaturesString = "диаметр[мм]:215;профиль ступицы:23x26-23n;число зубцов:23;вес[кг]:1,07;";            
            products.Add(product2);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
