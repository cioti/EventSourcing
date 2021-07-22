using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcing.EF
{
    internal class SnapshotStore : ISnapshotStore
    {
        private readonly EventStoreDbContext _context;

        public SnapshotStore(EventStoreDbContext context)
        {
            _context = context;
        }

        public async Task<Snapshot> GetAsync(Guid aggregateId, CancellationToken cancellationToken = default)
        {
            return await _context.SnapshotStore.FindAsync(new object[] { aggregateId }, cancellationToken: cancellationToken);
        }

        public async Task SaveAsync(Snapshot snapshot, CancellationToken cancellationToken = default)
        {
            var snapshotExists = await _context.SnapshotStore.AnyAsync(ss => ss.AggregateId == snapshot.AggregateId, cancellationToken);
            if (snapshotExists)
            {
                _context.SnapshotStore.Update(snapshot);
            }
            else
            {
                _context.SnapshotStore.Add(snapshot);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
