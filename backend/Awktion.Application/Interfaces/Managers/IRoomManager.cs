using Awktion.Domain.Entities;

namespace Awktion.Application.Interfaces.Managers;

public interface IRoomManager
{
    public Task<bool> AddUserToRoom(int roomId, User user);
    public Task<bool> RemoveUserFromRoom(int roomId, User user);
}
