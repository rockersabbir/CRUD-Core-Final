using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace practiceCRUD.ViewModel
{
    public class EditCourseViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }
    }
}
