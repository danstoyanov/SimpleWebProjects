using System;
using System.ComponentModel.DataAnnotations;

using static Git.Data.DataConstants;

namespace Git.Data.Models
{
    public class Commit
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public string RepositoryId { get; set; }

        public Repository Repository { get; set; }
    }
}