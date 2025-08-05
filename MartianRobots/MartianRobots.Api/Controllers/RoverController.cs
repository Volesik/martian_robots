using MartianRobots.Abstractions.Services;
using MartianRobots.Abstractions.Utils;
using MartianRobots.Application.Interfaces;
using MartianRobots.Domain.Entities;
using MartianRobots.Dto.Requests;
using MartianRobots.Dto.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MartianRobots.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoverController : ControllerBase
    {
        private readonly IInstructionValidator _instructionValidator;
        private readonly IMarsRoverSimulator _marsRoverSimulator;
        private readonly IRoverMapper _roverMapper;
        
        public RoverController(
            IInstructionValidator instructionValidator,
            IMarsRoverSimulator marsRoverSimulator,
            IRoverMapper roverMapper)
        {
            _marsRoverSimulator = marsRoverSimulator;
            _roverMapper = roverMapper;
            _instructionValidator = instructionValidator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<MissionResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<MissionResult>> RunMarsMission([FromBody] MissionRequest request)
        {
            try
            {
                _instructionValidator.ValidateCoordinates(request.PlateauSizeX, request.PlateauSizeY);
                
                var plateau = new Plateau(request.PlateauSizeX, request.PlateauSizeY);
                var results = new List<MissionResult>();

                foreach (var roverConfiguration in request.RoverConfigurations)
                {
                    _instructionValidator.ValidateCoordinates(roverConfiguration.InitialPositionX, roverConfiguration.InitialPositionY);
                    _instructionValidator.ValidateInstructionLength(roverConfiguration.Commands);
                    
                    var rover = _roverMapper.ToModel(roverConfiguration, plateau);
                    _marsRoverSimulator.ExecuteCommands(rover, roverConfiguration.Commands);
                    results.Add(_roverMapper.ToDto(rover));
                }
            
                return Ok(results);
            }
            catch (ArgumentException exception)
            {
                return BadRequest($"Invalid mission input: {exception.Message}");
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Unexpected server error: {exception.Message}");
            }
        }
    }
}
