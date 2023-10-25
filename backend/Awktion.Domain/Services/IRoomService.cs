using Awktion.Domain.Models;
using Awktion.Domain.Rooms;

namespace Awktion.Domain.Services;

public interface IRoomService
{
    public void CreateNewRoom(Room newRoom);
    public void AddUserToRoom(int roomId, User user);
    public Room GetRoom(int roomId);


}
