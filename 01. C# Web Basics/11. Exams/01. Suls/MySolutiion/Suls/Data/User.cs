using System;
using System.Collections.Generic;

namespace Suls.Data
{
    public class User
    {
        public User()
        {
            this.Id  = Guid.NewGuid().ToString();
            this.Submissions = new HashSet<Submission>();
            this.Problems = new HashSet<Problem>();
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }

        public virtual ICollection<Problem> Problems { get; set; }
    }
}
