using EventSourcing.Utility;
using System;
using System.Text;

namespace EventSourcing.EF
{
    public static class Extensions
    {
        internal static IDomainEvent Deserialize(this Event @event, ISerializer serializer)
        {
            var eventData = Encoding.UTF8.GetString(@event.Data);
            var data = serializer.Deserialize<IDomainEvent>(eventData, Type.GetType(@event.EventType));

            data.AggregateId = @event.AggregateId;
            data.AggregateVersion = @event.Version;
            data.DateCreated = @event.DateCreated;

            return data;
        }

        internal static Event Serialize(this IDomainEvent domainEvent, string aggregateName, ISerializer serializer)
        {
            var eventData = serializer.Serialize(domainEvent,
                new[]
                {
                    nameof(domainEvent.AggregateId),
                    nameof(domainEvent.AggregateVersion),
                    nameof(domainEvent.DateCreated)}
                );
            var eventType = domainEvent.GetType().AssemblyQualifiedName;

            return new Event(domainEvent.AggregateId,
                domainEvent.AggregateVersion,
                aggregateName,
                eventType,
                Encoding.UTF8.GetBytes(eventData));
        }
    }
}
