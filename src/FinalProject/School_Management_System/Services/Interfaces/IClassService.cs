using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Management_System.Models;

namespace School_Management_System.Services.Interfaces
{
    public interface IClassService
    {
        List<Class> GetAllClasses();
        bool CreateClass(Class classEntity);
        bool UpdateClass(Class classEntity);
        bool DeleteClass(Class classEntity);

        bool assignSubject(Class classObj,Subject subjectEntity);
        Class? GetClassByName(string className);
        Class? GetClassById(int id);
    }
}
