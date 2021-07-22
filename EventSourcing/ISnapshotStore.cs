using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcing
{
    public interface ISnapshotStore
    {
        Task<Snapshot> GetAsync(Guid aggregateId, CancellationToken cancellationToken = default);
        Task SaveAsync(Snapshot snapshot, CancellationToken cancellationToken = default);
    }
}
