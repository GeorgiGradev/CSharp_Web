using Git.Services.Commits;
using Git.Services.Repositories;
using Git.ViewModels.Commits;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly IRepositoriesService repositoriesService;
        private readonly ICommitsService commitsService;

        public CommitsController(
            IRepositoriesService repositoriesService,
            ICommitsService commitsService)
        {
            this.repositoriesService = repositoriesService;
            this.commitsService = commitsService;
        }


        public HttpResponse All()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = GetUserId();
            var viewModel = this.commitsService.GetAllCommits(userId);
            return this.View(viewModel);
        }

        public HttpResponse Create(string id)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var repositoryName = this.repositoriesService.GetRepositoryName(id);

            var viewModel = new CreateCommitViewModel
            {
                Id = id,
                Name = repositoryName,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string id, CreateCommitInputModel model)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = GetUserId();

            this.commitsService.CreateCommit(userId, id, model);

            return this.Redirect("/Commits/All");
        }

        public HttpResponse Delete(string id)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            this.commitsService.DeleteCommit(id);

            return this.Redirect("/Commits/All");
        }

    }
}
