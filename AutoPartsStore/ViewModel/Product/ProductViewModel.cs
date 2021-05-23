using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.ViewModel
{
    class ProductViewModel : BaseViewModel
    {

        MainViewModel mainViewModel;

        IStoreService storeService;
        public ProductViewModel()
        {
            mainViewModel = MainViewModel.GetMainViewModel();
            mainViewModel.ProductViewModel = this;

            storeService = StoreService.GetStoreService();

            UpdateProduct();


        }
        public void UpdateProduct()
        {
            Product = UserConfiguration.GetUserConfiguration().SelectedProduct;
            ProductCount = 0;
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
        private short productCount;
        public short ProductCount
        {
            get
            {
                return productCount;
            }
            set
            {
                if (Product != null)
                {
                    if (value < 0 || value > Product.Availability)
                    {

                    }
                    else
                    {
                        SetProperty(ref productCount, value);
                    }
                }
                
            }
        }
        
        private RelayCommand updateProductCount;
        public RelayCommand UpdateProductCount
        {
            get
            {
                return updateProductCount ?? (updateProductCount = new RelayCommand(action =>
                {
                    if(action is string)
                    {
                        string parm = action as string;
                        if (parm.Equals("+"))
                        {
                            if(productCount != product.Availability)
                            {
                                ProductCount++;
                            }
                        }
                        else if(parm.Equals("-"))
                        {
                            if(productCount != 0)
                            {
                                ProductCount--;
                            }
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
