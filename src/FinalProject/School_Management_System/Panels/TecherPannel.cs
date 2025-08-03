using Microsoft.IdentityModel.Tokens;
using School_Management_System.Data;
using School_Management_System.Models;
using School_Management_System.Services.Interfaces;
using School_Management_System.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Panels
{
    public class TecherPannel
    {
        private readonly IClassesSubjectsService _classesSubjectsService;
        private readonly IClassService _classService;
        private readonly ISubjectService _subjectService;
        private readonly IResultSheetService _resultSheetService;


        private readonly Teacher _currentUser; 
        public TecherPannel(IClassesSubjectsService classesSubjectsService,IClassService classService,
            ISubjectService subjectService, IResultSheetService resultSheetService,
            Teacher currentUser)
        {
            _classesSubjectsService = classesSubjectsService;
            _classService = classService;
            _subjectService = subjectService;
            _resultSheetService = resultSheetService;
            _currentUser = currentUser;
        }

        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine($"--------- Welcome {_currentUser.Name} -----------");
            Console.WriteLine();
            Console.WriteLine("You are assigned to the following classes and subjects:");
            Console.WriteLine();
            viewClassesSubjects();
            Console.WriteLine("1. View grades of a class");
            Console.WriteLine("2. View grades of a student");
            Console.WriteLine("3. Insert grades");
            Console.WriteLine("4. Add student");
            Console.WriteLine("5. Remove student");
            Console.WriteLine("6. Logout");
            Console.Write("Please Select an option: ");
            var choice = Console.ReadLine();
            Console.Clear();
            switch (choice)
            {
                case "1":
                    ViewClassGrades();
                    DisplayMenu();
                    break;
                case "2":
                    ViewStudentGrades();
                    DisplayMenu();
                    break;
                case "3":
                    InsertGrades();
                    DisplayMenu();
                    break;
                case "4":
                    AddStudent();
                    DisplayMenu();
                    break;
                case "5":
                    RemoveStudent();
                    DisplayMenu();
                    break;
                case "6":
                    Logout();
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    DisplayMenu();
                    break;
            }

        }

        private void viewClassesSubjects()
        {
            var assignedClassesSubejcts = _classesSubjectsService.getAssignedClassesSubjects(_currentUser);
            if (assignedClassesSubejcts.Count == 0)
            {
                Console.WriteLine("You are not assigned to any classes or subjects.");
                return;
            }
            Console.WriteLine(string.Format("{0,-20} {1}", "Class Name", "Subjects"));
            foreach (var classSubject in assignedClassesSubejcts)
            {
                Console.Write(string.Format("{0,-20}", classSubject.Key));
                for (int i = 0; i < classSubject.Value.Count; i++)
                {
                    if (i > 0) Console.Write(", ");
                    Console.Write("{0}",classSubject.Value[i]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void ViewClassGrades()
        {
            Console.WriteLine("Please provide following information to view grades:");
            Console.Write("Class name: ");
            var className = Console.ReadLine();
            while (className.IsNullOrEmpty())
            {
                Console.WriteLine("Class name cannot be empty.");
                Console.Write("Class name: ");
                className = Console.ReadLine();
            }
            var classEntity = _classService.GetClassByName(className);
            Console.Write("Subject name: ");
            var subjectName = Console.ReadLine();
            while (subjectName.IsNullOrEmpty())
            {
                Console.WriteLine("Subject name cannot be empty.");
                Console.Write("Subject name: ");
                subjectName = Console.ReadLine();
            }
            var subjectEntity = _subjectService.GetSubjectByName(subjectName);

            if (_classesSubjectsService.IsAssignedTeacher(className, subjectName, _currentUser))
            {
                var allStudentsResult = _resultSheetService.getResultsOfAllStudents(classEntity,
                    subjectEntity);
                if (allStudentsResult.Count == 0)
                {
                    Console.WriteLine("No students found for this class and subject.");
                }else
                {
                    Console.WriteLine();
                    Console.WriteLine(string.Format("{0,-20} {1,-8} {2,-8} {3,-8}",
                        "Name", "1st", "Mid", "Final"));
                    foreach (var student in allStudentsResult)
                    {
                        Console.WriteLine(string.Format("{0,-20} {1,-8} {2,-8} {3,-8}",
                            student.StudentName, student.FirstTerm, student.MidTerm,
                            student.FinalTerm));
                    }
                }
            }
            else
            {
                Console.WriteLine("You are not assigned to this class or subject.");
            }
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
            return;
        }

        private void ViewStudentGrades()
        {
            Console.WriteLine("Please provide following information to view grades:");
            Console.Write("Class name: ");
            var className = Console.ReadLine();
            while (true)
            {
                if( className.IsNullOrEmpty())
                {
                    Console.WriteLine("Class name cannot be empty.");
                }
                else if (_classesSubjectsService.IsAssignedTeachertoClass(className,_currentUser) == false)
                {
                    Console.WriteLine("You are not assigned to this class.");
                }
                else
                {
                    break;
                }
                Console.Write("class name: ");
                className = Console.ReadLine();
            }
            var classEntity = _classService.GetClassByName(className);
            Console.Write("Student name: ");
            var studentName = Console.ReadLine();
            while (studentName.IsNullOrEmpty())
            {
                Console.WriteLine("Student name cannot be empty.");
                Console.Write("Student name: ");
                studentName = Console.ReadLine();
            }
            var subjectWiseResults = _resultSheetService.getAllResultsOfStudent(classEntity,
                    studentName, _currentUser);
            if (subjectWiseResults.Count == 0)
            {
                Console.WriteLine("No results found for this student in the specified class.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(string.Format("{0,-20} {1,-8} {2,-8} {3,-8}",
                    "Subject Name", "1st", "Mid", "Final"));
                foreach (var result in subjectWiseResults)
                {
                    var subject = _subjectService.GetSubjectById(result.SubjectId);
                    Console.WriteLine(string.Format("{0,-20} {1,-8} {2,-8} {3,-8}",
                            subject.Name, result.FirstTerm, result.MidTerm,
                            result.FinalTerm));
                }
            }
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
            return;
        }

        private void InsertGrades()
        {
            Console.WriteLine("Please provide following information to insert grades:");
            Console.Write("Class name: ");
            var className = Console.ReadLine();
            while (className.IsNullOrEmpty())
            {
                Console.WriteLine("Class name cannot be empty.");
                Console.Write("Class name: ");
                className = Console.ReadLine();
            }
            var classEntity = _classService.GetClassByName(className);
            Console.Write("Subject name: ");
            var subjectName = Console.ReadLine();
            while (subjectName.IsNullOrEmpty())
            {
                Console.WriteLine("Subject name cannot be empty.");
                Console.Write("Subject name: ");
                subjectName = Console.ReadLine();
            }
            var subjectEntity = _subjectService.GetSubjectByName(subjectName);
            if (_classesSubjectsService.IsAssignedTeacher(className, subjectName, _currentUser))
            {
                Console.Write("Student name: ");
                var studentName = Console.ReadLine();
                while (true)
                {
                    if (studentName.IsNullOrEmpty())
                    {
                        Console.WriteLine("Student name cannot be empty.");
                        Console.Write("Student name: ");
                        studentName = Console.ReadLine();

                    }
                    else if (!_resultSheetService.getAllResultsOfStudent(classEntity, studentName, _currentUser).Any())
                    {
                        Console.WriteLine("This student is not enrolled in this class.");
                        Console.Write("Press Enter to back to Teacher Panel...");
                        Console.ReadLine();
                        Console.Clear();
                        return;

                    }
                    else
                    {
                        break;
                    }
                }
                Console.Write("Term name(1st, mid, final): ");
                var termName = Console.ReadLine();
                while (termName.IsNullOrEmpty() ||
                       !(termName.Equals("1st", StringComparison.OrdinalIgnoreCase) ||
                         termName.Equals("mid", StringComparison.OrdinalIgnoreCase) ||
                         termName.Equals("final", StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Invalid term name. Please enter '1st', 'mid', or 'final'.");
                    Console.Write("Term name(1st, mid, final): ");
                    termName = Console.ReadLine();
                }
                Console.Write("Grade(0.00 to 5.00): ");
                var gradeInput = double.TryParse(Console.ReadLine(), out double grade);
                while (!gradeInput || grade < 0.00 || grade > 5.00)
                {
                    Console.WriteLine("Invalid grade. Please enter a grade between 0.00 and 5.00.");
                    Console.Write("Grade(0.00 to 5.00): ");
                    gradeInput = double.TryParse(Console.ReadLine(), out grade);
                }
                try
                {
                    bool success = _resultSheetService.InsertGrade(classEntity, subjectEntity, studentName,
                        termName, grade, _currentUser);
                    if (success)
                    {
                        Console.WriteLine("Grade inserted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to insert grade. Please try again.");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("You are not assigned to this class or subject.");
            }
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
            return;
        }

        private void AddStudent()
        {
            Console.WriteLine("Please provide following information to add a student:");
            Console.Write("Class name: ");
            var className = Console.ReadLine();
            while(true)
            {
                if (className.IsNullOrEmpty())
                {
                    Console.WriteLine("Class name cannot be empty.");
                }
                else if (_classesSubjectsService.IsAssignedTeachertoClass(className, _currentUser) == false)
                {
                    Console.WriteLine("You are not assigned to this class.");
                }
                else
                {
                    break;
                }
                Console.Write("Class name: ");
                className = Console.ReadLine();
            }
            var classEntity = _classService.GetClassByName(className);
            Console.Write("Student name: ");
            var studentName = Console.ReadLine();
            while (studentName.IsNullOrEmpty())
            {
                Console.WriteLine("Student name cannot be empty.");
                Console.Write("Student name: ");
                studentName = Console.ReadLine();
            }

            // method to new records to a result sheet with a class and every subjects that
            // the teacher is assigned to
            try
            {
                bool success = _resultSheetService.AddRecordsToSheets(classEntity, studentName, _currentUser);
                if (success)
                {
                    Console.WriteLine("Student added successfully.");
                }
                else
                {
                    Console.WriteLine("Student is already exists in this class.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the student: {ex.Message}");
            }
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
            return;
        }

        private void RemoveStudent()
        {
            Console.WriteLine("Please provide following information to remove a student:");
            Console.Write("Class name: ");
            var className = Console.ReadLine();
            while (true)
            {
                if (className.IsNullOrEmpty())
                {
                    Console.WriteLine("Class name cannot be empty.");
                }
                else if (_classesSubjectsService.IsAssignedTeachertoClass(className, _currentUser) == false)
                {
                    Console.WriteLine("You are not assigned to this class.");
                }
                else
                {
                    break;
                }
                Console.Write("Class name: ");
                className = Console.ReadLine();
            }
            var classEntity = _classService.GetClassByName(className);
            Console.Write("Student name: ");
            var studentName = Console.ReadLine();
            while (studentName.IsNullOrEmpty())
            {
                Console.WriteLine("Student name cannot be empty.");
                Console.Write("Student name: ");
                studentName = Console.ReadLine();
            }

            // method to remove records from result sheet with a class and every subjects that
            // the student is assigned
            try
            {
                bool success = _resultSheetService.RemoveRecordsToSheets(classEntity, studentName, _currentUser);
                if (success)
                {
                    Console.WriteLine("Student removed successfully.");
                }
                else
                {
                    Console.WriteLine("Student removal failed. Please check if the student exists in this class.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the student: {ex.Message}");
            }
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
            return;
        }

        private void Logout()
        {
            Console.WriteLine("Logging out...");
            return;
        }
    }
}
