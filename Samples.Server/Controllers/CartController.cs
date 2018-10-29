using Microsoft.AspNetCore.Mvc;
using Samples.Server.Services;
using Samples.Shared;
using Samples.Shared.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IShoppingCartService _cartService;

        public CartController(IShoppingCartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Get()
        {
            return Ok(await _cartService.GetUserCartAsync(User.GetUserId()));
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserCart cart)
        {
            cart.UserId = User.GetUserId();
            return Ok(await _cartService.SaveUserCartAsync(cart));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(Guid id,[FromBody] UserCart cart)
        {
            cart.UserId = User.GetUserId();
            return Ok(await _cartService.SaveUserCartAsync(cart));
        }
    }
}
