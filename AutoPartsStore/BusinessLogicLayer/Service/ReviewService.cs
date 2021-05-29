using AutoPartsStore.DataBaseLayer.UnitOfWork.Interfaces;
using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPartsStore.BusinessLogicLayer.Service
{
    public class ReviewService
    {
        IUnitOfWork unitOfWork;
        public ReviewService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void TryAddReview(Review review)
        {
            if (unitOfWork.ReviewRepository.GetAs(review).Where(r => r.CustomerId== review.CustomerId).Count() != 0)
            {
                throw new Exception("Вы уже оставляли отзыв");
            }
            else
            {
                review.DateTime = DateTime.Now;
                unitOfWork.ReviewRepository.Add(review);
                unitOfWork.Save();
            }
        }
        public void AddReview(Review review)
        {
                review.DateTime = DateTime.Now;
                unitOfWork.ReviewRepository.Add(review);
                unitOfWork.Save();
        }
        public void UpdateReview(Review review)
        {
            Review tempReview = unitOfWork.ReviewRepository.GetAs(review).FirstOrDefault();
            tempReview.Image = review.Image;
            tempReview.ReviewText = review.ReviewText;
            tempReview.Rating = review.Rating;
            tempReview.DateTime = review.DateTime;
            unitOfWork.ReviewRepository.Update(tempReview);
            unitOfWork.Save();

        }
        public IEnumerable<Review> GetReviews(Product product)
        {
            return unitOfWork.ReviewRepository.GetAll().Where(r => r.Product != null && r.Product.Id == product.Id);
        }
        public void DeleteReview(Review review)
        {
            unitOfWork.ReviewRepository.Delete(review.Id);
            unitOfWork.Save();
        }
    }
}
