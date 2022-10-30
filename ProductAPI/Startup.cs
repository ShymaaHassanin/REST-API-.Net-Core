using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Product.Data.Core;
using Product.Data.Repositories.Implementation;
using Product.Domain.Handlers;
using Product.Domain.Interface;
using ProductAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Product.Domain.Model;
using System.Text;
using System.IO;
using AutoWrapper;

namespace ProductAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpContextAccessor();
            services.AddDbContext<DBContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DBConn")));
            services.AddDbContext<DBContext>(ServiceLifetime.Transient);
            services.AddScoped<IUnitOfWork, SqlUnitOfWork>();
            services.AddScoped<IQueryableUnitOfWork, SqlUnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<ICategoriesOp, CategoriesOp>();
            services.AddScoped<IProductsOp, ProductsOp>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => { builder.AllowAnyOrigin(); builder.AllowAnyMethod(); builder.AllowAnyHeader(); });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseApiResponseAndExceptionWrapper<MapResponseObject>(new AutoWrapperOptions { IsDebug= false, ShowStatusCode = true, UseApiProblemDetailsException= false });

            app.UseCors(options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });

            }
            //app.UseHttpsRedirection();

          

            app.UseRouting();

            app.UseAuthorization();
      
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
