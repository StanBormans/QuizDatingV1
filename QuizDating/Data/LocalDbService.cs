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
            _connection.CreateTable<Quiz>();
            
            _connection.CreateTable<CharacterResult>();
            _connection.CreateTable<Question>();
        }

        //User


        public async Task CreateUser(User user)
        {
            _connection.Insert(user);
        }

        //Quiz

        public async Task CreateQuiz(Quiz quiz)
        {
            _connection.Insert(quiz);
            foreach (var question in quiz.Questions)
            {
                question.QuizId = quiz.Id;
                _connection.Insert(question);
            }
        }

        public async Task<List<Quiz>> GetQuizzesWithQuestions()
        {
            var quizzes = _connection.Table<Quiz>().ToList();
            foreach (var quiz in quizzes)
            {
                quiz.Questions = _connection.Table<Question>().Where(q => q.QuizId == quiz.Id).ToList();
            }
            return quizzes;
        }

        public async Task<Quiz> GetQuizWithQuestions(int quizId)
        {
            // Fetch the quiz by Id
            var quiz = _connection.Table<Quiz>().FirstOrDefault(q => q.Id == quizId);

            if (quiz != null)
            {
                // Fetch and assign its questions
                quiz.Questions = _connection.Table<Question>()
                    .Where(q => q.QuizId == quiz.Id)
                    .ToList();
            }

            return quiz;
        }

        public async Task UpdateQuiz(Quiz quiz)
        {
            _connection.Update(quiz);
            foreach (var question in quiz.Questions)
            {
                if (question.Id == 0)
                {
                    question.QuizId = quiz.Id;
                    _connection.Insert(question);
                }
                else
                {
                    _connection.Update(question);
                }
            }
        }

        public async Task DeleteQuiz(int quizId)
        {
            var questions = _connection.Table<Question>().Where(q => q.QuizId == quizId).ToList();
            foreach (var question in questions)
            {
                _connection.Delete(question);
            }
            _connection.Delete<Quiz>(quizId);
        }

        public async Task UpdateCharacterResult(CharacterResult result)
        {
            _connection.Update(result);
        }
    }
}
