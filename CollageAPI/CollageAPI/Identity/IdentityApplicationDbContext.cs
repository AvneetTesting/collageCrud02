using CollageAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollageAPI.Identity
{
    public class IdentityApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public IdentityApplicationDbContext(DbContextOptions<IdentityApplicationDbContext>options):base(options)
        {

        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<TeacherCourse> TeacherCourses { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
