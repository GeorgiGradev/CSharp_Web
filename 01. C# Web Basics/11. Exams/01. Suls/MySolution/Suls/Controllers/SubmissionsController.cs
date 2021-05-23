using Suls.Services;
using Suls.ViewModels.Submissions;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Suls.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly IProblemsService problemsService;
        private readonly ISubmissionsService submissionsService;

        public SubmissionsController(
            IProblemsService problemsService,
            ISubmissionsService submissionsService)
        {
            this.problemsService = problemsService;
            this.submissionsService = submissionsService;
        } 

        public HttpResponse Create(string id)
        {
            var viewModel = new CreateViewModel
            { 
                ProblemId = id,
                Name = problemsService.GetNameById(id),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create (string problemId, string code)
        {
            if (string.IsNullOrEmpty(code)
                || code.Length < 30
                | code.Length > 800)
            {
                return this.Error("Text should be between 30 and 800 characters");
            }


            var userId = this.GetUserId();
            this.submissionsService.Create(problemId, userId, code);
            return this.Redirect("/");
        }

        public HttpResponse Delete(string id)
        {
             this.submissionsService.Delete(id);
            return this.Redirect("/");
        }
    }
}
