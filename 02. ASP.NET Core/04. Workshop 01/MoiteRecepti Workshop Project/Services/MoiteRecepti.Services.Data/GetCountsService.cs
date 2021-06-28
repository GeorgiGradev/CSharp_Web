namespace MoiteRecepti.Services.Data
{
    using System.Linq;

    using MoiteRecepti.Data.Common.Repositories;
    using MoiteRecepti.Data.Models;
    using MoiteRecepti.Services.Data.Models;

    public class GetCountsService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IRepository<Image> imageRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;

        public GetCountsService(
            IDeletableEntityRepository<Category> categoryRepository,
            IRepository<Image> imageRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository,
            IDeletableEntityRepository<Recipe> recipesRepository)
        {
            this.categoryRepository = categoryRepository;
            this.imageRepository = imageRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.recipesRepository = recipesRepository;
        }

        public CountsDto GetCounts()
        {
            var countsDto = new CountsDto
            {
                CategoriesCount = this.categoryRepository.All().Count(),
                ImagesCount = this.imageRepository.All().Count(),
                IngredientsCount = this.ingredientsRepository.All().Count(),
                RecipesCount = this.recipesRepository.All().Count(),
            };

            return countsDto;
        }
    }
}
