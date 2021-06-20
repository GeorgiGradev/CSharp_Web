using Suls.ViewModels.Problems;

namespace Suls.Services.Problems
{
    public interface IProblemsService
    {
        public void CreateProblem(string userId, CreateProblemInputModel model);

        AllProblemsViewModel GetAllProblems();

        public AllProblemDetailsViewModel GetAllProblemDetails(string problemId);

        string GetNameById(string id);
    }
}
