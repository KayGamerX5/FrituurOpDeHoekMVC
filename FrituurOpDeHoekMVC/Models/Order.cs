using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrituurOpDeHoekMVC.Models
{
    public class Order
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        public string? ReadynessState { get; set; }

        public string? ReadynessEstimateTimer { get; set; }

        public virtual ICollection<Product>? Products { get; set; }

        public virtual User? User { get; set; }

        public int? UserId { get; set; }
    }
}
