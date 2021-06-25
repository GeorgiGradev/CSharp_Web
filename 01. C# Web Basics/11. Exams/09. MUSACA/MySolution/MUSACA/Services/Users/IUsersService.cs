using MUSACA.ViewModels.Users;

namespace MUSACA.Services.Users
{
    public interface IUsersService
    {
        void Create(RegisterInputModel register);

        bool IsUsernameAvailable(RegisterInputModel register);

        bool IsEmailAvailable(RegisterInputModel registerl);

        string GetUserId(LoginInputModel login);

        AllUserProfileViewModel GetAllUsersOrders(string userId);
    }
}
