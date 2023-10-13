using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Security.Policy;

namespace FrituurOpDeHoekAPI.Models
{
    public class User
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public virtual ICollection<OldOrder>? OldOrders { get; set; }

        public virtual ICollection<Order>? Orders { get; set;}
    }
}
