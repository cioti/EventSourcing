using System;

namespace EventSourcing.Sample.Aggregate
{
    public class SampleAggregateCreatedEvent : IDomainEvent
    {
        public SampleAggregateCreatedEvent(Guid aggregateId, long aggregateVersion)
        {
            AggregateId = aggregateId;
            AggregateVersion = ++aggregateVersion;
        }
        public Guid AggregateId { get; set; }
        public long AggregateVersion { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
