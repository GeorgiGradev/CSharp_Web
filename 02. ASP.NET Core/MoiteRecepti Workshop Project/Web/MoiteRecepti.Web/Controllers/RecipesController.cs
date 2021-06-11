namespace MoiteRecepti.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MoiteRecepti.Services.Data;
    using MoiteRecepti.Web.ViewModels.Recipes;

    public class RecipesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipiesService recipiesService;

        public RecipesController(
            ICategoriesService categoriesService,
            IRecipiesService recipiesService)
        {
            this.categoriesService = categoriesService;
            this.recipiesService = recipiesService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateRecipeInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAllASKeyValuePair();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllASKeyValuePair();
                return this.View(input);
            }

            await this.recipiesService.CreateAsync(input);

            // Redirect to Recipe Info Page
            return this.Redirect("/");
        }
    }
}
