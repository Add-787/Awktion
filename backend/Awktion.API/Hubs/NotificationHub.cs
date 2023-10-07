using Microsoft.AspNetCore.SignalR;

namespace Awktion.API.Hubs;

public record Notification(string text, DateTime Date);

public class NotificationHub : Hub
{
    public Task NotifyAll(Notification notification) => Clients.All.SendAsync("NotificationReceived", notification);
    
}