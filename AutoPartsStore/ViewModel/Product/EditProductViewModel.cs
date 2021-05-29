using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using AutoPartsStore.DataBaseConnector;
using AutoPartsStore.DataBaseLayer;
using AutoPartsStore.Model;
using AutoPartsStore.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace AutoPartsStore.ViewModel
{
    class EditProductViewModel : BaseViewModel
    {


        IStoreService storeService;
        public EditProductViewModel()
        {
            storeService = StoreService.GetStoreService();
            Categories = new ObservableCollection<Category>();
            Manufacturers = new ObservableCollection<Manufacturer>();
            Feature = new Feature();
            product = new Product();
            //product.VendorCode = new VendorCode();
            //productOEMNumber = new ProductOEMNumber();
            //productOEMNumber.VendorCode = product.VendorCode;

            product.Features = new ObservableCollection<Feature>();
            VehicleBrands = new ObservableCollection<VehicleBrand>();
            FillCategories();
            FillManufacturers();
            FillVehicleBrands();
        }
        public EditProductViewModel(Product product)
        {
            storeService = StoreService.GetStoreService();
            Categories = new ObservableCollection<Category>();
            Manufacturers = new ObservableCollection<Manufacturer>();
            VehicleBrands = new ObservableCollection<VehicleBrand>();
            Feature = new Feature();

            FillCategories();
            FillManufacturers();
            FillVehicleBrands();
            this.Product = product;
        }
        public void UpdateProductInfo(Product product)
        {
            Feature = new Feature();

            FillCategories();
            FillManufacturers();
            FillVehicleBrands();
            this.Product = product;
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
        private void FillVehicleBrands()
        {
            VehicleBrands.Clear();
            foreach (VehicleBrand vehicleBrand in storeService.VehicleService.GetAllBrands())
            {
                VehicleBrands.Add(vehicleBrand);
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
        private ObservableCollection<VehicleBrand> vehicleBrands;
        public ObservableCollection<VehicleBrand> VehicleBrands
        {
            get
            {
                return vehicleBrands;
            }
            set
            {
                SetProperty(ref vehicleBrands, value);
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

        private string oemNumberString;
        public string OemNumberString
        {
            get
            {
                return oemNumberString;
            }
            set
            {
                SetProperty(ref oemNumberString, value);
            }
        }
        private VehicleBrand selectedVehicleBrand;
        public VehicleBrand SelectedVehicleBrand
        {
            get
            {
                return selectedVehicleBrand;
            }
            set
            {
                SetProperty(ref selectedVehicleBrand, value);
            }
        }


        private ProductOEMNumber productOEMNumber;
        public ProductOEMNumber ProductOEMNumber
        {
            get
            {
                return productOEMNumber;
            }
            set
            {
                SetProperty(ref productOEMNumber, value);
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
        private string searchManufacturerString;
        public string SearchManufacturerString
        {
            get
            {
                return searchManufacturerString;
            }
            set
            {
                SetProperty(ref searchManufacturerString, value);
            }
        }
        public void UpdateProductComboboxes()
        {
            FillCategories();
            FillManufacturers();
            FillVehicleBrands();
        }
        #region command

        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ?? (updateCommand = new RelayCommand(action =>
                {
                    try
                    {
                        UpdateProductComboboxes();
                    }
                    catch(Exception e)
                    {
                        WindowProvider.NotifyWindow(e.Message);
                    }
                }, func =>
                {
                    return true;
                }));
            }
        }

        private RelayCommand addOemCommand;
        public RelayCommand AddOemCommand
        {
            get
            {
                return addOemCommand ?? (addOemCommand = new RelayCommand(action =>
                {
                    ProductOEMNumber = new ProductOEMNumber();
                    productOEMNumber.Product = product;
                    productOEMNumber.OEM = oemNumberString;
                    productOEMNumber.VehicleBrand = selectedVehicleBrand;
                    product.ProductOEMNumbers.Add(ProductOEMNumber);
                }, func =>
                {
                    return true;
                }));
            }
        }
        public void ValidateProduct(Product product)
        {
            if (String.IsNullOrEmpty(product.VendorCode))
            {
                throw new Exception("Артикул должен быть указан");
            }
            else if (product.Price == 0)
            {
                throw new Exception("Укажите цену");
            }
            else if (product.Category == null)
            {
                throw new Exception("Выберите категорию");
            }
            else if (product.Manufacturer == null)
            {
                throw new Exception("Выберите производителя");
            }
            else if (product.ProductOEMNumbers != null && product.ProductOEMNumbers.Count() != 0)
            {
                foreach(ProductOEMNumber productOEMNumber in product.ProductOEMNumbers)
                {
                    if(String.IsNullOrEmpty(productOEMNumber.OEM) || productOEMNumber.VehicleBrand == null)
                    {
                        throw new Exception("Неправильно заполнены OEM номера");
                    }
                }
            }
        }
        private RelayCommand addProductCommand;
        public RelayCommand AddProductCommand
        {
            get
            {
                return addProductCommand ?? (addProductCommand = new RelayCommand(action =>
                {
                    try
                    {
                        ValidateProduct(Product);
                        storeService.ProductService.AddProduct(Product);
                        Product = new Product(); 
                    }
                    catch(Exception e)
                    {
                        WindowProvider.NotifyWindow(e.Message);
                    }
                    
                }, func =>
                {
                    return true;
                }));
            }
        }
        private RelayCommand deleteOemNumberCommand;
        public RelayCommand DeleteOemNumberCommand
        {
            get
            {
                return deleteOemNumberCommand ?? (deleteOemNumberCommand = new RelayCommand(action =>
                {
                    try
                    {
                        if (action is ProductOEMNumber)
                        {
                            ProductOEMNumber oem = (ProductOEMNumber)action;
                            product.ProductOEMNumbers.Remove(oem);
                        }
                        else if (action is Feature)
                        {
                            Feature feature = (Feature)action;
                            product.Features.Remove(feature);
                        }
                    }
                    catch(Exception e)
                    {
                        WindowProvider.NotifyWindow(e.Message);
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
                        storeService.CategoryService.GetAllCategories().Where(c => c.Name.Contains(SearchCategoryString)))
                        {
                            Categories.Add(category);
                        }
                    }
                    else if (action == manufacturers)
                    {
                        Manufacturers.Clear();
                        foreach (Manufacturer manufacturer in
                        storeService.ManufacturerService.GetAllManufacturers().Where(c => c.Name.Contains(SearchManufacturerString)))
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

        private RelayCommand updateProductCount;
        public RelayCommand UpdateProductCount
        {
            get
            {
                return updateProductCount ?? (updateProductCount = new RelayCommand(action =>
                {
                    if (action is string)
                    {
                        string parm = action as string;
                        if (parm.Equals("+"))
                        {
                            Product.Availability++;
                        }
                        else if (parm.Equals("-"))
                        {
                            Product.Availability--;
                        }
                    }
                }, func =>
                {
                    return true;
                }));
            }
        }

    }
}
