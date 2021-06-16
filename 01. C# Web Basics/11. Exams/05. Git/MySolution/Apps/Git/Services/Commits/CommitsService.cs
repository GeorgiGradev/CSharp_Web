using Git.Data;
using Git.Data.Models;
using Git.ViewModels.Commits;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services.Commits
{
    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string description, string repositoryId, string userId)
        {
            var commit = new Commit()
            {
                CreatedOn = DateTime.UtcNow,
                Description = description,
                CreatorId = userId,
                RepositoryId = repositoryId
            };

            this.db.Commits.Add(commit);
            this.db.SaveChanges();
        }

        public bool Delete(string id, string userId)
        {
            var commit = this.db
                .Commits
                .FirstOrDefault(c => c.Id == id);

            if (commit.CreatorId != userId)
            {
                return false;
            }

            this.db.Commits.Remove(commit);

            this.db.SaveChanges();

            return true;
        }

        public IEnumerable<AllCommitsViewModel> GetAll(string userId)
        {
            var viewModel = this.db.Commits
                .Where(x => x.CreatorId == userId)
                .Select(x => new AllCommitsViewModel
                {
                    Id = x.Id,
                    Repository = x.Repository.Name,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn.ToString()
                })
                .ToList();

            return viewModel;
        }
    }
}
