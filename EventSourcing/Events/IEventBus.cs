using System.Threading.Tasks;

namespace EventSourcing.Events
{
    public interface IEventBus
    {
        public Task PublishAsync(params IEvent[] events);
    }
}
