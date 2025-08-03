using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Management_System.Models;

namespace School_Management_System.Services.Interfaces
{
    public interface ISubjectService
    {
        List<Subject> GetAllSubjects();
        bool CreateSubject(Subject subject);
        bool UpdateSubject(Subject subject);
        bool DeleteSubject(Subject subject);
        bool AssignTeacher(Class classEntity, Subject subject, Teacher teacher);
        Subject? GetSubjectByName(string subjectName);
        Subject? GetSubjectById(int id);


    }
}
