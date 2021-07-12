using EventSourcing.Sample.Aggregate;
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
        private readonly IEventStoreRepository _repo;

        public SampleController(IEventStoreRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("/save")]
        public async Task<IActionResult> Save()
        {
            var agg = new SampleAggregate();
            agg.ChangeProperty("bla bla");
            var events = await _repo.SaveAsync(agg);
            return Ok(events);
        }

        [HttpGet("/get")]
        public async Task<IActionResult> Get()
        {
            var agg = await _repo.LoadAggregateAsync<SampleAggregate>(Guid.Parse("AEC9EAA0-F361-4640-8D76-5E8FE66E1CF9"));
            return Ok(agg);
        }
    }
}
