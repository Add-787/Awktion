using Awktion.Domain.Entities;
using Awktion.Domain.Entities.Games;
using Awktion.Domain.Entities.Users;

namespace Awktion.Domain.Services;

public interface IRoomService
{
    public void CreateRoom(Room newRoom);
    public void AddUser(int roomId, User user);
    public Room GetRoom(int roomId);
    public void GenerateSettings(int roomId, Settings settings);
    public void StartGameRoom(int roomId);

    public void DeleteRoom(int roomId);


}
