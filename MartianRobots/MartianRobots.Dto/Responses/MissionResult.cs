namespace MartianRobots.Dto.Responses;

public class MissionResult
{
    public int FinalPositionX { get; set; }
    
    public int FinalPositionY { get; set; }
    
    public char FinalDirection { get; set; }
    
    public bool IsRoverLost { get; set; }
}