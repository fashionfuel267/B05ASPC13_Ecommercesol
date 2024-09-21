using System.ComponentModel.DataAnnotations.Schema;

namespace B05ASPC13_Ecommerce2.Models
{
    public class CourseInstructor
    {
        public int Id { get; set; }
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        [ForeignKey("Instructor")]
        public int InstructorID { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual Course Course { get; set; }
    }
}
