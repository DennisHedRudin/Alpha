using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class NotificationTargetGroupsEntity
{

    [Key]
    public int Id { get; set; }
    public string TargetGroup { get; set; } = null!;
    public ICollection<NotificationEntity> Notification { get; set; } = [];

}