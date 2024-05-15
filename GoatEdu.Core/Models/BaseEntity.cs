using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models;

public class BaseEntity
{
    [Column("isDeleted")]
    public bool? IsDeleted { get; set; }
}