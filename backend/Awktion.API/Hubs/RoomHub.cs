using Microsoft.AspNetCore.SignalR;

namespace Awktion.API.Hubs;

public class RoomHub : Hub<IRoomClient> 
{
    public RoomHub() {

    }
    public async Task RefreshClient() => await Clients.All.GetRooms();
}

public interface IRoomClient {
    Task GetRooms();
}