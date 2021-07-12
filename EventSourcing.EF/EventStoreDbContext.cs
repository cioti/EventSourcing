using Microsoft.EntityFrameworkCore;

namespace EventSourcing.EF
{
    internal class EventStoreDbContext : DbContext
    {
        public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Event>();
            entity.HasKey(ev => new { ev.AggregateId, ev.Version });
            entity.Property(ev => ev.Version).IsRequired();
            entity.Property(ev => ev.Data).IsRequired();
            entity.Property(ev => ev.DateCreated).IsRequired();
            entity.Property(ev => ev.AggregateId).IsRequired();
        }
    }
}
