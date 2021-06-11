namespace MoiteRecepti.Services.Data
{
    using System.Threading.Tasks;

    using MoiteRecepti.Web.ViewModels.Recipes;

    public interface IRecipiesService
    {
        Task CreateAsync(CreateRecipeInputModel input);
    }
}
