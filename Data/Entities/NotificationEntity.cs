using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class NotificationEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();


    [ForeignKey(nameof(TargetGroup))]  
    public int NotificationTargetGroupId { get; set; }
    public NotificationTargetGroupsEntity TargetGroup { get; set; } = null!;


    [ForeignKey(nameof(NotificationType))]
    public int NotificationTypeId { get; set; }
    public NotificationTypeEntity NotificationType { get; set; } = null!;

    public string Icon { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.Now;
    public ICollection<NotificationDissmissEntity> DissmissedNotificaitons { get; set; } = [];
}

