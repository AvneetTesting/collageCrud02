using CollageAPI.Data;
using CollageAPI.Models;
using CollageAPI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class TeachersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TeachersController(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// GetAll method is used to get the list of the Teachers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var teacher = _context.TeacherCourses.Include(c => c.Course).Include(t => t.Teacher).OrderBy(t=>t.Teacher.Id).ToList();
            //_context.Teachers.Include("course").ToList();
            return Ok(teacher);
            //_context.Teachers.ToList()
        }
        /// <summary>
        /// AddNew method is used to add new teacher in database
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddNew(TeacherVM teacher)
        {
            //Teacher details saving
            Teacher teacherDetails = new Teacher()
            {
                Name= teacher.TeacherName
            };
            _context.Teachers.Add(teacherDetails);
            _context.SaveChanges();
            var teacherList = _context.Teachers.ToList();
            var tId = teacherList[teacherList.Count - 1].Id;
            //Relation saving
            TeacherCourse teacherCourse = new TeacherCourse()
            {
                TeacherId = tId,
                CourseId = teacher.CourseId
            };
            _context.TeacherCourses.Add(teacherCourse);

            _context.SaveChanges();
            return Ok();

            //Teacher teacherDetails = new Teacher() { 
            //Name=teacher.Teacher.Name
            //};
            //var abc=_context.Teachers.Add(teacherDetails);
            //_context.SaveChanges();
            //var teacherList = _context.Teachers.ToList();
            //var tId = teacherList[teacherList.Count-1].Id;



            //TeacherCourse teacherCourse = new TeacherCourse()
            //{
            //    TeacherId = tId,
            //    CourseId=teacher.CourseId
            //};

            //_context.TeacherCourses.Add(teacherCourse);

            //_context.SaveChanges();

        }
        /// <summary>
        /// Update method will help to update the information of existing teacher in database
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPatch]
        public IActionResult Update(TeacherUpdateVM teacher)
        {
            Teacher teacherDetails = new Teacher()
            {
                Id=teacher.teacherId,
                Name = teacher.name
            };

            _context.Teachers.Update(teacherDetails);
            _context.SaveChanges();

            

            //var TCourses = _context.TeacherCourses.Where(tc => tc.TeacherId == teacher.Teacher.Id);
            //List<Course> TC = new();
            //foreach (var t in TCourses)
            //{
            //    var data = _context.Courses.FirstOrDefault(tc => tc.Id == t.CourseId);
            //    TC.Add(data);
            //}


            TeacherCourse teacherCourse = new TeacherCourse()
            {
                Id=teacher.id,
                TeacherId = teacher.teacherId,
                CourseId = teacher.courseId
            };

            _context.TeacherCourses.Update(teacherCourse);

            _context.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// Delete method will help to delete the record of a teacher from record 
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int teacherId)
        {

            Teacher teacher = _context.Teachers.Find(teacherId);
            var teachRef = _context.TeacherCourses.Where(s => s.TeacherId == teacherId).ToList();
            _context.Remove(teacher);
            _context.RemoveRange(teachRef);
            _context.SaveChanges();
            return Ok();

            //var employee = (from emp in _context.tbemployee
            //                join dsg in _context.tbdsg
            //                on emp.dsgId equals dsg.dsgId
            //                where emp.empId.Equals(empId)
            //                select new
            //                {
            //                    emp.empId,
            //                    emp.ename,
            //                    emp.esal,
            //                    emp.eadd,
            //                    dsg.dsgId,
            //                    dsg.dsgname
            //                }).ToList();

        }
        /// <summary>
        /// GetTeacherByName will help to get the information of a teacher by its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("GetTeacherByName")]
        public IActionResult GetTeacherByName(string name)
        {
            var teacherDetail = _context.TeacherCourses.Include(c => c.Teacher).Include(s => s.Course).Where(s => s.Teacher.Name == name).ToList();
            return Ok(teacherDetail);
        }
    }
}