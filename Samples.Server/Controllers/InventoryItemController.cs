using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Samples.BusinessLogic.Data;

namespace Samples.Server.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoryItemController : BaseController<InventoryItem, Guid, Shared.InventoryItem>
    {
        public InventoryItemController(ApplicationDbContext dbContext, ILogger<InventoryItemController> logger, IMapper mapper)
            : base(dbContext, logger, mapper)
        {
        }
    }
}
