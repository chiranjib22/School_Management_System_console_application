using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Management_System.Data;

namespace School_Management_System.Utilities
{
    public class UsernameChecker : IUsernameChecker
    {
        private readonly AppDbContext _context;

        public UsernameChecker(AppDbContext context)
        {
            _context = context;
        }
        public bool IsUniqueUserName(string username)
        {
            // check the uniqueness
            bool exist = _context.Users.Any(u => u.UserName.ToLower() == username.ToLower());
            return !exist;
        }
    }
}
