

using Awktion.Domain.Models;

namespace Awktion.API.Domain.Games;

public interface IGame
{
    public void StartGame(Settings settings);
    public void NewRound();

}
