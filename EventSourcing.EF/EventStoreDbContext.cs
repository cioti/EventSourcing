using EventSourcing.Events;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing.EF
{
    internal class EventStoreDbContext : DbContext
    {
        public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Event> EventStore { get; set; }
        public DbSet<Snapshot> SnapshotStore { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var eventEntity = modelBuilder.Entity<Event>();
            eventEntity.HasKey(ev => new { ev.AggregateId, ev.AggregateVersion });
            eventEntity.Property(ev => ev.AggregateVersion).IsRequired();
            eventEntity.Property(ev => ev.EventType).IsRequired();
            eventEntity.Property(ev => ev.AggregateName).IsRequired();
            eventEntity.Property(ev => ev.Data).IsRequired();
            eventEntity.Property(ev => ev.DateCreated).IsRequired();
            eventEntity.Property(ev => ev.AggregateId).IsRequired();

            var snapshotEntity = modelBuilder.Entity<Snapshot>();
            snapshotEntity.HasKey(ev => ev.AggregateId);
            snapshotEntity.Property(ev => ev.AggregateVersion).IsRequired();
            snapshotEntity.Property(ev => ev.Data).IsRequired();
            snapshotEntity.Property(ev => ev.DateCreated).IsRequired();
            snapshotEntity.Property(ev => ev.AggregateId).IsRequired();
        }
    }
}
