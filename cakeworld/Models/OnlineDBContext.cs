using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using cakeworld.Models;

namespace cakeworld.Models

    {
    public class OnlineDBContext : DbContext
    {
        public OnlineDBContext(DbContextOptions<OnlineDBContext> options)
        : base(options)

        {
        }


        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cake> Cakes { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<cakeworld.Models.Rating> Rating { get; set; }
    }
    }



