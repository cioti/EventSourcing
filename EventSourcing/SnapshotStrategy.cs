using System;
using System.Linq;

namespace EventSourcing
{
    internal class SnapshotStrategy : ISnapshotStrategy
    {
        private readonly int _interval;

        public SnapshotStrategy(int interval)
        {
            if (interval < 1)
            {
                throw new ArgumentException($"{nameof(interval)} cannot be less than 1.");
            }
            _interval = interval;
        }

        public bool ShouldTakeSnapshot(AggregateBase aggregate)
        {
            var i = aggregate.AggregateVersion;
            for (var j = 0; j < aggregate.GetUncomittedEvents().Count(); j++)
                if (++i % _interval == 0 && i != 0)
                    return true;
            return false;
        }
    }
}
