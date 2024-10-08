using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B05ASPC13_Ecommerce2.Models
{
    public class CourseLesson
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string? Name { get; set; }
        [StringLength(150)]
        public string? Description { get; set; }
        [StringLength(150)]
        public string? VideoPath { get; set; }
        [NotMapped]
        public IFormFile? VideoFile { get; set; }
        [ForeignKey("Course")]
        public int SectionId { get; set; }
        public virtual CourseSection? CourseSection { get; set; }
    }
}
