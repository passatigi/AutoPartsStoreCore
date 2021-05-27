using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using AutoPartsStore.Model;
using AutoPartsStore.Model.Vehicle;
using AutoPartsStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutoPartsStore.ViewModel
{
    public class ProductsViewModel : BaseViewModel
    {

        private void FilProducts()
        {
            IEnumerable<Product> products = Products.AsEnumerable();
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
                products = value;
                NotifyPropertyChanged(nameof(Products));
                //SetProperty(ref products, value);
            }
        }
        private VehiclePart vehiclePart;

        public void UpdateProductsList()
        {
            if(Products == null)
            {
                Products = new ObservableCollection<Product>(); 
            }
            Products.Clear();
            vehiclePart = storeService.VehiclePartService.GetVehiclePart(
                UserConfiguration.GetUserConfiguration().SelectedVehicleEngine,
                UserConfiguration.GetUserConfiguration().SelectedCategory
                );
            foreach(Product product in storeService.ProductService.GetProductsByVehiclePart(vehiclePart))
            {
                Products.Add(product);
            }
            
            NotifyPropertyChanged("Products");

        }
        public void UpdateProductsList(IEnumerable<Product> products)
        {
            if (Products == null)
            {
                Products = new ObservableCollection<Product>();
            }
            Products.Clear();
            foreach (Product product in products)
            {
                Products.Add(product);
            }
            //Products.Add(new Product());
            NotifyPropertyChanged("Products");

            //Products.
        }

        private RelayCommand aboutProductCommand;
        public RelayCommand AboutProductCommand
        {
            get
            {
                return aboutProductCommand ?? (aboutProductCommand = new RelayCommand(action =>
                {
                    if (action is long)
                    {
                        UserConfiguration.GetUserConfiguration().SelectedProduct = storeService.ProductService.GetById((long)action);
                        WindowProvider.OpenProductWindow();
                        if(mainViewModel.ProductViewModel != null)
                        {
                            mainViewModel.ProductViewModel.UpdateProduct();
                        }

                    }

                }, func =>
                {
                    return true;
                }));
            }
        }
        private RelayCommand addToShoppingCartCommand;
        public RelayCommand AddToShoppingCartCommand
        {
            get
            {
                return addToShoppingCartCommand ?? (addToShoppingCartCommand = new RelayCommand(action =>
                {
                    if(action is long)
                    {
                        mainViewModel.AddProductToShoppingCart(storeService.ProductService.GetById((long)action), 1);
                    }
                }, func =>
                {
                    return true;
                }));
            }
        }

        MainViewModel mainViewModel;
        
        IStoreService storeService;
        public ProductsViewModel()
        {
            mainViewModel = MainViewModel.GetMainViewModel();
            mainViewModel.ProductsViewModel = this;

            storeService = StoreService.GetStoreService();

            Products = new ObservableCollection<Product>();
            UpdateProductsList();
        }
       
    }
}
