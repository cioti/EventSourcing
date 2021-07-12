using System;

namespace EventSourcing
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException(Type agg) : base($"Aggregate {agg.Name} was not found.")
        {
        }
    }

    public class AggregateDefaultConstructorMissingException : Exception
    {
        public AggregateDefaultConstructorMissingException(Type agg) : base($"Aggregate {agg.Name} has no default constructor.")
        {
        }
    }

    public class AggregateUnorderedEventsException : Exception
    {
        public AggregateUnorderedEventsException(Guid aggregateId, Type agg) : base($"Aggregate {agg.Name} with id {aggregateId} has incorrect events order.")
        {
        }
    }

    public class AggregateInvalidEventVersion : Exception
    {
        public AggregateInvalidEventVersion(Guid aggregateId, Type agg) : base($"Aggregate {agg.Name} with id {aggregateId} has invalid event version.")
        {
        }
    }

    public class AggregateEventApplierNotFound : Exception
    {
        public AggregateEventApplierNotFound(Type agg,Type @event) : base($"Aggregate {agg.Name} has no registered event applier for event ${@event.Name}.")
        {
        }
    }
}
