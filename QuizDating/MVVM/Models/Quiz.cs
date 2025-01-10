using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizDating.Models
{
    public class Quiz
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string? Name { get; set; }
        [Ignore]
        public List<Question> Questions { get; set; } = new List<Question>();
        [Column("CharacterResultId")]
        public int CharacterResultId { get; set; }
        [Ignore]
        public CharacterResult? CharacterResult { get; set; }
        [Column("Finished")]
        public bool Finished { get; set; }
    }
}
