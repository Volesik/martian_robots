using MartianRobots.Application.Interfaces;
using MartianRobots.Common.Validators;
using MartianRobots.Domain.Entities;
using MartianRobots.Dto.Mappers;
using MartianRobots.Dto.Requests;
using MartianRobots.Dto.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MartianRobots.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoverController : ControllerBase
    {
        private readonly IMarsRoverSimulator _marsRoverSimulator;
        
        public RoverController(IMarsRoverSimulator marsRoverSimulator)
        {
            _marsRoverSimulator = marsRoverSimulator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<MissionResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<MissionResult>> RunMarsMission([FromBody] MissionRequest request)
        {
            try
            {
                InstructionValidator.ValidateCoordinates(request.PlateauSizeX, request.PlateauSizeY);
                
                var plateau = new Plateau(request.PlateauSizeX, request.PlateauSizeY);
                var results = new List<MissionResult>();

                foreach (var roverConfiguration in request.RoverConfigurations)
                {
                    InstructionValidator.ValidateCoordinates(roverConfiguration.InitialPositionX, roverConfiguration.InitialPositionY);
                    InstructionValidator.ValidateInstructionLength(roverConfiguration.Commands);
                    
                    var rover = RoverMapper.ToModel(roverConfiguration, plateau);
                    _marsRoverSimulator.ExecuteCommands(rover, roverConfiguration.Commands);
                    results.Add(RoverMapper.ToDto(rover));
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
