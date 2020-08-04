using System;

namespace MT.OnlineManagement.BusinessEntities
{
    public class ReviewDetails
    {
        public string RestaurantName { get; set; }
        public int RestaurantId { get; set; }
        public string Rating { get; set; }
        public string Review { get; set; }
        public int CustId { get; set; }
    }
}
