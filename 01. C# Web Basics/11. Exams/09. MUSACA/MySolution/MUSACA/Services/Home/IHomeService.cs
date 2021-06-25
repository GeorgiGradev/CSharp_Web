using MUSACA.ViewModels.Home;

namespace MUSACA.Services.Home
{
    public interface IHomeService
    {
        LoginViewModel GetLoginViewModel(string userId);
    }
}
