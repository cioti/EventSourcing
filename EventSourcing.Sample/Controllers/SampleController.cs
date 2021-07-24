using EventSourcing.Commands;
using EventSourcing.Sample.Aggregate;
using EventSourcing.Sample.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcing.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IEventStoreRepository _repo;

        public SampleController(ICommandBus commandBus,IEventStoreRepository repo)
        {
            _commandBus = commandBus;
            _repo = repo;
        }

        [HttpGet("/save/{id}")]
        public async Task<IActionResult> Save(Guid id)
        {
            await _commandBus.SendAsync(new CreateSampleCommand(id));
            return Ok();
        }

        [HttpGet("/get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var agg = await _repo.LoadAggregateAsync<SampleAggregate>(id);
            return Ok(agg);
        }
    }
}
