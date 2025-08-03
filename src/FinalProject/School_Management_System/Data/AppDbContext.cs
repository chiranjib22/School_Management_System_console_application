using Microsoft.EntityFrameworkCore;
using School_Management_System.Models.JoinedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Management_System.Models;


namespace School_Management_System.Data
{
    public class AppDbContext : DbContext
    {
        private readonly string _connectionString;
        public AppDbContext()
        {
            _connectionString = "Server=.\\SQLEXPRESS;Database=CSharpB20;User Id=csharpb20;Password=123456;Trust Server Certificate = True;";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Teacher>().ToTable("Teachers");
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Class>().ToTable("Classes");
            modelBuilder.Entity<Subject>().ToTable("Subjects");
            modelBuilder.Entity<ResultSheet>().ToTable("ResultSheets");

            modelBuilder.Entity<Admin>().HasData(AdminSeed.GetAdmin());
            // configure composite primary key for relationships
            modelBuilder.Entity<ClassesSubjects>()
                .HasKey(tc => new {tc.ClassId,tc.SubjectId});

            // configure relationships

            //  class and subject have many-to-many relationship
            modelBuilder.Entity<ClassesSubjects>()
                .HasOne(cs => cs.Class)
                .WithMany(c => c.AssignedSubjects)
                .HasForeignKey(cs => cs.ClassId)
                .IsRequired(false);

            modelBuilder.Entity<ClassesSubjects>()
                .HasOne(cs => cs.Subject)
                .WithMany(s => s.AssignedClasses)
                .HasForeignKey(cs => cs.SubjectId)
                .IsRequired(false);


            // Teacher and ClassesSubjects have one-to-many relationship
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.AssignedSubjects)
                .WithOne(cs => cs.AssignedTeacher)
                .HasForeignKey(cs => cs.AssignedTeacherId)
                .IsRequired(false);

            // classesSubjects and ResultSheet have one-to-many relationship
            modelBuilder.Entity<ResultSheet>()
                .HasOne(cs => cs.ClassSubject)
                .WithMany(rs => rs.ResultSheets)
                .HasForeignKey(rs => new { rs.ClassId, rs.SubjectId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ResultSheet> ResultSheets { get; set; }
        public DbSet<ClassesSubjects> ClassesSubjects { get; set; }
    }
}
