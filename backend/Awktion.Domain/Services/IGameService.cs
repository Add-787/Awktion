using Awktion.Domain.Models;

namespace Awktion.Domain.Services;

public interface IGameService
{
    public void StartGame(int roomId);
    public void CreateNewGame(int roomId, GameSettings settings);
    public void PlayerPicked(int roomId, Player player);
}
