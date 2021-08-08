namespace EventSourcing
{
    public interface ISnapshotStrategy
    {
        bool ShouldTakeSnapshot(AggregateBase aggregate);
    }
}
