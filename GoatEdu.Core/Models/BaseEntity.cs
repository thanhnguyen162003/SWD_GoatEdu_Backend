using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure;

public class BaseEntity
{
    [Column("isDeleted")]
    public bool? IsDeleted { get; set; }

}