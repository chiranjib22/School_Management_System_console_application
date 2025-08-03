using School_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Services.Interfaces
{
    public interface IResultSheetService
    {
        bool AddRecordsToSheets(Class? classEntity, string? studentName, Teacher currentUser);
        List<ResultSheet> getAllResultsOfStudent(Class? classEntity, string? studentName, Teacher currentUser);
        List<ResultSheet> getResultsOfAllStudents(Class? classEntity, Subject? subjectEntity);
        bool InsertGrade(Class? classEntity, Subject? subjectEntity, string? studentName, 
            string termName, double grade, Teacher currentUser);
        bool RemoveRecordsToSheets(Class? classEntity, string? studentName, Teacher currentUser);
    }
}
