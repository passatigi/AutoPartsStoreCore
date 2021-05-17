using AutoPartsStore.BusinessLogicLayer.Service;
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
    class ProductViewModel : BaseViewModel
    {

        private void FilProducts()
        {
            IEnumerable<Product> products = storeService.ProductService.GetAllProducts();
            if (isSortByManufacturer)
            {
                if (isSortAcs)
                {
                    products = products.OrderBy(p => p.Manufacturer.Name);
                 }
                else
                {
                    products = products.OrderByDescending((p => p.Manufacturer.Name));
                }
            }
            else
            {
                if (isSortAcs)
                {
                    products = products.OrderBy(p => p.Price);
                }
                else
                {
                    products = products.OrderByDescending((p => p.Price));
                }
            }

            Products.Clear();
            foreach (Product product in products)
            {
                Products.Add(product);
            }
        }



        private bool isSortByManufacturer = true;

        public bool IsSortByManufacturer
        {
            get
            {
                return isSortByManufacturer;
            }
            set
            {
                if (value != isSortByManufacturer)
                {
                    isSortByManufacturer = value;
                    FilProducts();
                    NotifyPropertyChanged(nameof(isSortByManufacturer));

                }
                else
                {
                    SetProperty(ref isSortByManufacturer, value);
                }
            }
        }
        private bool isSortAcs = true;

        public bool IsSortAcs
        {
            get
            {
                return isSortAcs;
            }
            set
            {
                if (value != isSortAcs)
                {
                    isSortAcs = value;
                    FilProducts();
                    NotifyPropertyChanged(nameof(IsSortAcs));
                }
                else
                {
                    SetProperty(ref isSortAcs, value);
                }
            }
        }
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
                SetProperty(ref product, value);
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
                SetProperty(ref products, value);
            }
        }
        IStoreService storeService;
        public ProductViewModel()
        {
            storeService = StoreService.GetStoreService();
            Products = new ObservableCollection<Product>();
            FilProducts();
        }
       
    }
}
