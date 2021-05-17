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
        private string searchCategoryString;
        public string SearchCategoryString
        {
            get
            {
                return searchCategoryString;
            }
            set
            {
                SetProperty(ref searchCategoryString, value);
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
                    if (action is int)
                    {
                        //efStoreUnitOfWork.
                        //efStoreUnitOfWork.db.Products
                    }

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

        private RelayCommand findCategoryCommand;
        public RelayCommand FindCategoryCommand
        {
            get
            {
                return findCategoryCommand ?? (findCategoryCommand = new RelayCommand(action =>
                {
                    if (action is string)
                    {
                        Categories.Clear();
                        foreach (Category category in
                        storeService.CategoryService.GetAllCategories().Where(c => c.Name.Contains(SearchCategoryString)))
                        {
                            Categories.Add(category);
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
