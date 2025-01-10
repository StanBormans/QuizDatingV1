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
        public string? EffectA { get; set; }

        [Column("EffectB")]
        public string? EffectB { get; set; }

        [Column("ResultAIncrement")]
        public int ResultAIncrement { get; set; }

        [Column("ResultBIncrement")]
        public int ResultBIncrement { get; set; }

        [Ignore]
        public Button OptionAButton { get; set; }

        [Ignore]
        public Button OptionBButton { get; set; }

        [Column("QuizId")]
        public int QuizId { get; set; }
    }
}
