using EventSourcing.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcing.EF
{
    internal class EventStore : IEventStore
    {
        private readonly EventStoreDbContext _context;
        private readonly ISerializer _serializer;

        public EventStore(EventStoreDbContext context, ISerializer serializer)
        {
            _context = context;
            _serializer = serializer;
        }

        public async Task<IEnumerable<IDomainEvent>> LoadEventsAsync(Guid aggregateId, long version, CancellationToken cancellationToken = default)
        {
            var events = await _context.Events
                .AsNoTracking()
                .Where(ev => ev.AggregateId == aggregateId && ev.Version > version)
                .ToListAsync(cancellationToken);

            return events.Select(ev => ev.Deserialize(_serializer)).AsEnumerable();
        }

        public async Task<IEnumerable<IDomainEvent>> LoadEventsAsync(Guid aggregateId, CancellationToken cancellationToken = default)
        {
            var events = await _context.Events
                .AsNoTracking()
                .Where(ev => ev.AggregateId == aggregateId)
                .ToListAsync(cancellationToken);

            return events.Select(ev => ev.Deserialize(_serializer)).AsEnumerable();
        }

        public async Task WriteEventsAsync(string aggregateName, IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            var events = domainEvents.Select(de => de.Serialize(aggregateName, _serializer));

            await _context.Events.AddRangeAsync(events, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
