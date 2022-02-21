using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static SMS.Data.DataConstants;

namespace SMS.Data.Models
{
    public class User
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Username { get; init; }

        [Required]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }

        public string CartId { get; init; }

        public Cart Cart { get; set; }
    }
}