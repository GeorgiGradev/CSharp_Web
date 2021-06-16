using Git.ViewModels.Commits;
using System.Collections.Generic;

namespace Git.Services.Commits
{
    public interface ICommitsService
    {
        void Create(string description, string repositoryId, string userId);

        public IEnumerable<AllCommitsViewModel> GetAll(string userId);

        bool Delete(string id, string userId);

    }
}
