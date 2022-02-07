using CollageAPI.Data;
using CollageAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// GetAll method is used for retreiving the list of courses 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {

            return Ok(_context.Courses.ToList());
        }
        /// <summary>
        /// AddNew method is used for adding new course in database
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddNew(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return Ok("Added New");
        }
        /// <summary>
        /// Update method is used to update the existing course information
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPatch]
        public IActionResult Update(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
            return Ok("Update");
        }
        /// <summary>
        /// Delete method is used to delete any existing record
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int courseId)
        {
            Course course = _context.Courses.Find(courseId);
            _context.Remove(course);
            _context.SaveChanges();
            return Ok("Delete");
        }
    }
}
