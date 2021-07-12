using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcing
{
    public interface IEventStoreRepository
    {
        Task<TAggregate> LoadAggregateAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken = default) where TAggregate : IEventSourcingAggregate;
        Task<List<IDomainEvent>> SaveAsync<TAggregate>(TAggregate aggregate, CancellationToken cancellationToken = default) where TAggregate : IEventSourcingAggregate;
    }
}
