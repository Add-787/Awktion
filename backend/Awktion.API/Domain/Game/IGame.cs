using Awktion.API.Domain.Game.Models;

namespace Awktion.API.Domain.Game;

public interface IGame
{
    public void StartGame(GameSettings settings);
    public void NewRound();




}
