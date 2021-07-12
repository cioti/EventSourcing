﻿using EventSourcing.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing.EF
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventSourcing(this IServiceCollection services,string connString)
        {
            services.AddSingleton<ISerializer, JsonSerializer>();
            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddDbContext<EventStoreDbContext>(opts
                => opts.UseSqlServer(connString, opts => opts.MigrationsAssembly(typeof(EventStoreDbContext).Assembly.FullName)));
            return services;
        }
    }
}