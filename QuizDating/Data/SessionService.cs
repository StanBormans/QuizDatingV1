using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizDating.Models;

namespace QuizDating.Data
{
    public static class SessionService
    {
        private static User _loggedInUser;

        public static User LoggedInUser
        {
            get => _loggedInUser;
            set
            {
                _loggedInUser = value;

                // Ensure the CharacterResult is loaded
                if (_loggedInUser != null && _loggedInUser.CharacterResultId > 0)
                {
                    var dbService = new LocalDbService();
                    _loggedInUser.CharacterResult = dbService.GetCharacterResult(_loggedInUser.CharacterResultId);
                }
            }
        }

        public static bool IsUserLoggedIn => _loggedInUser != null;

        public static void Logout()
        {
            _loggedInUser = null;
        }
    }
}
