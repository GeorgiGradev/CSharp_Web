using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
   public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/Repositories/All");
            }
            return this.View();
        }
    }
}
