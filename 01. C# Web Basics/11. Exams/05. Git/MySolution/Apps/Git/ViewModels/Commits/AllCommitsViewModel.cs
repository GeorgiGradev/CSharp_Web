using System.Collections.Generic;

namespace Git.ViewModels.Commits
{
    public class AllCommitsViewModel
    {
        public IEnumerable<CommitViewModel> Commits { get; set; }
    }
}
