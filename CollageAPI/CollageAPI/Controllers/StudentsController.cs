using CollageAPI.Identity;
using CollageAPI.Models;
using CollageAPI.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IdentityApplicationDbContext _context;
        public StudentsController(IdentityApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// GetAll method is used to get the list of all students from database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            //_context.StudentCourses.ToList()
            // return Ok(_context.Students.ToList());
            //_context.Students.ToList()
            //_context.Trails.Include(t => t.NationalPark).
            //   FirstOrDefault(s => s.Id == trailId);
            //    context.Customers.Where(c => c.CustomerID == 1)
            //.Include(c => c.Invoices)
            //.Where(c => c.Invoices.Any(i => i.Date >= fromDate))
            //.FirstOrDefault();
            //var abc = _context.StudentCourses.Include(c => c.Student).ToList();

            var student = _context.StudentCourses.Include(c => c.Student).Include(s => s.Course).ToList();
            return Ok(student);
        }
        /// <summary>
        /// GetStudentByName is used to get the information of a student by his name 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("GetStudentByName")]
        public IActionResult GetStudentByName(string name)
        {
            var studentDetail = _context.StudentCourses.Include(c => c.Student).Include(s => s.Course).Where(s=>s.Student.Name==name).ToList();
            return Ok(studentDetail);
        }
        /// <summary>
        /// The Following method is used for adding new student in the database
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddNew(StudentVM student)
        {

            Student studentDetails = new Student()
            {
                Name = student.StudentName
            };
            var abc = _context.Students.Add(studentDetails);
            _context.SaveChanges();
            var studentList = _context.Students.ToList();
            var tId = studentList[studentList.Count - 1].Id;


            StudentCourse studentCourse = new StudentCourse()
            {
                StudentId = tId,
                CourseId = student.CourseId
            };

            _context.StudentCourses.Add(studentCourse);

            _context.SaveChanges();
            return Ok();



            //Student studentDetails = new Student()
            //{
            //    Name = student.Student.Name
            //};
            //var abc = _context.Students.Add(studentDetails);
            //_context.SaveChanges();

            //var studentList = _context.Students.ToList();
            //var tId = studentList[studentList.Count - 1].Id;


            //StudentCourse studentCourse = new StudentCourse()
            //{
            //    StudentId = tId,
            //    CourseId = student.CourseId
            //};

            //_context.StudentCourses.Add(studentCourse);

            //_context.SaveChanges();

        }
        /// <summary>
        /// update method is used to update the existing student record
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPatch]
        public IActionResult Update(StudentUpdateVM student)
        {
            Student studentDetails = new Student()
            {
                Id = student.studentId,
                Name = student.name
            };

            var abc = _context.Students.Update(studentDetails);
            _context.SaveChanges();



            //var TCourses = _context.TeacherCourses.Where(tc => tc.TeacherId == teacher.Teacher.Id);
            //List<Course> TC = new();
            //foreach (var t in TCourses)
            //{
            //    var data = _context.Courses.FirstOrDefault(tc => tc.Id == t.CourseId);
            //    TC.Add(data);
            //}


            StudentCourse studentCourse = new StudentCourse()
            {
                Id = student.id,
                StudentId = student.studentId,
                CourseId = student.courseId
            };

            _context.StudentCourses.Update(studentCourse);

            _context.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// Delete method is used to delete the existing record of a student from database
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int studentId)
        {
            Student student = _context.Students.Find(studentId);
            var studRef = _context.StudentCourses.Where(s => s.StudentId == studentId).ToList();
            _context.Remove(student);
            //_context.Remove(studRef);
            _context.RemoveRange(studRef);
            _context.SaveChanges();
            return Ok();
        }
    }
}
