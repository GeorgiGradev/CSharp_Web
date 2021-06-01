using Suls.ViewModels.Problems;
using System.Collections.Generic;

namespace Suls.Services
{
    public interface IProblemsService
    {
        public void CreateProblem(CreateProblemInputModel problemModel, string id);

        public ICollection<AllProblemsViewModel> GetAllProblems();

        public string GetNameById(string id);

        public ProblemViewModel GetById(string id);
    }
}
