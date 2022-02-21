using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static SharedTrip.Data.DataConstants;

namespace SharedTrip.Data.Models
{
    public class Trip
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string StartPoint { get; init; }

        [Required]
        public string EndPoint { get; init; }

        public DateTime DepartureTime { get; init; } = DateTime.UtcNow;

        [Required]
        [Range(2, 6)]
        public int Seats { get; init; }

        [Required]
        [MaxLength(80)]
        public string Description { get; init; }

        public string ImagePath { get; init; }

        public List<UserTrip> UserTrips { get; init; } = new List<UserTrip>();
    }
}