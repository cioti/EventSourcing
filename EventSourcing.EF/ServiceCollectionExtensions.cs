using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EventSourcing.EF
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventSourcing(this IServiceCollection services, string connString, params Assembly[] assemblies)
        {
            services.AddCoreES(assemblies);
            services.AddScoped<ServiceFactory>(p => p.GetService);
            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<ISnapshotStore, SnapshotStore>();
            services.AddScoped<EventStoreRepository>();
            services.AddScoped<IEventStoreRepository, SnapshotStoreRepository>();
            services.AddDbContext<EventStoreDbContext>(opts
                => opts.UseSqlServer(connString, opts => opts.MigrationsAssembly(typeof(EventStoreDbContext).Assembly.FullName)));
            return services;
        }
    }
}
