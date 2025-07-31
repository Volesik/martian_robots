using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Services;
using MartianRobots.Cli.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MartianRobots.Cli;

public static class Startup
{
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // App services
        services.AddScoped<IMarsRoverSimulator, MarsRoverSimulator>();
        services.AddScoped<MissionRunner>();

        return services.BuildServiceProvider();
    }
}