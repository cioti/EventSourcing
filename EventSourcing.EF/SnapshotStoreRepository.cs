using EventSourcing.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcing.EF
{
    internal class SnapshotStoreRepository : IEventStoreRepository
    {
        private readonly ISnapshotStore _snapshotStore;
        private readonly IEventStore _eventStore;
        private readonly EventStoreRepository _eventStoreRepository;
        private readonly ISerializer _serializer;

        public SnapshotStoreRepository(ISnapshotStore snapshotStore,
            IEventStore eventStore,
            EventStoreRepository eventStoreRepository,
            ISerializer serializer)
        {
            _snapshotStore = snapshotStore;
            _eventStore = eventStore;
            _eventStoreRepository = eventStoreRepository;
            _serializer = serializer;
        }

        public async Task<TAggregate> LoadAggregateAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken = default) where TAggregate : AggregateBase
        {
            var aggregate = await RestoreAggregateFromSnapshot<TAggregate>(aggregateId, cancellationToken);
            if (aggregate == null)
            {
                return await _eventStoreRepository.LoadAggregateAsync<TAggregate>(aggregateId, cancellationToken);
            }


            var events = await _eventStore.LoadEventsAsync(aggregateId, aggregate.AggregateVersion, cancellationToken);

            foreach (var @event in events)
            {
                aggregate.Apply(@event);
            }
            return aggregate;
        }

        public async Task<List<IDomainEvent>> SaveAsync<TAggregate>(TAggregate aggregate, CancellationToken cancellationToken = default) where TAggregate : AggregateBase
        {

            // check snapshot strategy if we take snapshot
            var serializedAggregate = _serializer.Serialize(aggregate);
            var snapshot = new Snapshot(aggregate.Id, aggregate.AggregateVersion, Encoding.UTF8.GetBytes(serializedAggregate));

            await _snapshotStore.SaveAsync(snapshot, cancellationToken);

            return await _eventStoreRepository.SaveAsync(aggregate, cancellationToken);
        }

        private async Task<TAggregate> RestoreAggregateFromSnapshot<TAggregate>(Guid aggregateId, CancellationToken cancellationToken) where TAggregate : AggregateBase
        {
            var snapshot = await _snapshotStore.GetAsync(aggregateId, cancellationToken);
            if (snapshot == null)
            {
                return null;
            }
            var stringData = Encoding.UTF8.GetString(snapshot.Data);
            var aggregate = _serializer.Deserialize<TAggregate>(stringData, typeof(TAggregate));
            aggregate.Id = snapshot.AggregateId;
            aggregate.AggregateVersion = snapshot.AggregateVersion;

            return aggregate;
        }
    }
}
