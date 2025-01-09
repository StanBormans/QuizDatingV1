using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizDating.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
        public int ResultId { get; set; }
        public CharacterResult? CharacterResult { get; set; }
        public bool Finished { get; set; }
    }
}
