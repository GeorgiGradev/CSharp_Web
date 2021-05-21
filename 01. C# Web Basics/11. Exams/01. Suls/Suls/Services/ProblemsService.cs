using Suls.Data;
using Suls.ViewModels.Problems;
using SulsApp.Data;
using System.Collections.Generic;
using System.Linq;

namespace Suls.Services
{
    public class ProblemsService : IProblemsService
    {
        private readonly ApplicationDbContext db;

        public ProblemsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, int points) 
        {
            var problem = new Problem { Name = name, Points = points }; // ще създава нова задача
            this.db.Problems.Add(problem); // добавяме го в dbSet<Problem> Problems
            db.SaveChanges(); // запазваме в базата данни
        }

        public IEnumerable<HomePageProblemViewModel> GetAll()
        {
            var problems = db.Problems.Select(x => new HomePageProblemViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Count = x.Submissions.Count()
            })
            .ToList();

            return problems;
        }

        public string GetNameById(string id)
        {
            var problem = this.db.Problems.FirstOrDefault(x => x.Id == id);
            return problem?.Name;
        }

        public ProblemViewModel GetById (string id)
        {
            return this.db.Problems.Where(x => x.Id == id)
                .Select(x => new ProblemViewModel
                { 
                    Name = x.Name,
                    Submissions = x.Submissions.Select(s=> new SubmissionViewModel
                    { 
                        CreatedOn = s.CreatedOn,
                        SubmissionId = s.Id,
                        AchievedResult = s.AchievedResult,
                        Username = s.User.Username,
                        MaxPoints = x.Points
                    })
                }).FirstOrDefault();
        }

    }
}
