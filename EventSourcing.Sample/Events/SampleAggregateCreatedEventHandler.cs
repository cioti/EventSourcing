using EventSourcing.Events;
using EventSourcing.Sample.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcing.Sample.Events
{
    public class SampleAggregateCreatedEventHandler : IEventHandler<SampleAggregateCreatedEvent>
    {
        public Task Handle(SampleAggregateCreatedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("bla bla bla");
            return Task.CompletedTask;
        }
    }
}
