using Suls.Services.Problems;
using Suls.ViewModels.Problems;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Suls.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemsService problemsService;

        public ProblemsController(IProblemsService problemsService)
        {
            this.problemsService = problemsService;
        }

        public HttpResponse Create()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateProblemInputModel model)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();

            if (string.IsNullOrEmpty(model.Name)
                    || model.Name.Length < 5
                    || model.Name.Length > 20)
            {
                return this.Redirect("/Problems/Create");
            }


            if (model.Points < 50 
                || model.Points > 300)
            {
                return this.Redirect("/Problems/Create");
            }

            this.problemsService.CreateProblem(userId, model);

            return this.Redirect("/");
        }

        public HttpResponse Details(string id)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = this.problemsService.GetAllProblemDetails(id);

            return this.View(viewModel);
        }
    }
}
