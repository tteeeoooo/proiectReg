using System.ComponentModel.DataAnnotations;
using Store.Models;

namespace Store.Models
{
    public class Product
    {
        [Key]
        //[MaxLength(255)]
        public int Id { get; set; }
        [Required(ErrorMessage = "The product name is required")]
        [StringLength(100, ErrorMessage = "The product name cannot exceed 100 characters.")]
        public string Name { get; set; }
        [StringLength(1000, ErrorMessage = "The description cannot exceed 1000 characters.")]
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public float Price { get; set; }
        public DateTime DateListed { get; set; } = DateTime.Now;
        public string Brand { get; set; }
        [Required(ErrorMessage = "Adding the product category is required")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}