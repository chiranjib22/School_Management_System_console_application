using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Management_System.Models;
using School_Management_System.Data;
using School_Management_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace School_Management_System.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _context;

        public TeacherService(AppDbContext context)
        {
            _context = context;
        }

        public Teacher? GetTeacherById(int id)
        {
            return _context.Teachers.FirstOrDefault(t => t.Id == id);
        }

        public Teacher? GetTeacherByUsername(string teacherUserName)
        {
            teacherUserName = GenerateUsername(teacherUserName);
            return _context.Teachers.FirstOrDefault(t => t.UserName == teacherUserName);
        }

        public List<Teacher> GetAllTeachers()
        {
            return _context.Teachers.ToList();
        }

        public bool CreateTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateTeacher(Teacher teacher)
        {
            var existingTeacher = GetTeacherById(teacher.Id);
            if (existingTeacher == null)
            {
                return false; // Teacher not found
            }
            existingTeacher.Name = teacher.Name;
            _context.Teachers.Update(existingTeacher);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteTeacher(Teacher teacher)
        {
            var existingTeacher = GetTeacherById(teacher.Id);
            if (existingTeacher == null)
            {
                return false; // Teacher not found
            }

            // remove from classesSubjects if exists
            var relatedClassesSubjects = _context.ClassesSubjects
                .Where(cs => cs.AssignedTeacherId == existingTeacher.Id);
            if (relatedClassesSubjects.Any())
            {
                foreach(var cs in relatedClassesSubjects)
                {
                    cs.AssignedTeacherId = null; // Unassign the teacher
                }
            }
            _context.Teachers.Remove(existingTeacher);
            return _context.SaveChanges() > 0;
        }

        public string GenerateUsername(string input)
        {
            return input.Replace(" ", "");
        }
    }
}
