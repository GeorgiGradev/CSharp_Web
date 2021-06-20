using Suls.Services.Problems;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Suls.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProblemsService problemsService;

        public HomeController(IProblemsService problemsService)
        {
            this.problemsService = problemsService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (IsUserSignedIn())
            {
               return this.Redirect("/Home/IndexLoggedIn");
            }
            return this.View();
        }

        public HttpResponse IndexLoggedIn()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            var viewModel = this.problemsService.GetAllProblems();
            return this.View(viewModel);
        }
    }
}
 