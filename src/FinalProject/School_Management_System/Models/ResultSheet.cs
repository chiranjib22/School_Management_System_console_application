using School_Management_System.Models.JoinedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Models
{
    public class ResultSheet
    {
        public int Id { get; set; }

        public string StudentName { get; set; }
        // Result Sheet Information
        public double? FirstTerm { get; set; }
        public double? MidTerm { get; set; }
        public double? FinalTerm { get; set; }

        // navigation properties
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public ClassesSubjects? ClassSubject { get; set; }
    }
}
