using System.Text.RegularExpressions;

namespace BookStore.Models
{

    public class Validator
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool IsValidLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login)) return false;

            string pattern = @"^[a-zA-Z0-9_-]{3,20}$";
            return Regex.IsMatch(login, pattern);
        }

        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            string pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            return Regex.IsMatch(password, pattern);
        }
    }
}
