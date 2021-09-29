using System;

namespace EventSourcing
{
    public abstract class DomainEvent : IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public long AggregateVersion { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
