using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebbApp_Alpha.Hubs;

namespace WebbApp_Alpha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController(IHubContext<NotificationHub> notificationHub, INotificationService notificationsService) : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _notificationHub = notificationHub;
        private readonly INotificationService _notificationsService = notificationsService;
    }
}
