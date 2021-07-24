using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcing.Commands
{
    internal class CommandBus : ICommandBus
    {
        private readonly IMediator _mediator;

        public CommandBus(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task SendAsync(ICommand command, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(command, cancellationToken);
        }
    }
}
