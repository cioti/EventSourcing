using System.Collections.Generic;

namespace EventSourcing
{
    public interface IEventSourcingAggregate : IAggregateRoot
    {
        long AggregateVersion { get; set; }
        void Apply(IDomainEvent @event);
        IEnumerable<IDomainEvent> GetUncomittedEvents();
    }
}
