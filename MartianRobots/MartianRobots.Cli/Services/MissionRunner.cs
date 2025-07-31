using MartianRobots.Application.Interfaces;
using MartianRobots.Cli.Constants;
using MartianRobots.Common.Constants;
using MartianRobots.Common.Enums;
using MartianRobots.Domain.Entities;
using MartianRobots.Domain.Interfaces;
using MartianRobots.Dto.Mappers;

namespace MartianRobots.Cli.Services;

public class MissionRunner
{
    private readonly IMarsRoverSimulator _marsRoverSimulator;

    public MissionRunner(IMarsRoverSimulator marsRoverSimulator)
    {
        _marsRoverSimulator = marsRoverSimulator;
    }
    
    public void Run()
    {
        Console.WriteLine("=== Mars Rover Mission ===");

        try
        {
            var plateau = GetPlateau();

            Console.WriteLine("Enter rover instructions (empty line to exit).");

            while (true)
            {
                if (!TryReadRover(out var xCoordinate, out var yCoordinate, out var direction))
                {
                    break;
                }

                Console.Write("Enter command sequence (e.g. LMLMLMLMM): ");

                var commands = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(commands))
                {
                    Console.WriteLine("Commands cannot be empty.");
                    continue;
                }

                try
                {
                    var rover = new Rover(xCoordinate, yCoordinate, direction, plateau);
                    _marsRoverSimulator.ExecuteCommands(rover, commands);
                    PrintRoverResult(rover);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"[ERROR] Failed to execute rover commands: {exception.Message}");
                    throw;
                }
            }

            Console.WriteLine("Mission complete.");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"[FATAL ERROR] Mission aborted: {exception.Message}");
        }
    }

    private static Plateau GetPlateau()
    {
        Plateau? plateau = null;
        
        while (plateau == null)
        {
            Console.Write("Enter plateau upper-right coordinates (e.g. 5 3): ");
            var userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Plateau coordinates are required.");
                continue;
            }

            var plateauSize = userInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (plateauSize.Length == CliConstants.PlateauSize &&
                int.TryParse(plateauSize[0], out var maxPlateauSizeX) &&
                int.TryParse(plateauSize[1], out var maxPlateauSizeY))
            {
                plateau = new Plateau(maxPlateauSizeX, maxPlateauSizeY);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter two integers separated by space.");
            }
        }
        
        return plateau;
    }
    
    private static bool TryReadRover(out int xCoordinate, out int yCoordinate, out Direction direction)
    {
        xCoordinate = RoverConstants.DefaultPosition;
        yCoordinate = RoverConstants.DefaultPosition;
        direction = default;

        Console.Write("Enter rover position (e.g. 1 1 E): ");
        var input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Ending mission...");
            return false;
        }

        var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != CliConstants.CoordinateSize ||
            !int.TryParse(parts[0], out xCoordinate) ||
            !int.TryParse(parts[1], out yCoordinate) ||
            !char.TryParse(parts[2], out var directionChar))
        {
            Console.WriteLine("Invalid format. Please enter format: X Y D (e.g. 3 3 N)");
            return TryReadRover(out xCoordinate, out yCoordinate, out direction);
        }

        try
        {
            direction = DirectionMapper.CharToDirection(directionChar);
            return true;
        }
        catch
        {
            Console.WriteLine("Invalid direction character. Use N, E, S, or W.");
            return TryReadRover(out xCoordinate, out yCoordinate, out direction);
        }
    }
    
    private void PrintRoverResult(IRover rover)
    {
        Console.Write($"{rover.Position.X} {rover.Position.Y} {DirectionMapper.DirectionToChar(rover.CurrentDirection)}");
        
        if (rover.IsRoverLost)
        {
            Console.Write(" LOST");
        }
        
        Console.WriteLine("");
    }
}