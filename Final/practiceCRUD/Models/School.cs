using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practiceCRUD.Models
{
    public class School
    {
        public int SchoolID { get; set; }
        public string SchoolName { get; set; }

        public IList<Student> Students { get; set; } = new List<Student>();
    }
}
