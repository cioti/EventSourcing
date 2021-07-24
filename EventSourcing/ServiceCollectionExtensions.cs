using EventSourcing.Commands;
using EventSourcing.Events;
using EventSourcing.Utility;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EventSourcing
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreES(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<ISerializer, JsonSerializer>();
            services.AddScoped<ServiceFactory>(p => p.GetService);
            services.AddScoped<ParallelMediator>();
            services.AddScoped<IEventBus, EventBus>();
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddMediatR(assemblies);
            return services;
        }
    }
}
