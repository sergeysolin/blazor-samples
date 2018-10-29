using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Samples.BusinessLogic.Data;
using Samples.Shared;

namespace Samples.Server.Controllers
{
    public class BaseController<T, TId, TViewModel> : ControllerBase 
        where T: class
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;

        public BaseController(ApplicationDbContext dbContext, ILogger logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> Get()
        {
            return await Task.FromResult(
                Ok(
                    _dbContext.Set<T>().ToList()
                )
            );
                //.Skip((request.Page - 1) * request.PageSize)
                //.Take(request.PageSize)
                //.ToAsyncEnumerable();
        }

        [Route("{id}")]
        public async Task<IActionResult> Get(TId id, CancellationToken cancellationToken)
        {
            var item = await _dbContext.Set<T>()
                .FindAsync(id.ToString(), cancellationToken);

            return Ok(_mapper.Map<TViewModel>(item));
        }

        [Route("{id}")]
        public async Task<IActionResult> Post(TViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var result = await _dbContext.AddAsync(_mapper.Map<T>(model), cancellationToken);

                return Ok(_mapper.Map<TViewModel>(result));
            }

            return BadRequest();
        }
    }
}
