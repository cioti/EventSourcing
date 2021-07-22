﻿using EventSourcing.Sample.Aggregate;
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

        [HttpGet("/save/{id}")]
        public async Task<IActionResult> Save(Guid id)
        {
            var agg = new SampleAggregate(id);
            agg.ChangeProperty("bla bla");
            var events = await _repo.SaveAsync(agg);
            return Ok(events);
        }

        [HttpGet("/get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var agg = await _repo.LoadAggregateAsync<SampleAggregate>(id);
            return Ok(agg);
        }
    }
}
