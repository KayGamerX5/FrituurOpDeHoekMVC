using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrituurOpDeHoekAPI.Models
{
    public class Product
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        public virtual Category? Category { get; set; }

        public int? CategoryId { get; set; }
        
        public virtual ICollection<Sale>? Sales { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<OldOrder>? OldOrders { get; set; }
    }
}
