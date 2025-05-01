using Business.Interfaces;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class NotificationService(DataContext dataContext) : INotificationService
{
    private readonly DataContext _dataContext = dataContext;

    public async Task AddNotificationAsync(int notificationTypeId, string message, string image = null!, int notificationTargetGroup = 1)
    {
        if (string.IsNullOrEmpty(image))
        {
            switch (notificationTypeId)
            {
                case 1:
                    image = "~/images/Avatars/10.svg";
                    break;

                case 2:
                    image = "~/images/project/project-template.svg";
                    break;
            }
        }

        var notificationEntity = new NotificationEntity
        {
            NotificationTargetGroupId = notificationTargetGroup,
            NotificationTypeId = notificationTypeId,
            Icon = image,
            Message = message,
        };

        _dataContext.Add(notificationEntity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<NotificationEntity>> GetNotificationsAsync(string userId, int take = 10)
    {
        var dismissedIds = await _dataContext.DissmissedNotifications
            .Where(x => x.UserId == userId)
            .Select(x => x.NotificationId)
            .ToListAsync();

        var notifications = await _dataContext.Notifications
            .Where(x => !dismissedIds.Contains(x.Id))
            .OrderByDescending(x => x.Created)
            .Take(take)
            .ToListAsync();

        return notifications;
    }

    public async Task DismissNotificationAsync(string notificationId, string userId)
    {
        var alreadyDismissed = await _dataContext.DissmissedNotifications.AnyAsync(x => x.NotificationId == notificationId && x.UserId == userId);
        if (!alreadyDismissed)
        {
            var dismissed = new NotificationDissmissEntity
            {
                NotificationId = notificationId,
                UserId = userId,
            };

            _dataContext.Add(dismissed);
            await _dataContext.SaveChangesAsync();
        }
    }
}
