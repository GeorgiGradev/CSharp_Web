using Git.Data;
using Git.Data.Models;
using Git.ViewModels.Commits;
using System;
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

        public void CreateCommit(string userId, string repositoryId, CreateCommitInputModel model)
        {
            var commit = new Commit
            {
                Description = model.Description,
                CreatedOn = DateTime.UtcNow,
                CreatorId = userId,
                RepositoryId = repositoryId,
            };

            this.db.Commits.Add(commit);
            this.db.SaveChanges();
        }

        public void DeleteCommit(string commitId)
        {
            var commit = this.db.Commits
                .Where(x => x.Id == commitId)
                .FirstOrDefault();

            this.db.Commits.Remove(commit);
            this.db.SaveChanges();
        }

        public AllCommitsViewModel GetAllCommits(string userId)
        {
            var viewModel = new AllCommitsViewModel
            {
                Commits = this.db.Commits
                    .Where(x => x.CreatorId == userId)
                    .Select(x => new CommitViewModel
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Repository = x.Repository.Name,
                        CreatedOn = x.CreatedOn.ToString(),
                    }).ToList()
            };

            return viewModel;
        }
    }
}
