using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionExampleAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace AuctionExampleAPI
{
    public class Startup
    {
        #region Commands

        /*
         * Scaffold-DbContext "Host=localhost;Port=5432;Database=AuctionExample;Username=postgres;Password=dmitriy3452zz" Npgsql.EntityFrameworkCore.PostgreSQL
         * Add-Migration InitMigration -Context AuctionExampleContext
         */

        #endregion

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // use in-memory database
            services.AddDbContext<AuctionExampleContext>(options =>
                options.UseInMemoryDatabase("AuctionExample"));

            // use real database
            //services.AddDbContext<AuctionExampleContext>(c =>
            //    c.UseNpgsql(Configuration.GetConnectionString("AuctionExampleConnection")));

            services
                .AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuctionExample API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuctionExample API V1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
