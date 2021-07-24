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
        private readonly IEventStoreRepository _repository;

        public SampleAggregateCreatedEventHandler(IEventStoreRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(SampleAggregateCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("bla bla bla");
                var agg = await _repository.LoadAggregateAsync<SampleAggregate>(new Guid("3f6c1c8f-da88-4dd6-b410-7912db4d594f"));
                Console.WriteLine(agg.AggregateVersion);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            
        }
    }
}
