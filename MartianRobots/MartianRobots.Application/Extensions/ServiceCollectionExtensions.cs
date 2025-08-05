using MartianRobots.Abstractions.Factories;
using MartianRobots.Abstractions.Services;
using MartianRobots.Abstractions.Utils;
using MartianRobots.Application.Commands;
using MartianRobots.Application.Factories;
using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Mappers;
using MartianRobots.Application.Services;
using MartianRobots.Application.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace MartianRobots.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IDirectionMapper, DirectionMapper>()
            .AddSingleton<IInstructionValidator, InstructionValidator>()
            .AddSingleton<IDirectionUtils, DirectionUtils>()
            .AddTransient<IRoverMapper, RoverMapper>()
            .AddTransient<IRoverFactory, RoverFactory>()
            .AddScoped<IMarsRoverSimulator, MarsRoverSimulator>()
            .AddScoped<ICommandFactory, CommandFactory>();
        
        return services;
    }
}