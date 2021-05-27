using Andreys.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Andreys.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }


        public HttpResponse Index()
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpGet("/")]
        public HttpResponse Home()
        {
            if (IsUserSignedIn())
            {
                var viewModel = this.productsService.GetAll();
                return this.View(viewModel);
            }
            return this.Redirect("/Home/Index");
        }
    }
}