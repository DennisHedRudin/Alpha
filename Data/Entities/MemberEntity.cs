﻿using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class MemberEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string? FirstName { get; set; }

    [ProtectedPersonalData]
    public string? LastName { get; set; }

    [ProtectedPersonalData]
    public string? JobTitle { get; set; }

    public virtual MemberAddressEntity? Adress { get; set; }

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
    public ICollection<NotificationDissmissEntity> DissmissedNotifications { get; set; } = [];

}
