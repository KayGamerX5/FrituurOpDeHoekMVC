using System.ComponentModel.DataAnnotations;

namespace FrituurOpDeHoekMVC.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
