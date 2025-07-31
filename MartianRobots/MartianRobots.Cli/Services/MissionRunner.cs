using MartianRobots.Application.Interfaces;
using MartianRobots.Common.Enums;
using MartianRobots.Domain.Entities;
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
        
        Plateau? plateau = null;
        while (plateau == null)
        {
            Console.Write("Enter plateau upper-right coordinates (e.g. 5 3): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Plateau coordinates are required.");
                continue;
            }

            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2 || !int.TryParse(parts[0], out var maxX) || !int.TryParse(parts[1], out var maxY))
            {
                Console.WriteLine("Invalid input. Please enter two integers separated by space.");
                continue;
            }

            plateau = new Plateau(maxX, maxY);
        }

        Console.WriteLine("Enter rover instructions (empty line to exit).");
        
        while (true)
        {
            Console.Write("Enter rover position (e.g. 1 1 E): ");
            var positionInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(positionInput))
            {
                Console.WriteLine("Ending mission...");
                break;
            }

            var posParts = positionInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (posParts.Length != 3 ||
                !int.TryParse(posParts[0], out var x) ||
                !int.TryParse(posParts[1], out var y) ||
                !char.TryParse(posParts[2], out var dirChar))
            {
                Console.WriteLine("Invalid rover position. Please enter format: X Y D (e.g. 3 3 N)");
                continue;
            }

            Direction direction;
            try
            {
                direction = DirectionMapper.CharToDirection(dirChar);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid direction character. Use N, E, S, or W.");
                continue;
            }

            Console.Write("Enter command sequence (e.g. LMLMLMLMM): ");
            var commands = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(commands))
            {
                Console.WriteLine("Commands cannot be empty.");
                continue;
            }
            
            var rover = new Rover(x, y, direction, plateau);
            _marsRoverSimulator.ExecuteCommands(rover, commands);

            Console.Write($"{rover.Position.X} {rover.Position.Y} {DirectionMapper.DirectionToChar(rover.CurrentDirection)}");
            if (rover.IsRoverLost)
                Console.Write(" LOST");
            Console.WriteLine();
        }

        Console.WriteLine("Mission complete.");
    }
}