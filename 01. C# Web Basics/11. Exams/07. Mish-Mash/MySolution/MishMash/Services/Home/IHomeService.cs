using MishMash.ViewModels.Home;

namespace MishMash.Services.Home
{
    public interface IHomeService
    {
       HomeViewModel GetLoginDetails(string userId);
    }
}
