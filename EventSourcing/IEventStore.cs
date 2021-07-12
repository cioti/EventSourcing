using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcing
{
    public interface IEventStore
    {
        Task<IEnumerable<IDomainEvent>> LoadEventsAsync(Guid aggregateId, CancellationToken cancellationToken = default);
        public Task<IEnumerable<IDomainEvent>> LoadEventsAsync(Guid aggregateId, long version, CancellationToken cancellationToken = default);
        public Task WriteEventsAsync(string aggregateName, IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
    }
}
