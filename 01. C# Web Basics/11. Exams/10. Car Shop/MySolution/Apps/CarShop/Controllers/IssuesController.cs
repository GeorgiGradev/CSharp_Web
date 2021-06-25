using CarShop.Services.Issues;
using CarShop.ViewModels.Issues;
using SUS.HTTP;
using SUS.MvcFramework;


namespace CarShop.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssuesService issuesService;

        public IssuesController(IIssuesService issuesService)
        {
            this.issuesService = issuesService;
        }

        public HttpResponse Add(string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = new IssueAddViewModel
            {
                CarId = carId
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Add(string carId, IssueInputModel inputModel)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(inputModel.Description)
                || inputModel.Description.Length < 4)
            {
                return this.Error("Description length should have more than 5 symbols ");
            }

            this.issuesService.CreateIssue(carId, inputModel);

            return this.Redirect($"/Issues/CarIssues?carId={carId}");
        }

        public HttpResponse CarIssues(string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();

            var viewModel = this.issuesService.GetAllIssues(carId, userId);

            return this.View(viewModel);
        }

        public HttpResponse Delete(string issueId, string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            this.issuesService.DeleteIssue(issueId);
            return this.Redirect($"/Issues/CarIssues?carId={carId}");
        }


        public HttpResponse Fix(string issueId, string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            this.issuesService.FixIssue(issueId);
            return this.Redirect($"/Issues/CarIssues?carId={carId}");
        }

    }
}
