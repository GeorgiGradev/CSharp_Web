using CarShop.ViewModels.Issues;

namespace CarShop.Services.Issues
{
    public interface IIssuesService
    {
        AllIssuesViewModel GetAllIssues(string carId, string userId);

        void CreateIssue(string carId, IssueInputModel model);

        void DeleteIssue(string issueId);

        void FixIssue(string issueId);
    }
}
