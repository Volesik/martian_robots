namespace MartianRobots.Dto.Requests;

public class MissionRequest
{
    public int PlateauSizeX { get; set; }
    
    public int PlateauSizeY { get; set; }

    public List<RoverCommand> RoverConfigurations { get; set; } = [];
}