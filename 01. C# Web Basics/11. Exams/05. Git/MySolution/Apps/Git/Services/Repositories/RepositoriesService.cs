using Git.Data;
using Git.Data.Models;
using Git.Services.Users;
using Git.ViewModels.Repositories;
using System;
using System.Linq;

namespace Git.Services.Repositories
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void CreateRepository(string userId, RepositoryInputModel input)
        {
            var repository = new Repository
            {
                Name = input.Name,
                OwnerId = userId,
                CreatedOn = DateTime.UtcNow,
                IsPublic = input.RepositoryType == "Public" ? true : false,
            };

            this.db.Repositories.Add(repository);
            this.db.SaveChanges();
        }

        public AllRepositoriesViewModel GetAllRepositories()
        {
            var viewModel = new AllRepositoriesViewModel
            {
                AllRepositoryViewModels = this.db.Repositories
                                .Where(x => x.IsPublic == true)
                                .Select(x => new RepositoryViewModel
                                {
                                    RepoName = x.Name,
                                    OwnerName = x.Owner.Username,
                                    CreatedOn = x.CreatedOn.ToString(),
                                    Commits = x.Commits.Count(),
                                    Id = x.Id
                                }).ToList()
            };

            return viewModel;
        }

        public string GetRepositoryName(string id)
        {
            var repositoryName = this.db.Repositories
                .Where(x => x.Id == id)
                .Select(x => x.Name)
                .FirstOrDefault();

            return repositoryName;
        }
    }
}
