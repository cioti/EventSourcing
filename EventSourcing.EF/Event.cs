using System;

namespace EventSourcing.EF
{
    internal class Event
    {
        private Event() { }
        public Event(Guid aggregateId, long version,string aggregateName, string eventType, byte[] data)
        {
            if (aggregateId == Guid.Empty)
                throw new ArgumentException($"{nameof(AggregateId)} cannot be empty.", nameof(aggregateId));
            if (version < 1)
                throw new ArgumentException($"{nameof(Version)} cannot be less than 1.", nameof(version));
            if (string.IsNullOrWhiteSpace(eventType))
                throw new ArgumentException($"{nameof(EventType)} cannot be empty.", nameof(eventType));
            if (string.IsNullOrWhiteSpace(aggregateName))
                throw new ArgumentException($"{nameof(AggregateName)} cannot be empty.", nameof(aggregateName));
            if (data == null || data.Length == 0)
                throw new ArgumentException($"{nameof(Data)} cannot be empty.", nameof(data));

            AggregateId = aggregateId;
            AggregateName = aggregateName;
            Version = version;
            EventType = eventType;
            Data = data;
        }

        public Guid AggregateId { get; private set; }
        public string AggregateName { get; private set; }
        public long Version { get; private set; }
        public string EventType { get; private set; }
        public byte[] Data { get; private set; }
        public DateTime DateCreated { get; private set; }
    }
}
