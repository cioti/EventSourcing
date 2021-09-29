using System;

namespace EventSourcing
{
    public interface IAggregateRoot
    {
        public Guid AggregateId { get; }
    }
}
