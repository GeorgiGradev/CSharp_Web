using Git.Data;
using Git.Services;
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
            var viewModel = this.repositoriesService.GetAll();

            return this.View(viewModel);
        }

        public HttpResponse Create()
        {
            return this.View();

        }

        [HttpPost]
        public HttpResponse Create(CreateRepositoryInputModel model)
        {
            if (string.IsNullOrEmpty(model.Name)
               || model.Name.Length < 3
               || model.Name.Length > 10)
            {
                return this.View();
            }

            var userId = GetUserId();

            this.repositoriesService.CreateRepository(model.Name, model.RepositoryType, userId);

            return this.Redirect("/Repositories/All");
        }

    }
}
