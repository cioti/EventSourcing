using System.Threading;
using System.Threading.Tasks;

namespace EventSourcing.Commands
{
    public interface ICommandBus
    {
        public Task SendAsync(ICommand command, CancellationToken cancellationToken = default);
    }
}
