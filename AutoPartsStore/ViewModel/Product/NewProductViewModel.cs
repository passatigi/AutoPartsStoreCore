using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using AutoPartsStore.DataBaseConnector;
using AutoPartsStore.DataBaseLayer;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace AutoPartsStore.ViewModel
{
    class NewProductViewModel : BaseViewModel
    {
        //private long id;
        //private Category category;
        //private Manufacturer manufacturer;
        //private VendorCode vendorCode;
        //private string imagePath;
        //private Decimal price;
        //private int availability;
        //private string description;
        //private ObservableCollection<Feature> features;

        IStoreService storeService;
            public NewProductViewModel()
        {
            storeService = StoreService.GetStoreService();
            Categories = new ObservableCollection<Category>();
            Manufacturers = new ObservableCollection<Manufacturer>();
            Feature = new Feature();
            product = new Product();
            product.Features = new ObservableCollection<Feature>();
            FillCategories();
            FillManufacturers();
        }
        private void FillCategories()
        {
            Categories.Clear();
            foreach (Category category in storeService.CategoryService.GetAllCategories())
            {
                Categories.Add(category);
            }
        }
        private void FillManufacturers()
        {
            Manufacturers.Clear();
            foreach (Manufacturer manufacturer in storeService.ManufacturerService.GetAllManufacturers())
            {
                Manufacturers.Add(manufacturer);
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

        private ObservableCollection<Category> categories;
        public ObservableCollection<Category> Categories
        {
            get
            {
                return categories;
            }
            set
            {
                SetProperty(ref categories, value);
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

        private Feature feature;
        public Feature Feature
        {
            get
            {
                return feature;
            }
            set
            {
                SetProperty(ref feature, value);
            }
        }
        private string searchString;
        public string SearchString
        {
            get
            {
                return searchString;
            }
            set
            {
                SetProperty(ref searchString, value);
            }
        }
        #region command
        //AddManufacturerCommand
        private RelayCommand addProductCommand;
        public RelayCommand AddProductCommand
        {
            get
            {
                return addProductCommand ?? (addProductCommand = new RelayCommand(action =>
                {
                    storeService.ProductService.AddProduct(Product);
                    Product = new Product();

                }, func =>
                {
                    return true;
                }));
            }
        }
        private RelayCommand addManufacturerCommand;
        public RelayCommand AddManufacturerCommand
        {
            get
            {
                return addManufacturerCommand ?? (addManufacturerCommand = new RelayCommand(action =>
                {
                    WindowProvider.OpenAddManufacturerWindow();
                }, func =>
                {
                    return true;
                }));
            }
        }
        private RelayCommand addFeatureCommand;
        public RelayCommand AddFeatureCommand
        {
            get
            {
                return addFeatureCommand ?? (addFeatureCommand = new RelayCommand(action =>
                {
                    product.Features.Add(Feature);
                    Feature = new Feature();
                }, func =>
                {
                    return true;
                }));
            }
        }

        private RelayCommand findStringCommand;
        public RelayCommand FindStringCommand
        {
            get
            {
                return findStringCommand ?? (findStringCommand = new RelayCommand(action =>
                {
                    if (action == categories)
                    {
                        Categories.Clear();
                        foreach (Category category in
                        storeService.CategoryService.GetAllCategories().Where(c => c.Name.Contains(SearchString)))
                        {
                            Categories.Add(category);
                        }
                    }
                    else if (action == manufacturers)
                    {
                        Manufacturers.Clear();
                        foreach (Manufacturer manufacturer in
                        storeService.ManufacturerService.GetAllManufacturers().Where(c => c.Name.Contains(SearchString)))
                        {
                            Manufacturers.Add(manufacturer);
                        }
                    }

                }, func =>
                {
                    return true;
                }));
            }
        }
        #endregion

        //private BitmapImage bitmapImage;
        //public BitmapImage BitmapImage
        //{
        //    get
        //    {
        //        return bitmapImage;
        //    }
        //    set
        //    {
        //        SetProperty(ref bitmapImage, value);
        //    }
        //}

    }
}
