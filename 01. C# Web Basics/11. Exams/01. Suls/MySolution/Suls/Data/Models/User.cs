using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Suls.Data.Models
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Submissions = new HashSet<Submission>();
            this.Problems = new HashSet<Problem>();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }

        public virtual ICollection<Problem> Problems { get; set; }
    }
}
