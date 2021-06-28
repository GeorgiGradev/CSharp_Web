namespace MoiteRecepti.Services.Data
{
    using MoiteRecepti.Services.Data.Models;
    using MoiteRecepti.Web.ViewModels.Home;

    public interface IGetCountsService
    {
        CountsDto GetCounts();
    }
}
