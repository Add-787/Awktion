using Awktion.Domain.Games;
using Awktion.Domain.Models;

namespace Awktion.Domain.Services;

public interface IGameService
{
    public void StartNewGame(int roomId, Settings settings);
    public void PlayerPicked(int roomId, Player player);
    protected void InitCallbacks(Game game);
    public void PutDown(int roomId, User user);
    public bool Bid(int roomId, User user, int amount);

}
