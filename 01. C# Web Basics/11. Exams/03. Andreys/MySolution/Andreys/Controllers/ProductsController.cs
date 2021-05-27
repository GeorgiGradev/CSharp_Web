using Andreys.Data.Enums;
using Andreys.Services;
using Andreys.ViewModels.Products;
using SUS.HTTP;
using SUS.MvcFramework;
using System;

namespace Andreys.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public HttpResponse Add()
        {
            if (IsUserSignedIn())
            {
                return this.View();
            }

            return this.Redirect("/Users/Login");
           
        }

        [HttpPost]
        public HttpResponse Add(AddProductInputModel input)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(input.Name)
                || input.Name.Length < 4
                || input.Name.Length > 20)
            {
                return this.Redirect("/Products/Add");
            }

            if (string.IsNullOrEmpty(input.Description)
                || input.Description.Length > 10)
            {
                return this.Redirect("/Products/Add");
            }

            if (!Enum.IsDefined(typeof(Category), input.Category))
            {
                return this.Redirect("/Products/Add");
            }

            if (!Enum.IsDefined(typeof(Gender), input.Gender))
            {
                return this.Redirect("/Products/Add");
            }

            this.productsService.Add(input);
            return this.Redirect("/");
        }

        public HttpResponse Details(int id)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var details = this.productsService.GetDetails(id);
            return this.View(details);
        }


        public HttpResponse Delete(int id)
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.productsService.DeleteProduct(id);
            return this.Redirect("/");
        }
    }
}