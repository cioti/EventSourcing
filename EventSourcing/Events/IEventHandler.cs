using MediatR;

namespace EventSourcing.Events
{
    public interface IEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
    {
    }
}
