using CollageAPI.Data;
using CollageAPI.Models;
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
    public class StudentTestingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentTestingController(ApplicationDbContext context)
        {
            _context = context;
        }
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
        [HttpGet("GetStudentByName")]
        public IActionResult GetStudentByName(string name)
        {
            var studentDetail = _context.StudentCourses.Include(c => c.Student).Include(s => s.Course).Where(s => s.Student.Name == name).ToList();
            return Ok(studentDetail);
        }
        [HttpPost]
        public IActionResult AddNew(StudentCourse student)
        {
            //Teacher teacherDetails = new Teacher()
            //{
            //    Name = teacher.Teacher.Name
            //};
            //var abc = _context.Teachers.Add(teacherDetails);
            //_context.SaveChanges();
            //var teacherList = _context.Teachers.ToList();
            //var tId = teacherList[teacherList.Count - 1].Id;



            //TeacherCourse teacherCourse = new TeacherCourse()
            //{
            //    TeacherId = tId,
            //    CourseId = teacher.CourseId
            //};

            //_context.TeacherCourses.Add(teacherCourse);

            //_context.SaveChanges();
            //return Ok("Added New");


            Student studentDetails = new Student()
            {
                Name = student.Student.Name
            };
            _context.Students.Add(studentDetails);
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
            return Ok("Added New");
        }
        [HttpPatch]
        public IActionResult Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
            return Ok("Update");
        }
        [HttpDelete]
        public IActionResult Delete(int studentId)
        {
            Student student = _context.Students.Find(studentId);
            var studRef = _context.StudentCourses.Where(s => s.StudentId == studentId).ToList();
            _context.Remove(student);
            //_context.Remove(studRef);
            _context.RemoveRange(studRef);
            _context.SaveChanges();
            return Ok("Delete");
        }
    }
}

