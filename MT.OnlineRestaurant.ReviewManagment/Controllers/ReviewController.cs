using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MT.OnlineManagement.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer;
using MT.OnlineRestaurant.DataAccessLayer.Entities;
using MT.OnlineRestaurant.DataAccessLayer.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MT.OnlineRestaurant.ReviewManagment.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class ReviewController : Controller
    {
        private readonly IReviewBAL bal_Repo;
        public ReviewController(IReviewBAL _bal_Repo)
        {
            bal_Repo = _bal_Repo;
        }
        [HttpGet]
        [Route("RestaurantRating")]
        public IActionResult GetRestaurantRating([FromQuery] int RestaurantID)
        {
            IQueryable<ReviewDetails> reviews;
            reviews = bal_Repo.GetRestaurantRating(RestaurantID);
            if (reviews != null)
            {
                return this.Ok(reviews);
            }

            return this.StatusCode((int)HttpStatusCode.InternalServerError, string.Empty);
        }

        [HttpPut]
        [Route("UpdateRestaurantRating")]
        public IActionResult UpdateRestaurantRating([FromQuery] int RestaurantID, ReviewRating reviewRating)
        {
            IQueryable<ReviewDetails> reviews;
            reviews = bal_Repo.UpdateReviewRating(reviewRating);
            if (reviews != null)
            {
                return this.Ok(reviews);
            }

            return this.StatusCode((int)HttpStatusCode.InternalServerError, string.Empty);
        }

        [HttpPost]
        [Route("RestaurantReviewRating")]
        public IActionResult RestaurantReviewRating([FromQuery]ReviewRating reviewRating)
        {
            IQueryable<ReviewDetails> reviews;
            reviews = bal_Repo.RestaurantReviewRating(reviewRating);
            if (reviews != null)
            {
                return this.Ok(reviews);
            }

            return this.StatusCode((int)HttpStatusCode.InternalServerError, string.Empty);
        }
    }
}
