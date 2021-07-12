using System;

namespace EventSourcing
{
    public interface IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public long AggregateVersion { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
