using EventSourcing.Commands;
using EventSourcing.Sample.Aggregate;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcing.Sample.Commands
{
    public class CreateSampleCommand : ICommand
    {
        public CreateSampleCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    internal class CreateSampleCommandHandler : ICommandHandler<CreateSampleCommand>
    {
        private readonly IEventStoreRepository _repository;

        public CreateSampleCommandHandler(IEventStoreRepository repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(CreateSampleCommand request, CancellationToken cancellationToken)
        {
            var agg = new SampleAggregate(request.Id);
            await _repository.SaveAsync(agg);
            return Unit.Value;
        }
    }
}
