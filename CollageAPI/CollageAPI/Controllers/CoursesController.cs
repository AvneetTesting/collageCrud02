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
        [HttpGet]
        public IActionResult GetAll()
        {

            return Ok(_context.Courses.ToList());
        }
        [HttpPost]
        public IActionResult AddNew(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return Ok("Added New");
        }
        [HttpPatch]
        public IActionResult Update(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
            return Ok("Update");
        }
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
