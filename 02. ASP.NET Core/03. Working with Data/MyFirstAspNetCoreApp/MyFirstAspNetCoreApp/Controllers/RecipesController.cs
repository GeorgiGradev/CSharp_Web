using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyFirstAspNetCoreApp.Models.Enums;
using MyFirstAspNetCoreApp.ViewModels.Recipes;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MyFirstAspNetCoreApp.Controllers
{

    public class RecipesController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public RecipesController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }



        public IActionResult Add()
        {
            var model = new AddRecipesInputModel
            {
                Name = "Enter name",
                Quantity = 0,
                Year = 1900,
                CreatedOn = DateTime.UtcNow,
                Type = RecipeType.Unknown,
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRecipesInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            if (!input.Image.FileName.EndsWith(".png") 
                || !input.Image.FileName.EndsWith(".jpg")) 
                // ако не е PNG
            {
                this.ModelState.AddModelError("Image", "Invalid file type");
            }

            if (input.Image.Length > 10 * 1024 * 1024) 
                // ако файла е по-голям от 10 MB
            {
                this.ModelState.AddModelError("Image", "Image size should be less than 10 MB");
            }

            using (FileStream fileStream 
                = new FileStream(this.webHostEnvironment.WebRootPath + "/user.png", FileMode.Create))
            {
                await input.Image.CopyToAsync(fileStream);
            }
               

            return this.RedirectToAction(nameof(ThankYou));


        }

        public IActionResult ThankYou()
        {
            return this.View();
        }

    }
}
