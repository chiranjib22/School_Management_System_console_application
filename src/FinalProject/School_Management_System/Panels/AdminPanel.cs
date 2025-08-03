using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Management_System.Services;
using School_Management_System.Services.Interfaces;
using School_Management_System.Models;
using School_Management_System.Models.JoinedEntities;
using Microsoft.IdentityModel.Tokens;
using School_Management_System.Utilities;

namespace School_Management_System.Panels
{
    public class AdminPanel
    {
        private readonly ITeacherService _teacherService;
        private readonly IClassService _classService;
        private readonly ISubjectService _subjectService;

        private readonly IUsernameChecker _usernameChecker;
        private readonly IClassChecker _classChecker;
        private readonly ISubjectChecker _subjectChecker;
        private readonly Admin _currentUser;

        public AdminPanel(ITeacherService teacherService, IClassService classService, ISubjectService subjectService,
            IUsernameChecker usernameChecker, IClassChecker classChecker, ISubjectChecker subjectChecker,
            Admin currentUser)
        {
            _teacherService = teacherService;
            _classService = classService;
            _subjectService = subjectService;
            _currentUser = currentUser;
            _usernameChecker = usernameChecker;
            _subjectChecker = subjectChecker;
            _classChecker = classChecker;
        }
        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine($"--------- Welcome {_currentUser.Name} -----------");
            Console.WriteLine();
            Console.WriteLine("1. Create Class");
            Console.WriteLine("2. Create Subject");
            Console.WriteLine("3. Create Teacher");
            Console.WriteLine("4. View classes");
            Console.WriteLine("5. View subjects");
            Console.WriteLine("6. View teachers");
            Console.WriteLine("7. Logout");
            Console.Write("Please Select an option: ");
            var choice = Console.ReadLine();
            Console.Clear();
            switch (choice)
            {
                case "1":
                    CreateClass();
                    DisplayMenu();
                    break;
                case "2":
                    CreateSubject();
                    DisplayMenu();
                    break;
                case "3":
                    CreateTeacher();
                    DisplayMenu();
                    break;
                case "4":
                    ViewClass();
                    DisplayMenu();
                    break;
                case "5":
                    ViewSubject();
                    DisplayMenu();
                    break;
                case "6":
                    ViewTeacher();
                    DisplayMenu();
                    break;
                case "7":
                    Logout();
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    DisplayMenu();
                    break;
            }
        }



        private void CreateClass()
        {
            Console.WriteLine("Please provide the following to create a new class: ");
            Console.Write("Class Name: ");
            var className = Console.ReadLine();
            while (true)
            {
                if (className.IsNullOrEmpty())
                {
                    Console.WriteLine("Please enter a valid class name!!");
                }
                else if (_classChecker.IsUniqueClassName(className))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Class name already exists!!");
                    Console.WriteLine("Press Enter to back to admin pannel...");
                    Console.ReadLine();
                    Console.Clear();
                    return;

                }
                Console.Write("Class Name: ");
                className = Console.ReadLine();
            }

            var classObj = new Class
            {
                Name = className
            };
            if (_classService.CreateClass(classObj))
            {
                Console.WriteLine("Class created successfully!");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Failed to create class. Please try again.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void CreateSubject()
        {
            Console.WriteLine("Please provide the following to create a new subject: ");
            Console.Write("Subject Name: ");
            var subjectName = Console.ReadLine();
            while (true)
            {
                if (subjectName.IsNullOrEmpty())
                {
                    Console.WriteLine("Please enter a valid subject name!!");
                }
                else if (_subjectChecker.IsUniqueSubjectName(subjectName))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Subject name already exists!!");
                    Console.WriteLine("Press Enter to back to admin pannel..");
                    Console.ReadLine();
                    Console.Clear();
                    return;
                }
                Console.Write("Subject Name: ");
                subjectName = Console.ReadLine();
            }
            var subject = new Subject
            {
                Name = subjectName
            };
            if (_subjectService.CreateSubject(subject))
            {
                Console.WriteLine("Subject created successfully!");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Failed to create subject. Please try again.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void CreateTeacher()
        {
            Console.WriteLine("Please provide the following information to create a new teacher: ");
            Console.Write("Teacher Name: ");
            string name = Console.ReadLine();
            while (name.IsNullOrEmpty())
            {
                Console.Write("Teacher Name: ");
                name = Console.ReadLine();
            }

            Console.Write("Teacher Username: ");
            string userName = Console.ReadLine();
            while (true)
            {
                if (userName.IsNullOrEmpty())
                {
                    Console.WriteLine("Please enter an valid username!!");
                }
                else
                {

                    userName = _teacherService.GenerateUsername(userName);
                    if (_usernameChecker.IsUniqueUserName(userName)) break;
                    else
                    {
                        Console.WriteLine("Username already exist!!");
                        Console.WriteLine("Press Enter to back to admin pannel...");
                        Console.ReadLine();
                        Console.Clear();
                        return;
                    }
                }
                Console.Write("Teacher Username: ");
                userName = Console.ReadLine();
            }

            Console.Write("Teacher Password: ");
            string password = Console.ReadLine();
            while (password.IsNullOrEmpty())
            {
                Console.Write("Teacher Password: ");
                password = Console.ReadLine();
            }

            var teacher = new Teacher
            {
                Name = name,
                UserName = userName,
                Password = PasswordHelper.Hash(password)
            };

            if (_teacherService.CreateTeacher(teacher))
            {
                Console.WriteLine("Teacher created successfully!");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Failed to create teacher. Please try again.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }

        }



        private void ViewClass()
        {
            Console.WriteLine("Following classes are present in this system: ");
            List<Class> classes = _classService.GetAllClasses();

            if (classes.Count == 0)
            {
                Console.WriteLine("No classes found.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
                return;
            }
            else
            {
                foreach (var classObj in classes)
                {
                    Console.WriteLine($"{classObj.Name}");
                }
            }
            Console.WriteLine();

            Console.WriteLine("What you want to do?");
            Console.WriteLine("1. Edit Class");
            Console.WriteLine("2. Delete Class");
            Console.WriteLine("3. Assign Subject");
            Console.WriteLine("4. Back to Admin Panel");
            Console.Write("Please Select an option: ");
            var choice = Console.ReadLine();
            Console.Clear();
            switch(choice)
            {
                case "1":
                    EditClass();
                    ViewClass();
                    break;
                case "2":
                    DeleteClass();
                    ViewClass();
                    break;
                case "3":
                    AssignSubject();
                    ViewClass();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    ViewClass();
                    break;
            }
        }

        private void EditClass()
        {
            Console.WriteLine("Provide following information to edit class: ");
            Console.Write("Current Class Name: ");
            string className = Console.ReadLine();
            while (className.IsNullOrEmpty())
            {
                Console.Write("Current Class Name: ");
                className = Console.ReadLine();
            }
            Class? classObj = _classService.GetClassByName(className);
            if (classObj == null)
            {
                Console.WriteLine("Class not found.");
                return;
            }
            Console.Write("New Class Name: ");
            string newClassName = Console.ReadLine();
            while (true)
            {
                if(newClassName.IsNullOrEmpty())
                {
                    Console.WriteLine("Please enter a valid class name!!");
                }
                else if (_classChecker.IsUniqueClassName(newClassName))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Class name already exists! Please try another one.");
                }
                Console.Write("New Class Name: ");
                newClassName = Console.ReadLine();
            }

            classObj.Name = newClassName;
            if (_classService.UpdateClass(classObj))
            {
                Console.WriteLine("Class updated successfully!");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Failed to update class. Please try again.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void DeleteClass()
        {
            Console.WriteLine("Provide following information to delete a class: ");
            Console.Write("Class Name: ");
            string className = Console.ReadLine();
            while (className.IsNullOrEmpty())
            {
                Console.Write("Class Name: ");
                className = Console.ReadLine();
            }
            Class? classObj = _classService.GetClassByName(className);
            if (classObj == null)
            {
                Console.WriteLine("Class not found.");
                return;
            }
            if (_classService.DeleteClass(classObj))
            {
                Console.WriteLine("Class deleted successfully!");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Failed to delete class. Please try again.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void AssignSubject()
        {
            Console.WriteLine("Provide following information to assign a subject to class: ");
            Console.Write("Class Name: ");
            string className = Console.ReadLine();
            while (className.IsNullOrEmpty())
            {
                Console.Write("Class Name: ");
                className = Console.ReadLine();
            }
            Class? classObj = _classService.GetClassByName(className);
            if (classObj == null)
            {
                Console.WriteLine("Class not found.");
                return;
            }
            Console.Write("Subject Name: ");
            string subjectName = Console.ReadLine();
            while (subjectName.IsNullOrEmpty())
            {
                Console.Write("Subject Name: ");
                subjectName = Console.ReadLine();
            }
            Subject? subject = _subjectService.GetSubjectByName(subjectName);
            if (subject == null)
            {
                Console.WriteLine("Subject not found.");
                return;
            }
            if (_classService.assignSubject(classObj,subject))
            {
                Console.WriteLine("Subject assigned successfully!");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Failed to assign subject. Please try again.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }



        private void ViewSubject()
        {
            Console.WriteLine("Following subjects are present in this system: ");
            List<Subject> subjects = _subjectService.GetAllSubjects();

            if (subjects.Count == 0)
            {
                Console.WriteLine("No subjects found.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
                return;
            }
            else
            {
                foreach (var subject in subjects)
                {
                    Console.WriteLine($"{subject.Name}");
                }
            }
            Console.WriteLine();

            Console.WriteLine("What you want to do?");
            Console.WriteLine("1. Edit subject");
            Console.WriteLine("2. Delete subject");
            Console.WriteLine("3. Assign a teacher");
            Console.WriteLine("4. Back to Admin Panel");
            Console.Write("Please Select an option: ");
            var choice = Console.ReadLine();
            Console.Clear();
            switch (choice)
            {
                case "1":
                    EditSubject();
                    ViewSubject();
                    break;
                case "2":
                    DeleteSubject();
                    ViewSubject();
                    break;
                case "3":
                    AssignTeacher();
                    ViewSubject();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    ViewSubject();
                    break;
            }
        }

        private void EditSubject()
        {
            Console.WriteLine("Provide following information to edit a subject:");
            Console.Write("Current Subject Name: ");
            string subjectName = Console.ReadLine();
            while (subjectName.IsNullOrEmpty())
            {
                Console.Write("Subject Name: ");
                subjectName = Console.ReadLine();
            }
            Subject? subject = _subjectService.GetSubjectByName(subjectName);
            if (subject == null)
            {
                Console.WriteLine("Subject not found.");
                return;
            }
            Console.Write("New Subject Name: ");
            string newSubjectName = Console.ReadLine();
            while (true)
            {
                if (newSubjectName.IsNullOrEmpty())
                {
                    Console.WriteLine("Please enter a valid subject name!!");
                }
                else if (_subjectChecker.IsUniqueSubjectName(newSubjectName))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Subject name already exists! Please try another one.");
                }
                Console.Write("New Subject Name: ");
                newSubjectName = Console.ReadLine();
            }
            subject.Name = newSubjectName;
            if (_subjectService.UpdateSubject(subject))
            {
                Console.WriteLine("Subject updated successfully!");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Failed to update subject. Please try again.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void DeleteSubject()
        {
            Console.WriteLine("Provide following information to delete a subject: ");
            Console.Write("Subject Name: ");
            string subjectName = Console.ReadLine();
            while (subjectName.IsNullOrEmpty())
            {
                Console.Write("Subject Name: ");
                subjectName = Console.ReadLine();
            }
            Subject? subject = _subjectService.GetSubjectByName(subjectName);
            if (subject == null)
            {
                Console.WriteLine("Subject not found.");
                return;
            }
            if (_subjectService.DeleteSubject(subject))
            {
                Console.WriteLine("Subject deleted successfully!");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Failed to delete subject. Please try again.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void AssignTeacher()
        {
            Console.WriteLine("Provide following information to assign a teacher: ");

            Console.Write("Class Name: ");
            string className = Console.ReadLine();
            while (className.IsNullOrEmpty())
            {
                Console.Write("Class Name: ");
                className = Console.ReadLine();
            }
            Class? classObj = _classService.GetClassByName(className);
            if (classObj == null)
            {
                Console.WriteLine("Class not found.");
                return;
            }
            Console.Write("Subject Name: ");
            string subjectName = Console.ReadLine();
            while (subjectName.IsNullOrEmpty())
            {
                Console.Write("Subject Name: ");
                subjectName = Console.ReadLine();
            }
            Subject? subject = _subjectService.GetSubjectByName(subjectName);
            if (subject == null)
            {
                Console.WriteLine("Subject not found.");
                return;
            }
            Console.Write("Teacher Username: ");
            string teacherUsername = Console.ReadLine();
            while (teacherUsername.IsNullOrEmpty())
            {
                Console.Write("Teacher Username: ");
                teacherUsername = Console.ReadLine();
            }
            Teacher? teacher = _teacherService.GetTeacherByUsername(teacherUsername);
            if (teacher == null)
            {
                Console.WriteLine("Teacher not found.");
                return;
            }
            if (_subjectService.AssignTeacher(classObj, subject, teacher))
            {
                Console.WriteLine("Teacher assigned successfully!");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Failed to assign teacher. Please try again.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }



        private void ViewTeacher()
        {
            Console.WriteLine("Following teachers are present in this system: ");
            List<Teacher> teachers = _teacherService.GetAllTeachers();

            if (teachers.Count == 0)
            {
                Console.WriteLine("No teachers found.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
                return;
            }
            else
            {
                foreach (var teacher in teachers)
                {
                    Console.WriteLine(string.Format("{0,-20} {1,-20}", teacher.Name, $"[Username: {teacher.UserName.Remove(0, 4)}]"));
                }
            }
            Console.WriteLine();

            Console.WriteLine("What you want to do?");
            Console.WriteLine("1. Edit teacher");
            Console.WriteLine("2. Delete teacher");
            Console.WriteLine("3. Back to Admin Panel");
            Console.Write("Please Select an option: ");
            var choice = Console.ReadLine();
            Console.Clear();
            switch (choice)
            {
                case "1":
                    EditTeacher();
                    ViewTeacher();
                    break;
                case "2":
                    DeleteTeacher();
                    ViewTeacher();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    ViewSubject();
                    break;
            }
        }

        private void EditTeacher()
        {
            Console.WriteLine("Provide following information to edit teacher: ");
            Console.Write("Current Teacher Username: ");
            string teacherUsername = Console.ReadLine();
            while (teacherUsername.IsNullOrEmpty())
            {
                Console.Write("Current Teacher Username: ");
                teacherUsername = Console.ReadLine();
            }
            Teacher? teacher = _teacherService.GetTeacherByUsername(teacherUsername);
            if (teacher == null)
            {
                Console.WriteLine("Teacher not found.");
                return;
            }
            Console.Write("New Teacher Name: ");
            string newName = Console.ReadLine();
            while (newName.IsNullOrEmpty())
            {
                Console.Write("New Teacher Name: ");
                newName = Console.ReadLine();
            }
            teacher.Name = newName;
            if (_teacherService.UpdateTeacher(teacher))
            {
                Console.WriteLine("Teacher updated successfully!");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Failed to update teacher. Please try again.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void DeleteTeacher()
        {
            Console.WriteLine("Provide following information to delete a teacher: ");
            Console.Write("Teacher Username: ");
            string teacherUsername = Console.ReadLine();
            while (teacherUsername.IsNullOrEmpty())
            {
                Console.Write("Teacher Username: ");
                teacherUsername = Console.ReadLine();
            }
            Teacher? teacher = _teacherService.GetTeacherByUsername(teacherUsername);
            if (teacher == null)
            {
                Console.WriteLine("Teacher not found.");
                return;
            }
            if (_teacherService.DeleteTeacher(teacher))
            {
                Console.WriteLine("Teacher deleted successfully!");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Failed to delete teacher. Please try again.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }
        private void Logout()
        {
            Console.WriteLine("Logging out...");
            // Logic to handle logout if necessary
            // For now, just return to exit the admin panel
            return;
        }
    }
}
