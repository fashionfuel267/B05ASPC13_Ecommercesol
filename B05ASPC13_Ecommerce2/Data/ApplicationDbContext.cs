using B05ASPC13_Ecommerce2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace B05ASPC13_Ecommerce2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<CourseInstructor> CourseInstructor { get; set; }
        public DbSet<CourseSection> CourseSections { get; set; }
        public DbSet<CourseLesson> CourseLessons { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<SubscruberComment> SubscruberComments { get; set; }
    }
}
