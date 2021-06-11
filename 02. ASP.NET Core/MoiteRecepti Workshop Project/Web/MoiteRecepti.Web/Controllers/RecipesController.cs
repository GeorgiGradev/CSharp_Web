namespace MoiteRecepti.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MoiteRecepti.Web.ViewModels.Recipes;

    public class RecipesController : Controller
    {
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateRecipeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            // Create Recipe using Service method
            // Redirect to Recipe Info Page
            return this.Redirect("/");
        }
    }
}
