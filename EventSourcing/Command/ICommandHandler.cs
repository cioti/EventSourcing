using MediatR;

namespace EventSourcing.Commands
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
    { }
}
