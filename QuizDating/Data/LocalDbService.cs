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

        public async Task<List<User>> GetUsers()
        {
            return _connection.Table<User>().ToList();
        }

        public async Task CreateUser(User user)
        {
            _connection.Insert(user);
        }

        public async Task UpdateUser(User user)
        {
            _connection.Update(user);
        }

        public async Task DeleteUser(User user)
        {
            _connection.Delete(user);
        }
    }
}
