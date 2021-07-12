﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcing.EF
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly IEventStore _eventStore;

        public EventStoreRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<TAggregate> LoadAggregateAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken = default) where TAggregate : IEventSourcingAggregate
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

        public async Task<List<IDomainEvent>> SaveAsync<TAggregate>(TAggregate aggregate, CancellationToken cancellationToken = default) where TAggregate : IEventSourcingAggregate
        {
            var aggregateName = aggregate.GetType().Name.Replace("Aggregate", string.Empty);
            var events = aggregate.GetUncomittedEvents().ToList();

            for (int i = 0; i < events.Count; i++)
            {
                if (aggregate.AggregateVersion + 1 != events[i].AggregateVersion)
                    throw new AggregateInvalidEventVersion(aggregate.Id, typeof(TAggregate));
            }

            await _eventStore.WriteEventsAsync(aggregateName, events, cancellationToken);
            return events;
        }  
    }
}