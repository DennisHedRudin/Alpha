using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class NotificationEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();


    [ForeignKey(nameof(TargetGroup))]  
    public string TargetGroupId { get; set; } = null!;
    public NotificationTargetGroupEntity TargetGroup { get; set; } = null!;


    [ForeignKey(nameof(NotificationType))]
    public string NotificationTypeId { get; set; } = null!;
    public NotificationTypeEntity NotificationType { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.Now;
}


public class NotificationTypeEntity
{

    [Key]
    public int Id { get; set; }
    public string NotificationType { get; set; } = null!;
    
}

public class NotificationTargetGroupEntity
{

    [Key]
    public int Id { get; set; }
    public string TargetGroup { get; set; } = null!;

}