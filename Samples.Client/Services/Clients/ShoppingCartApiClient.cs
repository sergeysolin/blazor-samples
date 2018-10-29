using Samples.Shared;
using Samples.Shared.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.Client.Services.Clients
{
    public class ShoppingCartApiClient : ApiClient<UserCart>
    {
        public ShoppingCartApiClient(IServiceProvider serviceProvider) 
            : base(serviceProvider, "/api/cart")
        {
        }
    }
}
