using System.Collections.Generic;

namespace Suls.ViewModels.Problems
{
    public class AllProblemDetailsViewModel
    {
        public IEnumerable<ProblemDetailsViewModel> AllProblemDetails { get; set; }

        public string ProblemName { get; set; }
    }
}
