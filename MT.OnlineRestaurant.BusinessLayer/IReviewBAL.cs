using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MT.OnlineManagement.BusinessEntities;

namespace MT.OnlineRestaurant.BusinessLayer
{
   public interface IReviewBAL
    {

        IQueryable<ReviewDetails> GetRestaurantRating(int restaurantID);

        IQueryable<ReviewDetails> UpdateReviewRating(ReviewRating reviewRating);

        IQueryable<ReviewDetails> RestaurantReviewRating(ReviewRating reviewRating);
    }
}
