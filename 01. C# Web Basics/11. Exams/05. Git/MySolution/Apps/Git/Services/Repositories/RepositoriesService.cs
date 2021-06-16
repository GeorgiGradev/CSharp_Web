using Git.Data;
using Git.Data.Models;
using System;
using SUS.MvcFramework;
using System.Collections.Generic;
using Git.ViewModels.Repositories;
using System.Linq;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;
        private readonly IUsersService usersService;

        public RepositoriesService(ApplicationDbContext db, IUsersService usersService)
        {
            this.db = db;
            this.usersService = usersService;
        }

        public void CreateRepository(string name, string type, string userId)
        {
            var isPublic = type == "Public";

            var repository = new Repository
            {
                Name = name,
                CreatedOn = DateTime.Now,
                IsPublic = isPublic,
                OwnerId = userId
            };

            this.db.Repositories.Add(repository);
            this.db.SaveChanges();

        }

        public IEnumerable<AllRepositoriesViewModel> GetAll()
        {
            var viewModel = this.db.Repositories
                .Where(x => x.IsPublic)
                .Select(x => new AllRepositoriesViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOn = x.CreatedOn.ToString(),
                    Owner = x.Owner.Username,
                    CommitsCount = x.Commits.Count()
                })
                .ToList();

            return viewModel;
        }

        public string GetRepositoryName(string id)
        {
            var repository = this.db.Repositories.FirstOrDefault(x => x.Id == id);

            return repository.Name;
        }
    }
}

