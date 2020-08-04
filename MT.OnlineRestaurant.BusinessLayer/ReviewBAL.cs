using System;
using System.Collections.Generic;
using System.Linq;
using MT.OnlineManagement.BusinessEntities;
using MT.OnlineRestaurant.DataAccessLayer.Entities;
using MT.OnlineRestaurant.DataAccessLayer.Repositories;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public class ReviewBAL : IReviewBAL
    {
        IReviewDAL  dalReviewRepository;
        private readonly string connectionstring;
        public ReviewBAL(IReviewDAL _dalreviewRepository)
        {
            dalReviewRepository = _dalreviewRepository;
        }
       
        public IQueryable<ReviewDetails> GetRestaurantRating(int restaurantID)
        {
            try
            {
                List<ReviewDetails> restaurantRatings = new List<ReviewDetails>();
                IQueryable<TblRating> rating;
                rating = dalReviewRepository.GetReviewRating(restaurantID);
                foreach (var item in rating)
                {
                    ReviewDetails reviewDetails = new ReviewDetails
                    {
                        Rating = item.Rating,
                        RestaurantId = item.TblRestaurantId,
                        Review = item.Comments,
                        CustId = item.TblCustomerId,
                        RestaurantName = item.TblRestaurant.Name
                    };
                    restaurantRatings.Add(reviewDetails);
                }
                return restaurantRatings.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<ReviewDetails> UpdateReviewRating(ReviewRating reviewRating)
        {
            try
            {
                List<ReviewDetails> restaurantRatings = new List<ReviewDetails>();
                IQueryable<TblRating> rating;
                DataAccessLayer.DataEntity.ReviewRating review = new DataAccessLayer.DataEntity.ReviewRating
                {
                    Rating = reviewRating.Rating,
                    ReviewComments = reviewRating.ReviewComments,
                    RestaurantID = reviewRating.RestaurantID,
                    CustomerId = reviewRating.CustomerId
                };
                rating = dalReviewRepository.UpdateReviewRating(review);
                foreach (var item in rating)
                {
                    ReviewDetails reviewDetails = new ReviewDetails
                    {
                        Rating = item.Rating,
                        RestaurantId = item.TblRestaurantId,
                        Review = item.Comments,
                        CustId = item.TblCustomerId,
                        RestaurantName = item.TblRestaurant.Name
                    };
                    restaurantRatings.Add(reviewDetails);
                }
                return restaurantRatings.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<ReviewDetails> RestaurantReviewRating(ReviewRating reviewRating)
        {
            try
            {
                List<ReviewDetails> restaurantRatings = new List<ReviewDetails>();
                IQueryable<TblRating> rating;
                DataAccessLayer.DataEntity.ReviewRating review = new DataAccessLayer.DataEntity.ReviewRating
                {
                    Rating = reviewRating.Rating,
                    ReviewComments = reviewRating.ReviewComments,
                    RestaurantID = reviewRating.RestaurantID,
                    CustomerId = reviewRating.CustomerId
                };
                rating = dalReviewRepository.RestaurantReviewRating(review);
                foreach (var item in rating)
                {
                    ReviewDetails reviewDetails = new ReviewDetails
                    {
                        Rating = item.Rating,
                        RestaurantId = item.TblRestaurantId,
                        Review = item.Comments,
                        CustId = item.TblCustomerId,
                        RestaurantName = item.TblRestaurant.Name
                    };
                    restaurantRatings.Add(reviewDetails);
                }
                return restaurantRatings.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
