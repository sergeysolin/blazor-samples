using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Samples.Shared;
using Samples.Shared.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<UserCart> ShoppingCarts { get; set; }

        public DbSet<ShoppingCartItem> CartItems { get; set; }

        public ApplicationDbContext(DbContextOptions options) 
            : base(options)
        {

        }
    }
}
