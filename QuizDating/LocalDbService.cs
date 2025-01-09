using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizDating.Models;

namespace QuizDating
{
    public class LocalDbService
    {
        private const string DbName = "SqliteDatabase.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DbName));
            _connection.CreateTableAsync<User>();
        }

        public async Task<List<User>> GetUsers()
        {
            return await _connection.Table<User>().ToListAsync();
        }

        public async Task CreateUser(User user)
        {
            await _connection.InsertAsync(user);
        }

        public async Task UpdateUser(User user)
        {
            await _connection.UpdateAsync(user);
        }

        public async Task DeleteUser(User user) 
        {
            await _connection.DeleteAsync(user);
        }
    }
}
 