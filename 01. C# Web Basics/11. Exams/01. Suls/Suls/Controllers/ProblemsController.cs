using Suls.Services;
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

        public HttpResponse Create() // така (без специален атрибут работи при GET).  Във View-тата има форма Create (Create.cshtml)
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }
            return this.View();
        } 

        [HttpPost]
        public HttpResponse Create(CreateInputModel input)
        {

            if (string.IsNullOrEmpty(input.Name) 
                || input.Name.Length < 5
                || input.Name.Length > 20)
            {
                return this.Error("Problem name should be bewteen 5 and 20 characters");
            }

            if (input.Points < 50 || input.Points > 300)
            {
                return this.Error("Points should be between 50 and 300");
            }

            this.problemsService.Create(input.Name, input.Points);
            return this.Redirect("/");
        }

        public HttpResponse Details (string id)
        {
            {
                if (!this.IsUserSignedIn())
                {
                    this.Redirect("/Users/Login");
                }
                return this.View();
            }
            var viewModel = this.problemsService.GetById(id);
            return this.View(viewModel);
        }
    }
}
