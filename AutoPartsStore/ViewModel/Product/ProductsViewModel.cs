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
using System.Windows;
using System.Windows.Media;

namespace AutoPartsStore.ViewModel
{
    public class ProductsViewModel : BaseViewModel
    {

        private void FilProducts()
        {
            List<Product> tempProducts = Products.ToList();
            if (isSortByManufacturer)
            {
                if (isSortAcs)
                {
                    tempProducts = tempProducts.OrderBy(p => p.Manufacturer.Name).ToList();
                 }
                else
                {
                    tempProducts = tempProducts.OrderByDescending((p => p.Manufacturer.Name)).ToList();
                }
            }
            else
            {
                if (isSortAcs)
                {
                    tempProducts = tempProducts.OrderBy(p => p.Price).ToList();
                }
                else
                {
                    tempProducts = tempProducts.OrderByDescending((p => p.Price)).ToList();
                }
            }

            Products.Clear();
            foreach (Product product in tempProducts)
            {
                Products.Add(product);
            }
            NotifyPropertyChanged("Products");
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
        private ObservableCollection<Manufacturer> manufacturers;
        public ObservableCollection<Manufacturer> Manufacturers
        {
            get
            {
                return manufacturers;
            }
            set
            {
                SetProperty(ref manufacturers, value);
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
            Products.Clear();
            Manufacturers.Clear();
            VehicleEngine vehicleEngine = UserConfiguration.GetUserConfiguration().SelectedVehicleEngine;
            Category category = UserConfiguration.GetUserConfiguration().SelectedCategory;
            if(vehicleEngine != null && category != null)
            {
                try
                {
                    vehiclePart = storeService.VehiclePartService.GetVehiclePart(
                    vehicleEngine, category);
                    FillProducts(storeService.ProductService.GetProductsByVehiclePart(vehiclePart));
                    NotifyPropertyChanged("Products");
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                MessageBox.Show("Автомобиль не выбран");
            }
        }
        private void FillProducts(IEnumerable<Product> products)
        {
            unshowedProducs = null;
            foreach (Product product in products)
            {
                Products.Add(product);
                if (!Manufacturers.Contains(product.Manufacturer))
                {
                    Manufacturers.Add(product.Manufacturer);
                }
            }
        }
        private List<Product> unshowedProducs;
        private void FillManufacturerProducts(int manufacturerId)
        {
            if (manufacturerId != 0)
            {
                if (unshowedProducs == null)
                {
                    unshowedProducs = products.ToList();
                }
                Products.Clear();
                foreach (Product product in unshowedProducs)
                {
                    if (product.Manufacturer.Id == manufacturerId)
                    {
                        Products.Add(product);
                    }
                }
                NotifyPropertyChanged("Products");
            }
        }

        public void UpdateProductsList(IEnumerable<Product> products)
        {
            Products.Clear();
            Manufacturers.Clear();
            FillProducts(products);
            NotifyPropertyChanged("Products");
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

        private RelayCommand showManufacturerProductCommand;
        public RelayCommand ShowManufacturerProductCommand
        {
            get
            {
                return showManufacturerProductCommand ?? (showManufacturerProductCommand = new RelayCommand(action =>
                {
                    if(action is int)
                    {
                        FillManufacturerProducts((int)action);
                    }
                    if(action is string)
                    {
                        if(unshowedProducs != null)
                        {
                            UpdateProductsList(unshowedProducs);
                        } 
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
            Manufacturers = new ObservableCollection<Manufacturer>();
            UpdateProductsList();
        }
       
    }
}
