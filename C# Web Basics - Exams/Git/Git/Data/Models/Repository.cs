using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static Git.Data.DataConstants;

namespace Git.Data.Models
{
    public class Repository
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(RepoNameMaxLegnth)]
        public string Name { get; init; } 

        [Required]
        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;

        [Required]
        public bool IsPublic { get; set; }  

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public List<Commit> Commits { get; set; } = new List<Commit>();

    }
}