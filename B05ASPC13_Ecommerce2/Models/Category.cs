using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace B05ASPC13_Ecommerce2.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        [ValidateNever]
        public ICollection<Course> Courses { get; set; }
    }

}
