namespace SharedTrip.Controllers
{
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                return Redirect("/Trips/All");
            }

            return this.View(); // Върни index.cshtml от папка Views/Home
        }
    }
}
