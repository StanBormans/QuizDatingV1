using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizDating.Models;
using QuizDating.Data;

namespace QuizDating.Data
{
    public class LocalDbService
    {
        private readonly SQLiteConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteConnection(
                DataConstanst.DatabasePath,
                DataConstanst.flags
            );
            _connection.CreateTable<User>();
        }

        //User


        public async Task CreateUser(User user)
        {
            _connection.Insert(user);
        }

        //Quiz

        public async Task<List<Quiz>> GetQuiz()
        {
            return _connection.Table<Quiz>().ToList();
        }

        public async Task<Quiz> GetQuizById(int id)
        {
            return _connection.Table<Quiz>().Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task CreateQuiz(Quiz quiz)
        {
            _connection.Insert(quiz);
        }

        public async Task UpdateQuiz(Quiz quiz)
        {
            _connection.Update(quiz);
        }

        public async Task DeleteQuiz(Quiz quiz)
        {
            _connection.Delete(quiz);
        }
    }
}
