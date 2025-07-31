using MartianRobots.Cli.Services;
using MartianRobots.Cli;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = Startup.ConfigureServices();

var runner = serviceProvider.GetRequiredService<MissionRunner>();
runner.Run();