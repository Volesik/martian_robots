using MartianRobots.Application.Commands;
using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MartianRobots.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services
            .AddScoped<IMarsRoverSimulator, MarsRoverSimulator>()
            .AddScoped<ICommandFactory, CommandFactory>();
        
        return services;
    }
}