using Microsoft.EntityFrameworkCore;
using School_Management_System.Data;
using School_Management_System.Models;
using School_Management_System.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Services
{
    public class ClassesSubjectsService : IClassesSubjectsService
    {
        private readonly AppDbContext _context;
        public ClassesSubjectsService(AppDbContext context)
        {
            _context = context;
        }


        public Dictionary<string, List<string>> getAssignedClassesSubjects(Teacher teacher)
        {
            var result = new Dictionary<string, List<string>>();
            result = _context.ClassesSubjects
                        .Where(cs => cs.AssignedTeacherId == teacher.Id)
                        .Include(cs => cs.Class)
                        .Include(cs => cs.Subject)
                        .AsEnumerable() // Bring data into memory for grouping
                        .GroupBy(cs => cs.Class?.Name)
                        .ToDictionary(
                            group => group.Key ?? "Unknown Class",
                            group => group
                            .Where(cs => cs.Subject != null)
                            .Select(cs => cs.Subject!.Name!)
                            .ToList()
                        );
            return result;
        }

        public bool IsAssignedTeacher(string className, string subjectName, Teacher currentUser)
        {
            return _context.ClassesSubjects
                .Any(cs => cs.Class!.Name == className && 
                           cs.Subject!.Name == subjectName && 
                           cs.AssignedTeacherId == currentUser.Id);
        }

        public bool IsAssignedTeachertoClass(string? className, Teacher currentUser)
        {
            return _context.ClassesSubjects
                .Any(cs => cs.Class!.Name == className && 
                           cs.AssignedTeacherId == currentUser.Id);
        }
    }
}
