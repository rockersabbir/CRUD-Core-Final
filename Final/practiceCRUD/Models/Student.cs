using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practiceCRUD.Models
{
    public class Student
    {

        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public int SchoolID { get; set; }
        public School SchoolName { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
