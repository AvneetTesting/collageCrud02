using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollageAPI.Models.ViewModels
{
    public class TeacherUpdateVM
    {
        public string name { get; set; }
        public int courseId { get; set; }
        public int id { get; set; }
        public int teacherId { get; set; }
    }
}
