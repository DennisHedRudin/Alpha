using Data.Entities;

namespace Business.Interfaces
{
    public interface INotificationService
    {
        Task AddNotificationAsync(int notificationTypeId, string message, string image = null!, int notificationTargetGroup = 1);
        Task DismissNotificationAsync(string notificationId, string userId);
        Task<IEnumerable<NotificationEntity>> GetNotificationsAsync(string userId, int take = 10);
    }
}