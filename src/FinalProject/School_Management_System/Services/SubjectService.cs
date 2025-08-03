using School_Management_System.Models;
using School_Management_System.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Management_System.Data;
namespace School_Management_System.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly AppDbContext _context;
        public SubjectService(AppDbContext context)
        {
            _context = context;
        }

        public Subject? GetSubjectById(int id)
        {
            return _context.Subjects.FirstOrDefault(s => s.Id == id);
        }

        public Subject? GetSubjectByName(string subjectName)
        {
            return _context.Subjects.FirstOrDefault(s => s.Name.ToLower() == subjectName.ToLower());
        }

        public List<Subject> GetAllSubjects()
        {
            return _context.Subjects.ToList();
        }
        public bool CreateSubject(Subject subject)
        {
            _context.Subjects.Add(subject);
            return _context.SaveChanges() > 0;
        }
        public bool UpdateSubject(Subject subject)
        {
            var existingSubject = GetSubjectById(subject.Id);
            if (existingSubject == null)
            {
                return false; // Subject not found
            }
            existingSubject.Name = subject.Name;
            _context.Subjects.Update(existingSubject);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteSubject(Subject subject)
        {
            var existingSubject = GetSubjectById(subject.Id);
            if (existingSubject == null)
            {
                return false; // Subject not found
            }

            var relatedClasses = _context.ClassesSubjects.Where(cs => cs.SubjectId == subject.Id);
            if (relatedClasses.Any())
            {
                _context.ClassesSubjects.RemoveRange(relatedClasses);
            }
            _context.Subjects.Remove(existingSubject);
            return _context.SaveChanges() > 0;
        }

        public bool AssignTeacher(Class classEntity, Subject subject, Teacher teacher)
        { 
            var classSubject = _context.ClassesSubjects
                .FirstOrDefault(cs => cs.ClassId == classEntity.Id && cs.SubjectId == subject.Id);
            if (classSubject != null)
            {
                classSubject.AssignedTeacherId = teacher.Id;
                classSubject.AssignedTeacher = teacher;

                _context.ClassesSubjects.Update(classSubject);
                return _context.SaveChanges() > 0;
            }
            else
            {
                Console.WriteLine("Subject is not assigned to the class!");
                return false;
            }
        }
    }
}
