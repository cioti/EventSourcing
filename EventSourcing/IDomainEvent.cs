using EventSourcing.Events;
using System;

namespace EventSourcing
{
    public interface IDomainEvent : IEvent
    {
        public Guid AggregateId { get; set; }
        public long AggregateVersion { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
