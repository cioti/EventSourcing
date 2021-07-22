using System;

namespace EventSourcing
{
    public class Snapshot
    {
        public Snapshot(Guid aggregateId, long aggregateVersion, byte[] data)
        {
            if (aggregateId == Guid.Empty)
                throw new ArgumentException($"{nameof(AggregateId)} cannot be empty.", nameof(aggregateId));
            if (aggregateVersion < 0)
                throw new ArgumentException($"{nameof(Version)} cannot be less than 0.", nameof(aggregateVersion));
            if (data == null || data.Length == 0)
                throw new ArgumentException($"{nameof(Data)} cannot be empty.", nameof(data));

            AggregateId = aggregateId;
            AggregateVersion = aggregateVersion;
            Data = data;
            DateCreated = DateTime.UtcNow;
        }

        public Guid AggregateId { get; private set; }
        public long AggregateVersion { get; private set; }
        public byte[] Data { get; private set; }
        public DateTime DateCreated { get; private set; }
    }
}
