using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public abstract class AggregateBase : IEventSourcingAggregate
    {
    
        private long _version;
        private readonly ICollection<IDomainEvent> _uncommitedEvents;
        private readonly Dictionary<Type, Action<IDomainEvent>> _eventAppliers;

        protected AggregateBase()
        {
            _version = InitialVersion;
            _uncommitedEvents = new List<IDomainEvent>();
            _eventAppliers = new();
            RegisterAppliers();
        }
        public const int InitialVersion = 0;
        public Guid Id { get; protected set; }
        public long AggregateVersion => _version;

        public void ClearUncommitedEvents() => _uncommitedEvents.Clear();
        public IEnumerable<IDomainEvent> GetUncomittedEvents() => _uncommitedEvents.AsEnumerable();
        public void Apply(IDomainEvent evt)
        {
            var evtType = evt.GetType();
            if (!this._eventAppliers.ContainsKey(evtType))
            {
                throw new AggregateEventApplierNotFound(this.GetType(),evtType);
            }
            this._eventAppliers[evtType](evt);
            _version++;
        }
        protected abstract void RegisterAppliers();
        protected void RegisterApplier<TEvent>(Action<TEvent> applier) where TEvent : IDomainEvent
        {
            _eventAppliers.Add(typeof(TEvent), (x) => applier((TEvent)x));
        }
    }
}
