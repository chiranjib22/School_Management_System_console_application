using School_Management_System.Data;
using School_Management_System.Models;
using School_Management_System.Models.JoinedEntities;
using School_Management_System.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Services
{
    public class ResultSheetService : IResultSheetService
    {
        private readonly AppDbContext _context;
        public ResultSheetService(AppDbContext context)
        {
            _context = context;
        }

        private bool IsRecordExists(ClassesSubjects classSubject, string studentName)
        {
            return _context.ResultSheets
                .Any(rs => rs.ClassId == classSubject.ClassId &&
                           rs.SubjectId == classSubject.SubjectId &&
                           rs.StudentName == studentName);
        }

        public bool AddRecordsToSheets(Class? classEntity, string? studentName, Teacher currentUser)
        {
            if (classEntity == null || string.IsNullOrEmpty(studentName))
            {
                return false;
            }
            var classesSubjects = _context.ClassesSubjects
                .Where(cs => cs.ClassId == classEntity.Id && 
                             cs.AssignedTeacherId == currentUser.Id)
                .ToList();

            if (classesSubjects.Count == 0)
            {
                throw new Exception("No subjects assigned to the class for the current teacher.");
            }

            foreach (var classSubject in classesSubjects)
            {
                if(IsRecordExists(classSubject, studentName))
                {
                    continue; // Skip if record already exists
                }
                var resultSheet = new ResultSheet
                {
                    ClassId = classSubject.ClassId!.Value,
                    SubjectId = classSubject.SubjectId!.Value,
                    StudentName = studentName,
                    FirstTerm = 0.0,
                    MidTerm = 0.0,
                    FinalTerm = 0.0
                };
                _context.ResultSheets.Add(resultSheet);
            }
            return _context.SaveChanges() > 0; // All records added successfully
        }

        public List<ResultSheet> getAllResultsOfStudent(Class? classEntity, string? studentName, Teacher currentUser)
        {
            if (classEntity == null || string.IsNullOrEmpty(studentName))
            {
                return new List<ResultSheet>();
            }
            var assignedSubjects = _context.ClassesSubjects
                .Where(cs => cs.ClassId == classEntity.Id && 
                             cs.AssignedTeacherId == currentUser.Id)
                .Select(cs => cs.SubjectId)
                .ToList();
            if (assignedSubjects.Count != 0)
            {
               return _context.ResultSheets
              .Where(rs => rs.ClassId == classEntity.Id &&
                           rs.StudentName == studentName &&
                           assignedSubjects.Contains(rs.SubjectId))
              .ToList();
            }else
            {
                return new List<ResultSheet>();
            }
        }

        public List<ResultSheet> getResultsOfAllStudents(Class? classEntity, 
            Subject? subjectEntity)
        {
            if (classEntity == null || subjectEntity == null)
            {
                return new List<ResultSheet>();
            }
            return _context.ResultSheets
                .Where(rs => rs.ClassId == classEntity.Id && 
                             rs.SubjectId == subjectEntity.Id)
                .ToList();
        }

        public bool InsertGrade(Class? classEntity, Subject? subjectEntity, string? studentName,
            string termName, double grade, Teacher currentUser)
        {
            var resultSheet = _context.ResultSheets
                .FirstOrDefault(rs => rs.ClassId == classEntity!.Id &&
                                      rs.SubjectId == subjectEntity!.Id &&
                                      rs.StudentName == studentName );
            if (resultSheet == null)
                throw new Exception("Result sheet not found for the specified class, subject, and student.");
            else
            {
                switch(termName)
                {
                    case "1st":
                        resultSheet.FirstTerm = grade;
                        break;
                    case "Mid":
                        resultSheet.MidTerm = grade;
                        break;
                    case "Final":
                        resultSheet.FinalTerm = grade;
                        break;
                    default:
                        throw new Exception("Invalid term name provided.");
                }
                _context.ResultSheets.Update(resultSheet);
                return _context.SaveChanges() > 0;
            }
        }

        public bool RemoveRecordsToSheets(Class? classEntity, string? studentName, Teacher currentUser)
        {
            if (classEntity == null || string.IsNullOrEmpty(studentName))
            {
                return false;
            }
            var classesSubjects = _context.ClassesSubjects
                .Where(cs => cs.ClassId == classEntity.Id &&
                             cs.AssignedTeacherId == currentUser.Id)
                .ToList();

            if (classesSubjects.Count == 0)
            {
                throw new Exception("No subjects assigned to the class for the current teacher.");
            }

            foreach (var classSubject in classesSubjects)
            {
                var resultSheet = _context.ResultSheets
                    .FirstOrDefault(rs => rs.ClassId == classSubject.ClassId &&
                                          rs.SubjectId == classSubject.SubjectId &&
                                          rs.StudentName == studentName);
                if (resultSheet != null)
                {
                    _context.ResultSheets.Remove(resultSheet);
                }
            }
            return _context.SaveChanges() > 0; // All records removed successfully
        }
    }
}
