using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(Status), IsUnique = true)]
public class StatusEntity
{
    [Key]
    public int Id { get; set; }
    public string Status { get; set; } = null!;

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}
