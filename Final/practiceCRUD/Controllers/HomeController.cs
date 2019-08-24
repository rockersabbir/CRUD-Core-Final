using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using practiceCRUD.Models;
using practiceCRUD.ViewModel;

namespace practiceCRUD.Controllers
{
    public class HomeController : Controller
    {


        public ApplicationDbContext DbContext { get; }

        public HomeController(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IActionResult Index()
        {

            List<StudentInfoViewModel> model = new List<StudentInfoViewModel>();
            foreach (var std in DbContext.Students)
            {
                std.StudentCourses = DbContext.studentCourses
                                         .Include(sc => sc.Course)
                                         .Where(sc => sc.StudentId == std.StudentId)
                                         .ToList();

                string school = DbContext.Schools.FirstOrDefault(s => s.SchoolID == std.SchoolID).SchoolName;

                model.Add(new StudentInfoViewModel { student = std, schoolName = school });
            }

            return View(model);

            // School school = DbContext.Schools.Include(sch => sch.Students).Single(sch => sch.SchoolID == 2);

            //// populate the StudentCourse table
            //Student s = DbContext.Students.FirstOrDefault(st => st.StudentId == 2);
            //Course c = DbContext.Courses.FirstOrDefault(cr => cr.CourseId == 2);

            //StudentCourse sc1 = new StudentCourse
            //{
            //    Student = s,
            //    StudentId = s.StudentId,
            //    Course = c,
            //    CourseId = c.CourseId
            //};

            //DbContext.studentCourses.Add(sc1);
            //DbContext.SaveChanges();

            // Retrive a student's courses

            //Student s = DbContext.Students.FirstOrDefault(st => st.SchoolID == 2);
            //s.StudentCourses = DbContext.studentCourses
            //                   .Include(sc => sc.Course)
            //                   .Where(sc => sc.StudentId == s.StudentId)
            //                   .ToList();
            //return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            StudentCreateViewModel model = new StudentCreateViewModel();
            foreach (var school in DbContext.Schools)
            {
                model.schoolList.Add(new SelectListItem
                {
                    Text = school.SchoolName,
                    Value = school.SchoolID.ToString()
                });
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(StudentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Student newStudent = new Student
                {
                    StudentName = model.StudentName,
                    SchoolID = model.SchoolID
                };

                DbContext.Students.Add(newStudent);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }


        //course
        [HttpGet]
        public IActionResult CourseDetails()
        {
            List<Course> courses = DbContext.Courses.ToList();

            return View(courses);
        }

        [HttpGet]
        public IActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCourse(CreateCourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                Course newCourse = new Course { CourseName = model.CourseName };
                DbContext.Courses.Add(newCourse);
                DbContext.SaveChanges();

                return RedirectToAction("CourseDetails");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EditCourse(int id)
        {
            Course course = DbContext.Courses.FirstOrDefault(c => c.CourseId == id);

            EditCourseViewModel model = new EditCourseViewModel { CourseName = course.CourseName, Id = id };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditCourse(EditCourseViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                Course course = DbContext.Courses.FirstOrDefault(c => c.CourseId == id);

                course.CourseName = model.CourseName;
                DbContext.Courses.Update(course);
                DbContext.SaveChanges();

                return RedirectToAction("CourseDetails");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteCourse(int id)
        {
            var course = DbContext.Courses.FirstOrDefault(c => c.CourseId == id);
            if (course != null)
            {
                DbContext.Courses.Remove(course);
                DbContext.SaveChanges();
                return RedirectToAction("CourseDetails");
            }
            else
            {
                ViewBag.ErrorMessage = $"Course with id: {id} not exist";
                return View("NotFound");
            }
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
