using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace AutoPartsStore.ViewModel
{
    public class ProductViewModel : BaseViewModel
    {

        MainViewModel mainViewModel;

        IStoreService storeService;
        public ProductViewModel()
        {
            mainViewModel = MainViewModel.GetMainViewModel();
            mainViewModel.ProductViewModel = this;

            storeService = StoreService.GetStoreService();

            UpdateProduct();
            ProductCount = 1;
            UserReview = new Review();
            UpdateReviews();

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

        
        private RelayCommand addToShoppingCartCommand;
        public RelayCommand AddToShoppingCartCommand
        {
            get
            {
                return addToShoppingCartCommand ?? (addToShoppingCartCommand = new RelayCommand(action =>
                {
                    mainViewModel.AddProductToShoppingCart(product, productCount);
                }, func =>
                {
                    return true;
                }));
            }
        }
        private RelayCommand addReviewCommand;
        public RelayCommand AddReviewCommand
        {
            get
            {
                return addReviewCommand ?? (addReviewCommand = new RelayCommand(action =>
                {
                    Customer customer = UserConfiguration.GetUserConfiguration().Customer;
                    if (customer == null)
                    {
                        WindowProvider.NotifyWindow("Перед тем как оставлять отзывы войдите");
                    }
                    else
                    {
                        UserReview.Customer = customer;
                        UserReview.Product = product;
                        try
                        {
                            storeService.ReviewService.AddReview(UserReview);
                            UpdateReviews();
                            UserReview = new Review();
                            WindowProvider.NotifyWindow("отзыв успешно добавлен");
                        }
                        catch (ReviewExistsException re)
                        {
                            WindowProvider.NotifyWindow(re.Message);
                        }
                        catch (Exception e)
                        {

                        }

                    }
                }, func =>
                {
                    return true;
                }));
            }
        }
        public void UpdateReviews()
        {
            Reviews.Clear();
            foreach(Review review in storeService.ReviewService.GetReviews(product).OrderByDescending(p => p.DateTime))
            {
                Reviews.Add(review);
            }
        }

        private Review userReview;
        public Review UserReview
        {
            get
            {
                return userReview;
            }
            set
            {
                SetProperty(ref userReview, value);
            }
        }
        public int ReviewsCount
        {
            get
            {
                return reviews.Count;
            }
        }
        private ObservableCollection<Review> reviews;
        public ObservableCollection<Review> Reviews
        {
            get
            {
                return reviews ?? (reviews = new ObservableCollection<Review>());
            }
            set
            {
                SetProperty(ref reviews, value);
            }
        }

    }
}
