using Microsoft.AspNetCore.Http;
using MyFirstAspNetCoreApp.Models.Enums;
using MyFirstAspNetCoreApp.Web.Web.Infrastructure.ValidatinoAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyFirstAspNetCoreApp.ViewModels.Recipes
{
    public class AddRecipesInputModel
    {

        public IFormFile Image { get; set; }

        public string Description { get; set; }

        [Required]
        [MinLength(10)]
        [Display(Name="Recipe Name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }

        [Range(1, 10)]
        public int Quantity { get; set; }

        [Range(1900, int.MaxValue)]
       // [CurrentYearMaxValue]
        public int Year { get; set; }

        public RecipeType Type { get; set; }
    }
}
