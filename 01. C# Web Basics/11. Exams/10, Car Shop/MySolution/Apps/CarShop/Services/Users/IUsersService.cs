using CarShop.ViewModels.Users;

namespace CarShop.Services
{
    public interface IUsersService
    {
        void Create(RegisterInputModel register);

        bool IsUsernameAvailable(RegisterInputModel register);

        bool IsEmailAvailable(RegisterInputModel registerl);

        string GetUserId(LoginInputModel login);

        bool IsUserMechanic(string userId);
    }
}
