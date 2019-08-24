using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace practiceCRUD.ViewModel
{
    public class StudentCreateViewModel
    {
        public StudentCreateViewModel()
        {
            schoolList = new List<SelectListItem>();
        }

        [Required]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        public int SchoolID { get; set; }

        public List<SelectListItem> schoolList { get; set; }
    }
}

