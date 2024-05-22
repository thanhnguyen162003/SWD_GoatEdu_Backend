using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Vote
    {
        public int Id { get; set; }
        public Guid Discussionid { get; set; }
        public Guid? Answerid { get; set; }
        public Guid Userid { get; set; }
        public short Votevalue { get; set; }
        public DateTime? Votetimestamp { get; set; }

        public virtual Answer? Answer { get; set; }
        public virtual Discussion Discussion { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
