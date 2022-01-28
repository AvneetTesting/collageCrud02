using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollageAPI.Models.ViewModels
{
    public class StudentVM
    {
        //public Student studentDisc = new Student()
        //{
        //    Id=0,
        //    Name=""
        //};
        //public StudentCourse studentSelectedCourses = new StudentCourse()
        //{
        //    Id = 0,
        //    CourseId = 0,
        //    StudentId = 0
        //};
        public string StudentName { get; set; }
        public int CourseId { get; set; }
    }
}
