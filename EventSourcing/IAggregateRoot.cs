using System;

namespace EventSourcing
{
    public interface IAggregateRoot
    {
        public Guid Id { get; }
    }
}
