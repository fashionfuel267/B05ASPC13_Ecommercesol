using System.ComponentModel.DataAnnotations.Schema;

namespace B05ASPC13_Ecommerce2.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Picpath { get; set; }
        [NotMapped]
        public IFormFile Pic { get; set; }
        public string Bio { get; set; }
    }
}
