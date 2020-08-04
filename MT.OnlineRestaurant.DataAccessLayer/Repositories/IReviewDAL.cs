using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MT.OnlineRestaurant.DataAccessLayer.DataEntity;
using MT.OnlineRestaurant.DataAccessLayer.Entities;

namespace MT.OnlineRestaurant.DataAccessLayer.Repositories
{
    public interface IReviewDAL
    {
        IQueryable<TblRating> GetReviewRating(int restaurantID);
        IQueryable<TblRating> RestaurantReviewRating(ReviewRating reviewRating);
        IQueryable<TblRating> UpdateReviewRating(ReviewRating review);
    }
}
