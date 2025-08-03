using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Management_System.Data;

namespace School_Management_System.Utilities
{
    class SubjectChecker : ISubjectChecker
    {
        private readonly AppDbContext _context;
        public SubjectChecker(AppDbContext context)
        {
            _context = context;
        }
        public bool IsUniqueSubjectName(string subjectName)
        {
            // Check if the subject name already exists in the database
            return !_context.Subjects.Any(s => s.Name.ToLower() == subjectName.ToLower());
        }
    }
}
