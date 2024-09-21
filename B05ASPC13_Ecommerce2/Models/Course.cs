using System.ComponentModel.DataAnnotations.Schema;

namespace B05ASPC13_Ecommerce2.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Category")]
        public int CatId { get; set; }

        public virtual Category? Category { get; set; }

    }
}
