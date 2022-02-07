using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollageAPI.Models.ViewModels
{
    public class StudentTestingAddNewVM
    {
        public int Id { get; set; }
        public string name { get; set; }
        public Course[] courseInfo { get; set; }
    }
}
