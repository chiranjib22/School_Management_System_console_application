using School_Management_System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Utilities
{
    public class ClassChecker : IClassChecker
    {
        private readonly AppDbContext _context;

        public ClassChecker(AppDbContext context)
        {
            _context = context;
        }

        public bool IsUniqueClassName(string className)
        {
            // Check if the class name already exists in the database
            return !_context.Classes.Any(c => c.Name.ToLower() == className.ToLower());
        }
    }
}
