using Microsoft.EntityFrameworkCore;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.DataAccessLayer.Entities
{
    public partial class ReviewManagementContext : DbContext
    {

        //private readonly string DbConnectionString;
        public ReviewManagementContext()
        {
        }
        public ReviewManagementContext(DbContextOptions<ReviewManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblRating> TblRating { get; set; }
        public virtual DbSet<TblRestaurant> TblRestaurant { get; set; }
        public virtual DbSet<TblRestaurantDetails> TblRestaurantDetails { get; set; }
    }
}
