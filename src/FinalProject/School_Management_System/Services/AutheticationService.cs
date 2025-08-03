using School_Management_System.Data;
using School_Management_System.Models;
using School_Management_System.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Services
{
    public class AutheticationService
    {
        private readonly AppDbContext _context;
        public AutheticationService(AppDbContext context)
        {
            _context = context;
        }

        public object Login(string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null; // Return null if username or password is empty
            }

            if (username != "admin")
            {
                // Teacher login
                var teacher = _context.Users.FirstOrDefault(t => t.UserName == username);
                if (teacher == null)
                {
                    throw new Exception("Username is wrong!");
                }
                else if (!PasswordHelper.Verify(password, teacher.Password))
                {
                    throw new Exception("Password is wrong!");
                }
                else
                {
                    return teacher; // Return the authenticated teacher
                }
            }
            else
            {
                // Admin login
                var admin = _context.Users.FirstOrDefault(u => u.UserName == username);
                if (admin == null)
                {
                    throw new Exception("Username is wrong!");
                }
                else if(!PasswordHelper.Verify(password, admin.Password))
                {
                    throw new Exception("Password is wrong!");
                }
                else
                {
                    return admin; // Return the authenticated admin
                }
            }
        }
    }
}
