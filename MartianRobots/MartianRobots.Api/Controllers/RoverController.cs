using MartianRobot.Application.Interfaces;
using MartianRobots.Api.Mappers;
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
        private readonly IMarsRoverSimulator _marsRoverSimulator;
        
        public RoverController(IMarsRoverSimulator marsRoverSimulator)
        {
            _marsRoverSimulator = marsRoverSimulator;
        }

        [HttpPost]
        public ActionResult<IEnumerable<MissionResult>> RunMarsMission([FromBody] MissionRequest request)
        {
            var plateau = new Plateau(request.PlateauSizeX, request.PlateauSizeY);
            var results = new List<MissionResult>();

            foreach (var roverConfiguration in request.RoverConfigurations)
            {
                var rover = RoverMapper.ToModel(roverConfiguration, plateau);
                _marsRoverSimulator.ExecuteCommands(rover, roverConfiguration.Commands);
                results.Add(RoverMapper.ToDto(rover));
            }
            
            return Ok(results);
        }
    }
}
