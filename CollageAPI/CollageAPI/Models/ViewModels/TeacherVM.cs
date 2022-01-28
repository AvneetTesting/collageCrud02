using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollageAPI.Models.ViewModels
{
    public class TeacherVM
    {
        //public TeacherCourse TcourseSelecte = new TeacherCourse()
        //{
        //    CourseId=0
        //};

        //public Teacher TechDetails = new Teacher()
        //{
        //    Name = ""
        //};

        public string TeacherName { get; set; }
        public int CourseId { get; set; }
    }
}
