using CollageAPI.Data;
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
    public class StudentTestingController : Controller
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
        public IActionResult AddNew(StudentTestingAddNewVM student)
        {
            Student studentDetails = new Student() 
            { 
                Name=student.name
            };

            _context.Students.Add(studentDetails);
            _context.SaveChanges();

            var studentList = _context.Students.ToList();
            var tId = studentList[studentList.Count - 1].Id;

            foreach (var item in student.courseInfo)
            {
                StudentCourse studentCourse = new StudentCourse()
                {
                    StudentId = tId,
                    CourseId = item.Id
                };
                _context.StudentCourses.Add(studentCourse);
                _context.SaveChanges();
            }
            //StudentCourse studentCourse = new StudentCourse()
            //{
            //    StudentId = tId,
            //    CourseId = student.CourseId
            //};

            //_context.StudentCourses.Add(studentCourse);

            //_context.SaveChanges();
            return Ok();
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
        private class GetStudentData
        {
            public int ID { get; set; }
            public string name { get; set; }
            public int stdCrsId { get; set; }
            public string courseName { get; set; }
            public string teacher { get; set; }
        }

        private class GetStudentDataTesting
        {
            public int Id { get; set; }
            public string StudentName { get; set; }
            public string StudentCourse { get; set; }
            public string Teacher { get; set; }
        }
        [HttpGet("LinqPractise")]
        public IActionResult LinqPractise()
        {
            var testObj = (from std in _context.Students
                          join stdCrs in _context.StudentCourses
                          on std.Id equals stdCrs.StudentId
                          join stdTech in _context.TeacherCourses
                          on stdCrs.CourseId equals stdTech.CourseId
                          where std.Name=="Raman"
                          select new GetStudentData {
                          ID=std.Id,
                          name=std.Name,
                          stdCrsId=stdCrs.Id,
                          courseName=stdCrs.Course.Name,
                          teacher=stdTech.Teacher.Name
                          }).ToList();

            //var newTestObj =(from stdCrs in _context.StudentCourses
            //                join techCrs in _context.TeacherCourses
            //                on stdCrs.CourseId equals techCrs.CourseId
            //                where stdCrs.StudentId==1
            //                select new GetStudentData { 
            //                }).ToList();

            var newTestObj = (from std in _context.Students
                              join stdCrs in _context.StudentCourses
                              on std.Id equals stdCrs.StudentId
                              join techCrs in _context.TeacherCourses
                              on stdCrs.CourseId equals techCrs.CourseId
                              join tech in _context.Teachers
                              on techCrs.TeacherId equals tech.Id
                              select new GetStudentDataTesting { 
                                  Id=std.Id,
                                  StudentName=std.Name,
                                  StudentCourse=stdCrs.Course.Name,
                                  Teacher=tech.Name
                              }).ToList();

            return Ok(newTestObj);
        }
        private class GetTeachersCourseList
        {
            public int Id { get; set; }
            public string TeacherName { get; set; }
            public string TeacherCourses { get; set; }
        }
    }
}