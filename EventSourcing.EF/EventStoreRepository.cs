using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcing.EF
{
    internal class EventStoreRepository : IEventStoreRepository
    {
        private readonly IEventStore _eventStore;

        public EventStoreRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<TAggregate> LoadAggregateAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken = default) where TAggregate : AggregateBase
        {
            var events = await _eventStore.LoadEventsAsync(aggregateId, cancellationToken);
            if (!events.Any())
            {
                throw new AggregateNotFoundException(typeof(TAggregate));
            }

            var aggregate = AggregateFactory<TAggregate>.CreateAggregate();

            foreach (var @event in events)
            {
                if (@event.AggregateVersion != aggregate.AggregateVersion + 1)
                {
                    throw new AggregateUnorderedEventsException(aggregateId, typeof(TAggregate));
                }
                aggregate.Apply(@event);
            }

            return aggregate;
        }

        public async Task<List<IDomainEvent>> SaveAsync<TAggregate>(TAggregate aggregate, CancellationToken cancellationToken = default) where TAggregate : AggregateBase
        {
            var aggregateName = aggregate.GetType().Name.Replace("Aggregate", string.Empty);
            var events = aggregate.FlushUncomittedEvents().ToList();

            await _eventStore.WriteEventsAsync(aggregateName, events, cancellationToken);
            return events;
        }  
    }
}
