using Git.Services.Repositories;
using Git.ViewModels.Repositories;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }


        public HttpResponse All()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = this.repositoriesService.GetAllRepositories();
            return this.View(viewModel);
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
        public HttpResponse Create(RepositoryInputModel input)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(input.Name)
                || input.Name.Length < 3
                || input.Name.Length > 10)
            {
                return this.Error("Name should be between 3 and 10 characters");
            }

            var userId = GetUserId();

            this.repositoriesService.CreateRepository(userId, input);

            return this.Redirect("/Repositories/All");
        }

    }
}
