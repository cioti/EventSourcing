using System;

namespace EventSourcing.Sample.Aggregate
{
    public class Property1ChangedEvent : IDomainEvent
    {
        public Property1ChangedEvent(Guid aggregateId,long aggregateVersion,string property1)
        {
            AggregateId = aggregateId;
            AggregateVersion = ++aggregateVersion;
            Property1 = property1;
        }
        public string Property1 { get; set; }
        public Guid AggregateId { get; set; }
        public long AggregateVersion { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
