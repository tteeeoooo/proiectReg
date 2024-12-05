using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Models;

namespace Store.Models
{
    public class Review
    {
        [Key]
        //[MaxLength(255)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Must enter the review content")]
        public string Content { get; set; }
        public int Grade {get; set;}
        public DateTime Date { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}