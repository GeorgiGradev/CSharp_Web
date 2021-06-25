using MUSACA.Services.Home;
using SUS.HTTP;
using SUS.MvcFramework;

namespace MUSACA.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        [HttpGet("/")]
        public HttpResponse IndexSlash()
        {
            return this.Index();
        }

        public HttpResponse Index()
        {
            if (!this.IsUserSignedIn())
            {
                return this.View();
            }
            var userId = this.GetUserId();
            var viewModel = this.homeService.GetLoginViewModel(userId);

            return this.View(viewModel, "IndexLoggedInUser");
        }
    }
}
