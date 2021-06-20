using Suls.Data.Models;
using Suls.ViewModels.Problems;
using SulsApp.Data;
using System;
using System.Linq;

namespace Suls.Services.Problems
{
    public class ProblemsService : IProblemsService
    {
        private readonly ApplicationDbContext db;

        public ProblemsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateProblem(string userId, CreateProblemInputModel model)
        {
            var problem = new Problem
            {
                Name = model.Name,
                Points = model.Points,
                UserId = userId,
            };
            ;
            this.db.Add(problem);
            this.db.SaveChanges();
        }

        public AllProblemDetailsViewModel GetAllProblemDetails(string problemId)
        {
            var problemName = this.db.Problems
            .Where(x => x.Id == problemId)
            .Select(x => x.Name)
            .FirstOrDefault();

            var viewModel = new AllProblemDetailsViewModel
            {
                ProblemName = problemName,
                AllProblemDetails = this.db.Submissions
                .Where(x => x.ProblemId == problemId)
                    .Select(x => new ProblemDetailsViewModel
                    {

                        Username = x.User.Username,
                        CreatedOn = DateTime.UtcNow,
                        MaxPoints = x.Problem.Points,
                        AchievedResult = x.AchievedResult,
                        SubmissionId = x.Id,
                    }).ToList()
            };
            ;
            return viewModel;
        }

        public AllProblemsViewModel GetAllProblems()
        {
            var viewModel = new AllProblemsViewModel
            {
                AllProblems = this.db.Problems
                .Select(x => new ProblemViewModel
                {
                    Name = x.Name,
                    Count = x.Submissions.Count,
                    Id = x.Id
                }).ToList()
            };

            return viewModel;
        }

        public string GetNameById(string id)
        {
            var name = this.db.Problems
                .Where(x => x.Id == id)
                .Select(x => x.Name)
                .FirstOrDefault();

            return name;

        }
    }
}
