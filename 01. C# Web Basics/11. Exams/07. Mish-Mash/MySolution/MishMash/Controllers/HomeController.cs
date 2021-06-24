using MishMash.Services.Home;
using SUS.HTTP;
using SUS.MvcFramework;

namespace MishMash.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }


        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (!this.IsUserSignedIn())
            {
                return this.View();
            }

            var userId = this.GetUserId();
            var viewModel = this.homeService.GetLoginDetails(userId);

            return this.View(viewModel, "/LoggedInIndex");
        }
    }
}
