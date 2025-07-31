using MartianRobots.Application.Extensions;
using MartianRobots.Cli.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MartianRobots.Cli;

public static class Startup
{
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddAppServices();
        services.AddScoped<MissionRunner>();

        return services.BuildServiceProvider();
    }
}