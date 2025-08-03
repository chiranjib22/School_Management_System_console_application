# 🎓 School Management System – C# Console Application

This is my **final project** for the _Professional Programming with C#_ course. It’s a role-based school management system built using C#, .NET Core, and Entity Framework Core. The system manages students, teachers, subjects, grades, and login functionality—all built following SOLID principles and secure password practices.

---

## 🛠 Tech Stack

- C# (.NET Core)
- Entity Framework Core
- Microsoft SQL Server
- BCrypt.Net (for password hashing)

---

## 📦 Features

- Admin and Teacher login (with encrypted passwords)
- CRUD operations for Students, Teachers, Subjects, Classes, and Grades
- Role-based access control
- Secure password storage using Bcrypt
- Structured using services, repositories, and models

---

## 📁 Project Structure

```
School_Management_System/
├── Models/          # Entity classes
├── Services/        # Business logic
├── Repository/      # Data access layer
├── Data/            # DbContext and configuration
├── Utilities/       # Helper methods (e.g., password hashing)
└── Program.cs       # Application entry point
```

---

## 🚀 Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/chiranjib22/School_Management_System_console_application.git
   cd src/FinalProject/School_Management_System
   ```

2. **Configure your database**
   In `AppDbContext.cs`, update the connection string in:
   ```csharp
   optionsBuilder.UseSqlServer("your_connection_string");
   ```

3. **Run the application**
   ```bash
   dotnet build
   dotnet run
   ```

---

## 🧠 What I Learned

- Practicing SOLID principles in real projects
- Using Entity Framework Core with SQL Server
- Implementing secure login using password hashing
- Designing layered and modular architecture
- Thinking like a backend developer

---

## 👨‍💻 Author

**Chiranjib Chakraborty**  
Final project for _Professional Programming with C#_  
🔗 [LinkedIn](https://www.linkedin.com/in/chiranjibchakraborty)  
💬 Open to backend internships and junior developer roles

---

## 📜 License

This project is open-source under the MIT License.