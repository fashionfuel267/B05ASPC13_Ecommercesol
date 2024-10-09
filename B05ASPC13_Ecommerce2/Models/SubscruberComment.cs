using System.ComponentModel.DataAnnotations.Schema;

namespace B05ASPC13_Ecommerce2.Models
{
    public class SubscruberComment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("Subscriber")]
        public int SubscriberId { get; set; }
        public virtual Subscriber? Subscriber { get; set; }
    }
}
