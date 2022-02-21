using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static CarShop.Data.DataConstants;

namespace CarShop.Data.DataModels
{
    public class Car
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string PlateNumber { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public List<Issue> Issues { get; set; } = new List<Issue>();
    }
}