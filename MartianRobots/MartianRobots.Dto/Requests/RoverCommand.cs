namespace MartianRobots.Dto.Requests;

public class RoverCommand
{
    public int InitialPositionX { get; set; }
    
    public int InitialPositionY { get; set; }
    
    public char InitialDirection { get; set; }
    
    public string Commands { get; set; } = string.Empty;
}