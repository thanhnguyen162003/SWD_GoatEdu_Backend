using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure;
[Table("Tag")]
public class Tag : BaseEntity
{
    public Tag()
    {
        Discussions = new HashSet<Discussion>();
        Flashcards = new HashSet<Flashcard>();
    }

    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("tagName", TypeName = "character varying")]
    public string? TagName { get; set; }
    [Column("createdAt", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }
    [Column("updatedAt", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }
    [InverseProperty("Tags")]
    public virtual ICollection<Discussion> Discussions { get; set; }
    [InverseProperty("Tags")]
    public virtual ICollection<Flashcard> Flashcards { get; set; }
}