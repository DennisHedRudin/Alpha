using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<MemberEntity>(options)
{
    public virtual DbSet<MemberAddressEntity> MemberAddresses { get; set; }
    public virtual DbSet<ProjectEntity> Projects { get; set; }
    public virtual DbSet<StatusEntity> Statuses { get; set; }
    public virtual DbSet<ClientEntity> Clients { get; set; }
    public virtual DbSet<NotificationDissmissEntity> DissmissedNotifications { get; set; }
    public virtual DbSet<NotificationEntity> Notifications { get; set; }
    public virtual DbSet<NotificationTargetGroupsEntity> NotificationTargetsGroups { get; set; }
    public virtual DbSet<NotificationTypeEntity> NotificationTypes { get; set; }


}
