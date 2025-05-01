using Microsoft.AspNetCore.SignalR;

namespace WebbApp_Alpha.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendNotificationToAll(object notification)
        {
            await Clients.All.SendAsync("AllRecevieNotification", notification);
        }

        public async Task SendNotificationToAdmin(object notification)
        {
            await Clients.All.SendAsync("AdminRecevieNotification", notification);
        }
    }
}
