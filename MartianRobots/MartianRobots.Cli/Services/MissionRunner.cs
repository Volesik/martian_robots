using MartianRobots.Abstractions.Domains;
using MartianRobots.Abstractions.Factories;
using MartianRobots.Abstractions.Services;
using MartianRobots.Abstractions.Utils;
using MartianRobots.Application.Interfaces;
using MartianRobots.Cli.Constants;
using MartianRobots.Common.Constants;
using MartianRobots.Common.Enums;
using MartianRobots.Domain.Entities;

namespace MartianRobots.Cli.Services;

public class MissionRunner
{
    private readonly IInstructionValidator _instructionValidator;
    private readonly IMarsRoverSimulator _marsRoverSimulator;
    private readonly IDirectionMapper _directionMapper;
    private readonly IRoverFactory _roverFactory;

    public MissionRunner(
        IInstructionValidator instructionValidator,
        IMarsRoverSimulator marsRoverSimulator,
        IDirectionMapper directionMapper,
        IRoverFactory roverFactory)
    {
        _instructionValidator = instructionValidator;
        _marsRoverSimulator = marsRoverSimulator;
        _directionMapper = directionMapper;
        _roverFactory = roverFactory;
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

                Console.Write("Enter command sequence (e.g. LFLFLFLFF): ");

                var commands = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(commands))
                {
                    Console.WriteLine("Commands cannot be empty.");
                    continue;
                }
                
                try
                {
                    _instructionValidator.ValidateInstructionLength(commands);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    continue;
                }

                try
                {
                    var rover = _roverFactory.Create(xCoordinate, yCoordinate, direction, plateau);
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

    private Plateau GetPlateau()
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
                _instructionValidator.ValidateCoordinates(maxPlateauSizeX, maxPlateauSizeY);
                plateau = new Plateau(maxPlateauSizeX, maxPlateauSizeY);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter two integers separated by space.");
            }
        }
        
        return plateau;
    }
    
    private bool TryReadRover(out int xCoordinate, out int yCoordinate, out Direction direction)
    {
        xCoordinate = CoordinateConstants.DefaultPosition;
        yCoordinate = CoordinateConstants.DefaultPosition;
        direction = default;

        while (true)
        {
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
                Console.WriteLine("Invalid format. Please enter: X Y D (e.g. 3 3 N)");
                continue;
            }

            try
            {
                direction = _directionMapper.CharToDirection(directionChar);
                _instructionValidator.ValidateCoordinates(xCoordinate, yCoordinate);

                return true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation error: {ex.Message}");
            }
        }
    }
    
    private void PrintRoverResult(IRover rover)
    {
        Console.WriteLine(rover + "\n");
    }
}