using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcing
{
    public abstract class AggregateBase : IEventSourcingAggregate
    {
        private readonly ICollection<IDomainEvent> _uncommitedEvents;
        private readonly Dictionary<Type, Action<IDomainEvent>> _eventAppliers;

        protected AggregateBase()
        {
            AggregateVersion = InitialVersion;
            _uncommitedEvents = new List<IDomainEvent>();
            _eventAppliers = new();
            RegisterAppliers();
        }
        public const int InitialVersion = 0;
        public Guid AggregateId { get; set; }
        public long AggregateVersion { get; set; }

        public void AddEvent(IDomainEvent @event) => _uncommitedEvents.Add(@event);
        public IEnumerable<IDomainEvent> GetUncomittedEvents()
        {
            lock (_uncommitedEvents)
            {
                return _uncommitedEvents.ToList();
            }
        }

        public IEnumerable<IDomainEvent> FlushUncomittedEvents()
        {
            lock (_uncommitedEvents)
            {
                var events = _uncommitedEvents.AsEnumerable().ToList();
                var i = 1;
                foreach (var @event in events)
                {
                    if (@event.AggregateId == Guid.Empty)
                    {
                        @event.AggregateId = AggregateId;
                    }
                    @event.AggregateVersion = AggregateVersion + i;
                    @event.DateCreated = DateTime.UtcNow;
                    i++;
                }
                AggregateVersion = AggregateVersion + _uncommitedEvents.Count;

                _uncommitedEvents.Clear();
                return events;
            }
        }
        public void Apply(IDomainEvent evt)
        {
            var evtType = evt.GetType();
            if (!this._eventAppliers.ContainsKey(evtType))
            {
                throw new AggregateEventApplierNotFound(this.GetType(), evtType);
            }
            this._eventAppliers[evtType](evt);
            AggregateId = evt.AggregateId;
            AggregateVersion++;
        }
        protected abstract void RegisterAppliers();
        protected void RegisterApplier<TEvent>(Action<TEvent> applier) where TEvent : IDomainEvent
        {
            _eventAppliers.Add(typeof(TEvent), (x) => applier((TEvent)x));
        }
    }
}
