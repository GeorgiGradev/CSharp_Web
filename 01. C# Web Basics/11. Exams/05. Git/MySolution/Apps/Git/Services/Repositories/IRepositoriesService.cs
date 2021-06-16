using Git.ViewModels.Repositories;
using System.Collections.Generic;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        public void CreateRepository(string name, string type, string userId);


        public IEnumerable<AllRepositoriesViewModel> GetAll();

        public string GetRepositoryName(string id);
    }
}
