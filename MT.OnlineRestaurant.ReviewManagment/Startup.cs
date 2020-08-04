using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MT.OnlineRestaurant.DataAccessLayer.Entities;
using MT.OnlineRestaurant.DataAccessLayer.Repositories;
using MT.OnlineRestaurant.BusinessLayer;
using MT.OnlineManagement.BusinessEntities;
using Microsoft.EntityFrameworkCore;
//using MT.OnlineRestaurant.ValidateUserHandler;
using MT.OnlineRestaurant.Logging;
using MT.OnlineRestaurant.ValidateUserToken;
using Swashbuckle.AspNetCore.Swagger;

namespace MT.OnlineRestaurant.ReviewManagment
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            _applicationPath = env.WebRootPath;
            _contentRootPath = env.ContentRootPath;
            // Setup configuration sources.

            var builder = new ConfigurationBuilder()
                .SetBasePath(_contentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                // builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        private static string _applicationPath = string.Empty;
        private static string _contentRootPath = string.Empty;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ReviewManager", Version = "v1" });
                c.OperationFilter<HeaderParameter>();//----Commented for Token Token-----
            });
            services.AddDbContext<ReviewManagementContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnectionString"),
             b => b.MigrationsAssembly("MT.OnlineRestaurant.DataAccessLayer")));

            services.AddTransient<IReviewBAL, ReviewBAL>();
            services.AddTransient<IReviewDAL, ReviewDAL>();

            services.AddMvc()
                    .AddMvcOptions(options =>
                    {
                        options.Filters.Add(new AuthorizeUser());//---commented for Token----
                        options.Filters.Add(new LoggingFilter(Configuration.GetConnectionString("DatabaseConnectionString")));
                        options.Filters.Add(new ErrorHandlingFilter(Configuration.GetConnectionString("DatabaseConnectionString")));

                        options.EnableEndpointRouting = false;
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "ReviewManager(v1.0)");
            });

            app.UseMvc();
        }
    }
}
