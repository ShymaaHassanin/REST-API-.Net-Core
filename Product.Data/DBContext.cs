using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Domain.Model;

namespace ProductAPI.Data
{
    public class DBContext : DbContext
    {
        public static IConfiguration Configuration { get; private set; }
        //  public readonly ILoggerFactory MyLoggerFactory;
        public DBContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
            //   MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        }

        DbSet<Product.Domain.Model.Product> Products { get; set; }

        DbSet<Category> Categorys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration["DBConn"]);
            }
            //optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }

        
    }
}
