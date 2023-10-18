namespace Awktion.Domain.Models;

public class Player {
    public required string Name { get; set; }
    public required Position Pos { get; set; }
    public required int BasePrice { get; set; }
    public required int Age { get; set; }
    public required string Country { get; set; }
}

public enum Position {
    FW,MID,DEF,GK
}
