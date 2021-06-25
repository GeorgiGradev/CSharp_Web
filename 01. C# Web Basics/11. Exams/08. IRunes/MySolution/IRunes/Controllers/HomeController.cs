using IRunes.Services.Users;
using SUS.HTTP;
using SUS.MvcFramework;

namespace IRunes.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                var userId = this.GetUserId();
                var viewModel = this.usersService.GetHomeViewModel(userId);
                return this.View(viewModel, "/Home");
            }
            return this.View();
        }
    }
}
