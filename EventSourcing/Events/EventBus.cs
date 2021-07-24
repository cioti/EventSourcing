using MediatR;
using System.Collections.Generic;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace EventSourcing.Events
{
    public class ParallelMediator : Mediator
    {
        public ParallelMediator(ServiceFactory  serviceFactory):base(serviceFactory)
        {
        }
        protected override Task PublishCore(IEnumerable<Func<INotification, CancellationToken, Task>> allHandlers, INotification notification, CancellationToken cancellationToken)
        {
            foreach (var handler in allHandlers)
            {
                Task.Run(() => handler(notification, cancellationToken));
            }

            return Task.CompletedTask;
        }
    }
    internal class EventBus : IEventBus
    {
        private readonly ParallelMediator _mediator;

        public EventBus(ParallelMediator mediator)
        {
            _mediator = mediator;
        }

        public Task PublishAsync(params IEvent[] events)
        {
            foreach (var @event in events)
            {
                _mediator.Publish(@event);
            }
            return Task.CompletedTask;
        }
    }
}
