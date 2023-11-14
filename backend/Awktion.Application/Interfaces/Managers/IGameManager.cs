using Awktion.Application.Dtos.Games;

namespace Awktion.Application.Interfaces.Managers;

public interface IGameManager
{
    public Task<CreateGameResponse> CreateGame(CreateGameRequest request);
    public Task<bool> StartGame(int roomId);
    
}
