using Microsoft.AspNetCore.SignalR;

namespace Awktion.API.Hubs;

public record Notification(string text, DateTime Date);

public class NotificationHub : Hub
{
    private readonly ILogger<NotificationHub> _logger;

    public NotificationHub(ILogger<NotificationHub> logger) {
        _logger = logger;
    }
    public Task NotifyAll(Notification notification) => Clients.All.SendAsync("NotificationReceived", notification);
    
}