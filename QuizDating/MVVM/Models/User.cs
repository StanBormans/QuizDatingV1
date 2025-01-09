using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizDating.Models
{
    [Table("person")]
    public class User
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Username")]
        public string? UserName { get; set; }
        [Column("Password")]
        public string? Password { get; set; }
        [Column("ProfolePicture")]
        public string? ProfilePicture {  get; set; }
        public List<Match> Matches { get; set; } = new List<Match>();
        public List<Quiz> Quizes { get; set; } = new List<Quiz>();
        [Column("CharacterResultId")]
        public int CharacterResultId { get; set; }
        public CharacterResult? CharacterResult { get; set; }
    }
}
