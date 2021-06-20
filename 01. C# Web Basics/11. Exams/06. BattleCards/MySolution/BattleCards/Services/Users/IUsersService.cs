using BattleCards.ViewModels.Users;

namespace BattleCards.Services.Users
{
    public interface IUsersService
    {
        string GetUserId(LoginInputModel model);

        void Create(RegisterInputModel model);

        bool IsUsernameAvailable(RegisterInputModel model);

        bool IsEmailAvailable(RegisterInputModel model);
    }
}
