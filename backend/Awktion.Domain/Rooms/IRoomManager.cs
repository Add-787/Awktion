using Awktion.Domain.Models;

namespace Awktion.Domain.Rooms;

public interface IRoomManager
{
    public void AddUser(int roomId, User user);
    public bool RemoveUser(int roomId, User user);
    public void DeleteRoom(int roomId);
    public void CloseRoom(int roomId);
    public List<User> GetUsers();
}
