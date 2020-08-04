using MT.OnlineRestaurant.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MT.OnlineRestaurant.DataAccessLayer.DataEntity;

namespace MT.OnlineRestaurant.DataAccessLayer.Repositories
{
    public class ReviewDAL : IReviewDAL
    {
        private readonly ReviewManagementContext dbContext;
        public ReviewDAL(ReviewManagementContext connection)
        {
            dbContext = connection;
        }

        public IQueryable<TblRating> GetReviewRating(int restaurantID)
        {
            try
            {
                if (dbContext != null)
                {
                    return (from rating in dbContext.TblRating
                            join restaurant in dbContext.TblRestaurant on
                            rating.TblRestaurantId equals restaurant.Id 
                            where rating.TblRestaurantId == restaurantID
                            select new TblRating
                            {
                                Rating = rating.Rating,
                                Comments = rating.Comments,
                                TblRestaurantId = restaurant.Id,
                                TblRestaurant = restaurant,
                                TblCustomerId = rating.TblCustomerId
                            }).AsQueryable();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TblRating> UpdateReviewRating(ReviewRating reviewRating)
        {
            try
            {
                if (dbContext != null)
                {
                    var updateRecord = from rating in dbContext.TblRating
                                 where rating.TblRestaurantId == reviewRating.RestaurantID
                                 select rating;
                    foreach(var items in updateRecord)
                    {
                        if(items.TblRestaurantId == reviewRating.RestaurantID &&
                            items.TblCustomerId == reviewRating.CustomerId)
                        {
                            items.Rating = reviewRating.Rating;
                            items.Comments = reviewRating.ReviewComments;
                            items.RecordTimeStampCreated = DateTime.Now;
                        }
                    }
                    dbContext.SaveChanges();
                    return (from rating in dbContext.TblRating
                            join restaurant in dbContext.TblRestaurant on
                            rating.TblRestaurantId equals restaurant.Id
                            where rating.TblRestaurantId == reviewRating.RestaurantID && rating.TblCustomerId == reviewRating.CustomerId
                            select new TblRating
                            {
                                Rating = rating.Rating,
                                Comments = rating.Comments,
                                TblRestaurantId = restaurant.Id,
                                TblRestaurant = restaurant,
                                TblCustomerId = rating.TblCustomerId
                            }).AsQueryable();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TblRating> RestaurantReviewRating(ReviewRating reviewRating)
        {
            try
            {
                if (dbContext != null)
                {
                    TblRating tblRating = new TblRating
                    {
                        Rating = reviewRating.Rating,
                        Comments = reviewRating.ReviewComments,
                        TblRestaurantId = reviewRating.RestaurantID,
                        TblCustomerId = reviewRating.CustomerId,
                        RecordTimeStamp = DateTime.Now,
                        RecordTimeStampCreated = DateTime.Now
                    };
                    dbContext.Set<TblRating>().Add(tblRating);
                    dbContext.SaveChanges();
                    return (from rating in dbContext.TblRating
                            join restaurant in dbContext.TblRestaurant on
                            rating.TblRestaurantId equals restaurant.Id
                            where rating.TblRestaurantId == reviewRating.RestaurantID && rating.TblCustomerId == reviewRating.CustomerId
                            select new TblRating
                            {
                                Rating = rating.Rating,
                                Comments = rating.Comments,
                                TblRestaurantId = restaurant.Id,
                                TblRestaurant = restaurant,
                                TblCustomerId = rating.TblCustomerId
                            }).AsQueryable();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
