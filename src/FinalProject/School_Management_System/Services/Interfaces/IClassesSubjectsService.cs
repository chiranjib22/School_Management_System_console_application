using School_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Services.Interfaces
{
    public interface IClassesSubjectsService
    {
        Dictionary<string, List<string>> getAssignedClassesSubjects(Teacher teacher);
        bool IsAssignedTeacher(string className, string subjectName, Teacher currentUser);
        bool IsAssignedTeachertoClass(string? className, Teacher currentUser);
    }
}
