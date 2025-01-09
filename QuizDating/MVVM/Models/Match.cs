using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizDating.Models
{
    public class Match
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? UserMatch { get; set; }
        public bool WentOnDate { get; set; }
    }
}
