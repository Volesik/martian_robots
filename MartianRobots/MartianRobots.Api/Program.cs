using MartianRobots.Application.Commands;
using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IMarsRoverSimulator, MarsRoverSimulator>();
builder.Services.AddScoped<ICommandFactory, CommandFactory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
