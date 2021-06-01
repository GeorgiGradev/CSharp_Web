using Suls.Data;
using Suls.ViewModels.Problems;
using Suls.ViewModels.Users;
using SulsApp.Data;
using System.Collections.Generic;
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

        public void CreateProblem(CreateProblemInputModel problemModel, string id)
        {
            var problem = new Problem
            {
                Name = problemModel.Name,
                Points = problemModel.Points,
                UserId = id
            };

            this.db.Problems.Add(problem);
            this.db.SaveChanges();
        }

        public ICollection<AllProblemsViewModel> GetAllProblems()
        {
            var allProblemsViewModel = this.db
                .Problems
                .Select(x => new AllProblemsViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Count = x.Submissions.Count()
                })
                .ToList();

            return allProblemsViewModel;
        }

        public string GetNameById(string id)
        {
            var problemName = this.db.Problems
                .Where(x => x.Id == id)
                .Select(x => x.Name)
                .FirstOrDefault();
            
            return problemName;
        }


        public ProblemViewModel GetById(string id)
        {
            return this.db.Problems.Where(x => x.Id == id)
                .Select(x => new ProblemViewModel
                {
                    Name = x.Name,
                    Submissions = x.Submissions.Select(s => new SubmissionViewModel
                    {
                        CreatedOn = s.CreatedOn,
                        SubmissionId = s.Id,
                        AchievedResult = s.AchievedResult,
                        Username = s.User.Username,
                        MaxPoints = x.Points,
                    }).ToList()
                }).FirstOrDefault();
        }
    }
}
