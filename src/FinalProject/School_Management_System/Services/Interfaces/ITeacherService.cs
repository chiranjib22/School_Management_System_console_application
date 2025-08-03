using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Management_System.Models;

namespace School_Management_System.Services.Interfaces
{
    public interface ITeacherService
    {
        List<Teacher> GetAllTeachers();
        bool CreateTeacher(Teacher teacher);
        bool UpdateTeacher(Teacher teacher);
        bool DeleteTeacher(Teacher teacher);
        string GenerateUsername(string input);
        Teacher? GetTeacherByUsername(string teacherUsername);
        Teacher? GetTeacherById(int id);
    }
}
