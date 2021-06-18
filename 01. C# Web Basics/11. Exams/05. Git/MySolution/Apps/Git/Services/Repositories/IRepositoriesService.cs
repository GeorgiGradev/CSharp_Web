using Git.ViewModels.Repositories;

namespace Git.Services.Repositories
{
    public interface IRepositoriesService
    {
        public void CreateRepository(string userId, RepositoryInputModel input);

        AllRepositoriesViewModel GetAllRepositories();

        string GetRepositoryName(string id);
    }
}
