using Awktion.Domain.Common;

namespace Awktion.Domain.Entities.Players;

/// <summary>
/// Refers to the sports players that will
/// be auctioned in the game.
/// </summary>

public class Player : BaseEntity {
    public required string Name { get; init; }
    public required Position Pos { get; init; }
    public required int BasePrice { get; init; }
    public required int Age { get; init; }
    public required string Country { get; init; }
}

public enum Position {
    FW,MID,DEF,GK
}


