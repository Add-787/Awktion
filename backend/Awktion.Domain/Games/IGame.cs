

using Awktion.Domain.Models;

namespace Awktion.API.Domain.Games;

public interface IGame
{
    public void StartGame(GameSettings settings);
    public void NewRound();

}
