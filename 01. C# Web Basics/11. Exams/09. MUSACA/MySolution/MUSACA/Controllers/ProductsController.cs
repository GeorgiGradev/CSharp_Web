using MUSACA.Services.Products;
using MUSACA.ViewModels.Orders;
using MUSACA.ViewModels.Products;
using SUS.HTTP;
using SUS.MvcFramework;

namespace MUSACA.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateProductInputModel inputModel)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(inputModel.Name)
                || inputModel.Name.Length < 3
                || inputModel.Name.Length > 10)
            {
                return this.Redirect("/Product/Create");
            }

            if (inputModel.Price < 0.01m)
            {
                return this.Redirect("/Product/Create");
            }

            if (!this.productsService.DoesProductExist(inputModel))
            {
                return this.Redirect("/Product/Create");
            }

            this.productsService.CreateProduct(inputModel);
            return this.Redirect("/");
        }

        [HttpPost]
        public HttpResponse Order(OrderProductInputModel inputModel)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();
            this.productsService.OrderProduct(userId, inputModel);

            return this.Redirect("/");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = this.productsService.GetAllProducts();
            return this.View(viewModel);
        }
    }
}
