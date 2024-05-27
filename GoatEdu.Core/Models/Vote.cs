using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure;

public class Vote
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("discussionId")]
    public Guid? DiscussionId { get; set; }
    [Column("answerId")]
    public Guid? AnswerId { get; set; }
    [Column("userId")]
    public Guid? UserId { get; set; }
    [Column("voteValue")]
    public short? VoteValue { get; set; }
    [Column("voteTimeStamp", TypeName = "timestamp without time zone")]
    public DateTime? VoteTimeStamp { get; set; }
    
    [ForeignKey("AnswerId")]
    [InverseProperty("Votes")]
    public virtual Answer? Answer { get; set; }
    [ForeignKey("DiscussionId")]
    [InverseProperty("Votes")]
    public virtual Discussion? Discussion { get; set; }
    [ForeignKey("UserId")]
    [InverseProperty("Votes")]
    public virtual User? User { get; set; }
}