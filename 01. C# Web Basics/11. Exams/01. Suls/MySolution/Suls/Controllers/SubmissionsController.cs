using Suls.Services.Problems;
using Suls.Services.Submissions;
using Suls.ViewModels.Submissions;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Suls.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionsService submissionsService;
        private readonly IProblemsService problemsService;

        public SubmissionsController(
            ISubmissionsService submissionsService,
            IProblemsService problemsService)
        {
            this.submissionsService = submissionsService;
            this.problemsService = problemsService;
        }

        public HttpResponse Create(string id)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var name = this.problemsService.GetNameById(id);
            var viewModel = new SubmissionCreateViewModel
            {
                Name = name,
                ProblemId = id
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string problemId, SubmissionInputModel model)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();

            if (string.IsNullOrEmpty(model.Code)
                    || model.Code.Length < 30
                    || model.Code.Length > 800)
            {
                return this.Redirect("/Submissions/Create");
            }

            this.submissionsService.CreateSubmission(problemId, userId, model);

            return this.Redirect("/");
        }

        public HttpResponse Delete(string id)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            this.submissionsService.DeleteSubmission(id);
            return this.Redirect("/");
        }
    }
}
