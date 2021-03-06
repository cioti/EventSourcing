using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcing.Sample.Aggregate
{
    public class SampleAggregate : AggregateBase
    {
        public SampleAggregate(Guid id)
        {
            AggregateId = id;
            AddEvent(new SampleAggregateCreatedEvent(AggregateId, AggregateVersion));
        }

        public string Property1 { get; set; }


        public void ChangeProperty(string property)
        {
            Property1 = property;
            AddEvent(new Property1ChangedEvent(AggregateId, AggregateVersion, property));
        }


        public void ApplyEvent(IDomainEvent @event)
        {
            switch (@event)
            {
                case Property1ChangedEvent:
                    Apply(@event as Property1ChangedEvent);
                    break;
                default:
                    break;
            }
        }

        public void Apply(Property1ChangedEvent @event)
        {
            Property1 = @event.Property1;
        }

        protected override void RegisterAppliers()
        {
            RegisterApplier<Property1ChangedEvent>(Apply);
        }
    }
}
