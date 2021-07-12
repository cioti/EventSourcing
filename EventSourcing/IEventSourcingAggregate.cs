using System.Collections.Generic;

namespace EventSourcing
{
    public interface IEventSourcingAggregate : IAggregateRoot
    {
        long AggregateVersion { get;}
        void Apply(IDomainEvent @event);
        IEnumerable<IDomainEvent> GetUncomittedEvents();
        void ClearUncommitedEvents();
    }
}
