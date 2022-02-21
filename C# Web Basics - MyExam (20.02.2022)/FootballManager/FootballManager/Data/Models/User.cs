using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static FootballManager.Data.DataConstants;

namespace FootballManager.Data.Models
{
    public class User
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Username { get; set; }

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public List<UserPlayer> UserPlayers { get; set; } = new List<UserPlayer>();
    }
}
//•	Has a Password – a string with min length 5 and max length 20 (before hashed)
//-no max length required for a hashed password in the database (required)

//•	Has UserPlayers collection
