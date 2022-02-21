using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static SMS.Data.DataConstants;

namespace SMS.Data.Models
{
    public class Cart
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string UserId { get; init; }

        public User User { get; init; }

        public List<Product> Products { get; init; } = new List<Product>();
    }
}