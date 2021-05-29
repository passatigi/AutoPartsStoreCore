using AutoPartsStore.Command;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.ViewModel
{
    class AdminProductViewModel : ProductViewModel
    {
        public AdminProductViewModel() : base()
        {
        }

        public override void UpdateProduct()
        {
            try
            {
                Product = mainViewModel.UserConfiguration.SelectedProduct;
                NewProductViewModel.UpdateProductInfo(Product);
            }
            catch (Exception e)
            {
                WindowProvider.NotifyWindow(e.Message);
            }

        }
        public EditProductViewModel newProductViewModel;
        public EditProductViewModel NewProductViewModel
        {
            get
            {
                return newProductViewModel ?? (newProductViewModel = new EditProductViewModel(Product));
            }
            set
            {
                SetProperty(ref newProductViewModel, value);
            }
        }
        private RelayCommand saveProductChangesCommand;
        public RelayCommand SaveProductChangesCommand
        {
            get
            {
                return saveProductChangesCommand ?? (saveProductChangesCommand = new RelayCommand(action =>
                {
                    try
                    {
                        mainViewModel.StoreService.ProductService.UpdateProduct(Product);
                    }
                    catch (Exception e)
                    {
                        WindowProvider.NotifyWindow(e.Message);
                    }

                }
                , func =>
                {
                    return true;
                }));
            }
        }

        private RelayCommand deleteReviewCommand;
        public RelayCommand DeleteReviewCommand
        {
            get
            {
                return deleteReviewCommand ?? (deleteReviewCommand = new RelayCommand(action =>
                {
                    try
                    {
                        if(action is Review)
                        {
                            mainViewModel.StoreService.ReviewService.DeleteReview(action as Review);
                            Product = mainViewModel.UserConfiguration.SelectedProduct;
                            UpdateReviews();
                        }
                        
                    }
                    catch (Exception e)
                    {
                        WindowProvider.NotifyWindow(e.Message);
                    }

                }
                , func =>
                {
                    return true;
                }));
            }
        }

    }
}
