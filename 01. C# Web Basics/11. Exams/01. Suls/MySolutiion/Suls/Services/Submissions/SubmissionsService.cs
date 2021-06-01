using Suls.Data;
using Suls.ViewModels.Submissions;
using SulsApp.Data;
using System;
using System.Linq;

namespace Suls.Services.Submissions
{
    class SubmissionsService : ISubmissionsService
    {
        private readonly ApplicationDbContext db;
        private readonly Random random;

        public SubmissionsService(ApplicationDbContext db, Random random)
        {
            this.db = db;
            this.random = random;
        }

        public void Create(SubmissionsCreateInputModel model, string userId)
        {

            var problemMaxPoints = this.db.Problems.Where(x => x.Id == model.ProblemId)
                .Select(x => x.Points).FirstOrDefault();
            var submission = new Submission
            {
                Code = model.Code,
                ProblemId = model.ProblemId,
                AchievedResult = this.random.Next(0, problemMaxPoints + 1),
                CreatedOn = DateTime.UtcNow,
                UserId = userId
            };

            this.db.Submissions.Add(submission);
            this.db.SaveChanges();
        }

        public void Delete(string id)
        {
            var submission = this.db.Submissions.Find(id);
            this.db.Submissions.Remove(submission);
            this.db.SaveChanges();
        }
    }
}
