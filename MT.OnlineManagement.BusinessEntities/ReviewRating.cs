using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineManagement.BusinessEntities
{
    public class ReviewRating
    {
        public int RestaurantID { get; set; }
        public string ReviewComments { get; set; }
        public string Rating { get; set; }
        public int CustomerId { get; set; }
    }
}
