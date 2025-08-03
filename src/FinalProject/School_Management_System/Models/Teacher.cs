using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Management_System.Models.JoinedEntities;

namespace School_Management_System.Models
{
    public class Teacher : User
    {
        // personal information
        public string Name { get; set; }

        // navigation properties
        public List<ClassesSubjects>? AssignedSubjects { get; set; }

    }
}
