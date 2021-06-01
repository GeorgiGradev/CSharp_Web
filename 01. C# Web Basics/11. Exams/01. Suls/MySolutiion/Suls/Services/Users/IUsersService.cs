using Suls.ViewModels.Users;

namespace Suls.Services
{
    public interface IUsersService
    {
        string GetUserId(LoginInputModel model);

        void CreateUser(RegisterInputModel model);

        bool IsUsernameAvailable(RegisterInputModel model);

        bool IsEmailAvailable(RegisterInputModel model);
    }
}
