using MediatR;

namespace EventSourcing.Events
{
    public interface IEvent : INotification
    {
    }
}
