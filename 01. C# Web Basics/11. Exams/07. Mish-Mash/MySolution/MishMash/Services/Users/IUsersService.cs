using MishMash.ViewModels.Users;

namespace MishMash.Services
{
    public interface IUsersService
    {
        void Create(RegisterInputModel register);

        bool IsUsernameAvailable(RegisterInputModel register);

        bool IsEmailAvailable(RegisterInputModel registerl);

        string GetUserId(LoginInputModel login);

        bool isUserAdmin(string userId);
    }
}
