using System;

namespace Suls.ViewModels.Problems
{
    public class ProblemDetailsViewModel
    {
        public string SubmissionId { get; set; }

        public string Username { get; set; }

        public int AchievedResult { get; set; }

        public int MaxPoints { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
