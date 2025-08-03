
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net; // Ensure you have the BCrypt.Net NuGet package installed

namespace School_Management_System.Utilities
{
    public static class PasswordHelper
    {
        public static string Hash(string password)
        {
            // Generate a salt and hash the password
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool Verify(string entered, string hashed)
        {
            return BCrypt.Net.BCrypt.Verify(entered, hashed);
        }
    }
}
