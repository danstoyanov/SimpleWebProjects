using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static SMS.Data.DataConstants;

namespace SMS.Data.Models
{
    public class Product
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Name { get; init; }

        [Range(0.05, 1000)]
        public decimal Price { get; init; }

        public string CartId { get; set; }

        public Cart Cart { get; init; }
    }
}