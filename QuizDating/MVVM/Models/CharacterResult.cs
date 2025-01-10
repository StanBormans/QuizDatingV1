using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizDating.Models
{
    public class CharacterResult
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        [Column("Outgoing")]
        public int Outgoing {  get; set; }
        [Column("Social")]
        public int Social { get; set; }
        [Column("Opennes")]
        public int Opennes {  get; set; }
    }
}
