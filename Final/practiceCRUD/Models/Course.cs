using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practiceCRUD.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
