using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static FootballManager.Data.DataConstants;

namespace FootballManager.Data.Models
{
    public class Player
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(PositionMaxLength)]
        public string Position { get; set; }

        [Required]
        [Range(MinSpeed, MaxSpeed)]
        public byte Speed { get; set; }
        
        [Required]
        [Range(MinEndurance, MaxEndurance)]
        public byte Endurance { get; set; }

        [Required]
        [MaxLength(PlayerDescriptinMaxLength)]
        public string Description { get; set; }

        public List<UserPlayer> UserPlayers { get; set; } = new List<UserPlayer>();
    }
}