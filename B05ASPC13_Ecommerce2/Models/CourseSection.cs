using System.ComponentModel.DataAnnotations.Schema;

namespace B05ASPC13_Ecommerce2.Models
{
    public class CourseSection
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course? Course { get; set; }
    }
}
