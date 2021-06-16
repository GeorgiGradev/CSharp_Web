using Git.Services;
using Git.Services.Commits;
using Git.ViewModels.Commits;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ICommitsService commitsService;
        private readonly IRepositoriesService repositoriesService;

        public CommitsController(ICommitsService commitsService, IRepositoriesService repositoriesService)
        {
            this.commitsService = commitsService;
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse All()
        {
            var userId = this.GetUserId();
            var viewModel = this.commitsService.GetAll(userId);

            return this.View(viewModel);
        }

        public HttpResponse Create(string id)
        {
            var repositoryName = this.repositoriesService.GetRepositoryName(id);
            var viewModel = new CreateCommitInputModel
            { 
                Id = id,
               Name = repositoryName
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string description, string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(description) ||
                description.Length < 5)
            {
                return this.Error("Description must have at least 5 characters");
            }

            var userId = this.GetUserId();

            this.commitsService.Create(description, id, userId);

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            if (!this.commitsService.Delete(id, userId))
            {
                return this.Error("Error occurred.");
            }

            return this.Redirect("/Commits/All");
        }

    }
}
