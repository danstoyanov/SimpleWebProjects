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
        public string StartPoint { get; set; }

        [Required]
        public string EndPoint { get; set; }

        [Required]
        public DateTime DepartureTime { get; init; } = DateTime.UtcNow;

        [Range(SeatsMinRange, SeatsMaxRange)]
        public int Seats { get; set; }

        [Required]
        [MaxLength(SeatDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        public IEnumerable<UserTrip> UserTrips { get; init; } = new List<UserTrip>();
    }
}
