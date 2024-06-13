using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure;

public class Rate
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("flashcardId")]
    public Guid? FlashcardId { get; set; }
    [Column("userId")]
    public Guid? UserId { get; set; }
    [Column("rateValue")]
    public short? RateValue { get; set; }
    [Column("createdAt", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }
    [ForeignKey("FlashcardId")]
    [InverseProperty("Rates")]
    public virtual Flashcard? Flashcard { get; set; }
    [ForeignKey("UserId")]
    [InverseProperty("Rates")]
    public virtual User? User { get; set; }
}