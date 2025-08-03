using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Management_System.Models;
using School_Management_System.Data;
using School_Management_System.Services.Interfaces;
using School_Management_System.Models.JoinedEntities;
using Microsoft.EntityFrameworkCore;

namespace School_Management_System.Services
{
    public class ClassService : IClassService
    {
        private readonly AppDbContext _context;

        public ClassService(AppDbContext context)
        {
            _context = context;
        }
        
        public Class? GetClassById(int id)
        {
            return _context.Classes.FirstOrDefault(c => c.Id == id);
        }

        public Class? GetClassByName(string className)
        {
            return _context.Classes.FirstOrDefault(c => c.Name.ToLower() == className.ToLower());
        }

        public List<Class> GetAllClasses()
        {
            return _context.Classes.ToList();
        }

        public bool CreateClass(Class classEntity)
        {
           _context.Classes.Add(classEntity);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateClass(Class classEntity)
        {
            var existingClass = GetClassById(classEntity.Id);
            if (existingClass == null)
            {
                return false; // Class not found
            }
            existingClass.Name = classEntity.Name;
            _context.Classes.Update(existingClass);
            return _context.SaveChanges() > 0;

        }

        public bool DeleteClass(Class classEntity)
        {
            var existingClass = GetClassById(classEntity.Id);
            if (existingClass == null)
            {
                return false; // Class not found
            }
            var relatedSubjects = _context.ClassesSubjects.Where(cs => cs.ClassId == classEntity.Id);
            if (relatedSubjects.Any())
            {
                _context.ClassesSubjects.RemoveRange(relatedSubjects);
            }
            _context.Classes.Remove(existingClass);
            return _context.SaveChanges() > 0;
        }

        public bool assignSubject(Class classObj, Subject subjectEntity)
        {
            var existingClass = _context.Classes
                .Include(c => c.AssignedSubjects)
                .FirstOrDefault(c => c.Id == classObj.Id);

            if (existingClass == null)
            {
                return false; // Class not found
            }

            if (existingClass.AssignedSubjects == null)
            {
                existingClass.AssignedSubjects = new List<ClassesSubjects>();
            }

            if (existingClass.AssignedSubjects.Any(cs => cs.SubjectId == subjectEntity.Id))
            {
                Console.WriteLine("Subject is already assigned to the class!");
                return false; // Subject already assigned to the class
            }

            existingClass.AssignedSubjects.Add(new ClassesSubjects
            {
                ClassId = existingClass.Id,
                SubjectId = subjectEntity.Id,
                Class = existingClass,
                Subject = subjectEntity
            });
            _context.Classes.Update(existingClass);
            return _context.SaveChanges() > 0;
        }
    }
}
