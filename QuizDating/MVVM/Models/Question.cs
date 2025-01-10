using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizDating.Models
{
    public class Question
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Title")]
        public string? Title { get; set; }

        [Column("Description")]
        public string? Description { get; set; }

        [Column("OptionA")]
        public string? OptionA { get; set; }

        [Column("OptionB")]
        public string? OptionB { get; set; }

        [Column("EffectA")]
        public string? EffectA { get; set; } // Effect of Option A (e.g., Outgoing, Social, Openness)

        [Column("EffectB")]
        public string? EffectB { get; set; } // Effect of Option B (e.g., Outgoing, Social, Openness)

        [Column("ResultAIncrement")]
        public int ResultAIncrement { get; set; } // Increment value for Option A

        [Column("ResultBIncrement")]
        public int ResultBIncrement { get; set; } // Increment value for Option B

        [Ignore]
        public Button OptionAButton { get; set; } // UI-specific property, ignored by the database

        [Ignore]
        public Button OptionBButton { get; set; } // UI-specific property, ignored by the database

        [Column("QuizId")]
        public int QuizId { get; set; } // Foreign key linking to the parent Quiz
    }
}
