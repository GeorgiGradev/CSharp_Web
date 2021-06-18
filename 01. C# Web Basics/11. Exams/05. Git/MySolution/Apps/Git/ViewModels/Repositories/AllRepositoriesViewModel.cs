using System.Collections.Generic;

namespace Git.ViewModels.Repositories
{
    public class AllRepositoriesViewModel
    {
        public IEnumerable<RepositoryViewModel> AllRepositoryViewModels { get; set; }
    }
}
