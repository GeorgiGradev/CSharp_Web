using System;

namespace Suls.ViewModels.Problems
{
    public class SubmissionViewModel
    {
        public string Username { get; set; }

        public int AchievedResult { get; set; }

        public int MaxPoints { get; set; }

        public DateTime CreatedOn { get; set; }

        public string SubmissionId { get; set; }

        public int Percentage => (int)Math.Round(this.AchievedResult * 100M / MaxPoints);
    }
}
