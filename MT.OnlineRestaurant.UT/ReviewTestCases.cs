using NUnit.Framework;
using Moq;
using MT.OnlineManagement.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer;
using MT.OnlineRestaurant.DataLayer;
using System.Collections.Generic;
using System.Linq;
using MT.OnlineRestaurant.ReviewManagment.Controllers;
using Microsoft.AspNetCore.Mvc;
using MT.OnlineRestaurant.DataAccessLayer.Entities;
using MT.OnlineRestaurant.DataAccessLayer.DataEntity;
using System;
using Microsoft.EntityFrameworkCore;
using MT.OnlineRestaurant.DataAccessLayer.Repositories;
using ReviewRating = MT.OnlineRestaurant.DataAccessLayer.DataEntity.ReviewRating;

namespace MT.OnlineRestaurant.UT
{
    [TestFixture]
    public class ReviewManagementTests
    {
        [Test]
        public void Test_Get_RestaurantReviews()
        {
            //Arrange
            List<ReviewDetails> restaurantRatings = new List<ReviewDetails>();
            restaurantRatings.Add(new ReviewDetails()
            {
                RestaurantId = 1,
                CustId = 1,
                Rating = "2",
                Review = "",
            });
            var mockOrder = new Mock<IReviewBAL>();
            mockOrder.Setup(x => x.GetRestaurantRating(1)).Returns(restaurantRatings.AsQueryable());

            //Act
            var reviewController = new ReviewController(mockOrder.Object);
            var data = reviewController.GetRestaurantRating(1);
            var okObjectResult = data as OkObjectResult;

            //Assert
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual((okObjectResult.Value as IEnumerable<ReviewDetails>).Count(), restaurantRatings.Count());
        }

        //[Test]
        //public void Test_Add_ReviewRating()
        //{
        //    ReviewRating rating = new ReviewRating()
        //    {
        //        CustomerId = 1,
        //        RestaurantID = 10,
        //        Rating = "4",
        //        ReviewComments = "Nice",
        //    };

        //    MT.OnlineManagement.BusinessEntities.ReviewRating reviewRating = new OnlineManagement.BusinessEntities.ReviewRating
        //    {
        //        ReviewComments = rating.ReviewComments,
        //        CustomerId = rating.CustomerId,
        //        RestaurantID = rating.RestaurantID,
        //        Rating = rating.Rating

        //    };
        //    List<TblRating> tblRating = new List<TblRating>();
        //    var mockOrder = new Mock<IReviewDAL>();
        //    mockOrder.Setup(x => x.RestaurantReviewRating(rating)).Returns(tblRating.AsQueryable());
        //    var reviewBal = new ReviewBAL(mockOrder.Object);
        //    var data = reviewBal.RestaurantReviewRating(reviewRating);


        //    //  var options = new DbContextOptionsBuilder<ReviewManagementContext>().
        //    //      UseInMemoryDatabase(databaseName: "ReviewManagement")
        //    //.Options;

        //    //  ReviewDAL reviewDAL = new ReviewDAL(new ReviewManagementContext(options));
        //    //  IQueryable<TblRating> details = reviewDAL.UpdateReviewRating(rating);
        //    var okObjectResult = data as OkObjectResult;

        //    //Assert
        //    Assert.AreEqual(200, okObjectResult.StatusCode);
        //    Assert.IsNotNull(okObjectResult);
        //    Assert.Greater((okObjectResult.Value as IEnumerable<TblRating>).Count(), 0);
        //}
    }
}