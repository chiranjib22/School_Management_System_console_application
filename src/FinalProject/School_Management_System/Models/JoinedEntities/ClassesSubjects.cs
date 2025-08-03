using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Models.JoinedEntities
{
    public class ClassesSubjects
    {
        public int? ClassId { get; set; }
        public int? SubjectId { get; set; }

        // Navigation properties
        public Class? Class { get; set; }
        public Subject? Subject { get; set; }

        // Additional properties can be added here if needed
        public List<ResultSheet>? ResultSheets { get; set; }
        public int? AssignedTeacherId { get; set; }
        public Teacher? AssignedTeacher { get; set; }
    }
}
