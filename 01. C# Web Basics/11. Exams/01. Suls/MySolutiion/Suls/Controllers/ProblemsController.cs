using Suls.Services;
using Suls.ViewModels.Problems;
using Suls.ViewModels.Users;
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
            if (!this.IsUserSignedIn())
            {
               return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.problemsService.GetById(id);
          
            return this.View(viewModel);
        }

        [HttpPost] 
        public HttpResponse Create(CreateProblemInputModel createModel)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            string userId = GetUserId();

            if (string.IsNullOrEmpty(createModel.Name)
                || createModel.Name.Length < 5
                || createModel.Name.Length > 20)
            {
                 return this.View();
            }

            if (createModel.Points < 50
                || createModel.Points > 300)
            {
                return this.View();
            }

            this.problemsService.CreateProblem(createModel, userId);

            return this.Redirect("/");
        }
    }
}
