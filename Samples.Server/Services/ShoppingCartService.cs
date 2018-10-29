using Microsoft.EntityFrameworkCore;
using Samples.Server.Data;
using Samples.Shared.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.Server.Services
{
    public interface IShoppingCartService
    {
        Task<UserCart> GetUserCartAsync(string userId);
        Task<UserCart> SaveUserCartAsync(UserCart cart);
    }

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDbContext _dbContext;

        public ShoppingCartService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserCart> GetUserCartAsync(string userId)
        {
            var cart = await _dbContext.ShoppingCarts
                .Include(c => c.Items)
                .ToAsyncEnumerable().FirstOrDefault(c => c.UserId == userId);

            if(cart == null)
            {
                cart = await SaveUserCartAsync(new UserCart() { UserId = userId, CartId = Guid.NewGuid() });
            }

            return cart;
        }

        public async Task<UserCart> SaveUserCartAsync(UserCart cart)
        {
            var existing = await _dbContext.ShoppingCarts.ToAsyncEnumerable().FirstOrDefault(c => c.UserId == cart.UserId);

            if(existing == null)
            {
                if(cart.CartId == Guid.Empty)
                {
                    cart.CartId = Guid.NewGuid();
                }

                await _dbContext.AddAsync(cart);
            }
            else
            {
                existing.Items = cart.Items;
                existing.Expiration = cart.Expiration;
                await _dbContext.SaveChangesAsync();
            }

            return await GetUserCartAsync(cart.UserId);
        }

    }
}
