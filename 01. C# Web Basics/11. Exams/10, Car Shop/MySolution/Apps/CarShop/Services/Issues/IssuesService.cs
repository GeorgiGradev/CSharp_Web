using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels.Issues;
using System;
using System.Linq;

namespace CarShop.Services.Issues
{
    public class IssuesService : IIssuesService
    {
        private readonly ApplicationDbContext db;

        public IssuesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateIssue(string carId, IssueInputModel model)
        {
            var issue = new Issue
            { 
                CarId = carId,
                Description = model.Description,
                IsFixed = false
            };

            this.db.Issues.Add(issue);
            this.db.SaveChanges();
        }

        public void DeleteIssue(string issueId)
        {
            var issue = this.db.Issues
                .Where(x => x.Id == issueId)
                .FirstOrDefault();

            this.db.Remove(issue);
            this.db.SaveChanges();

        }

        public void FixIssue(string issueId)
        {
            var issue = this.db.Issues
                .Where(x => x.Id == issueId)
                .FirstOrDefault();

            issue.IsFixed = true;
            this.db.SaveChanges();
        }

        public AllIssuesViewModel GetAllIssues(string carId, string userId)
        {
            var carModel = this.db.Cars
                .Where(x=>x.Id == carId)
                .Select(x=>x.Model)
                .FirstOrDefault();

            var userRole = this.db.Users
                .Where(x => x.Id == userId)
                .Select(x => x.IsMechanic.ToString())
                .FirstOrDefault();

            var viewModel = new AllIssuesViewModel
            {
                Model = carModel,
                CarId = carId,
                UserRole = userRole,
                Issues = this.db.Issues
                .Where(x => x.CarId == carId)
                .Select(x => new IssueViewModel
                {
                    Description = x.Description,
                    IsFixed = x.IsFixed == true ? "Yes" : "Not yet",
                    IssueId = x.Id,
                }).ToList()
            };

            return viewModel;
        }

        public AllIssuesViewModel GetALlIssues(string carId)
        {
            throw new NotImplementedException();
        }
    }
}
