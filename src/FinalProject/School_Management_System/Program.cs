using School_Management_System.Data;
using School_Management_System.Services;
using School_Management_System.Utilities;
using School_Management_System.Panels;
using School_Management_System.Models;

Console.WriteLine("Welcome to the School Management System!");
Console.WriteLine();


using var context = new AppDbContext();

var authenticationService = new AutheticationService(context);

Console.Write("Please enter your username: ");
var username = Console.ReadLine();

Console.Write("Please enter your password: ");
var password = Console.ReadLine();

try
{
    object loggedUser = authenticationService.Login(username, password);
    if (loggedUser == null)
    {
        Console.WriteLine("Invalid credentials. Please try again.");
        return;
    }

    if (loggedUser is Admin admin)
    {
        var teacherService = new TeacherService(context);
        var classService = new ClassService(context);
        var subjectService = new SubjectService(context);
        var classChecker = new ClassChecker(context);
        var usernameChecker = new UsernameChecker(context);
        var subjectChecker = new SubjectChecker(context);

        var adminPanel = new AdminPanel(
            teacherService,
            classService,
            subjectService,
            usernameChecker,
            classChecker,
            subjectChecker,
            admin);

        adminPanel.DisplayMenu();
    }
    else if (loggedUser is Teacher teacher)
    {
        var classSubjectService = new ClassesSubjectsService(context);
        var classService = new ClassService(context);
        var subjectService = new SubjectService(context);
        var resultSheetService = new ResultSheetService(context);

        var teacherPanel = new TecherPannel(
            classSubjectService,
            classService,
            subjectService,
            resultSheetService,
            teacher);

        teacherPanel.DisplayMenu();
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
