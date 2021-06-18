using Git.ViewModels;
using Git.ViewModels.Users;

namespace Git.Services.Users
{
    public interface IUsersService
    {
        //ще ни трябва за създаването на потребител
        void Create(RegisterInputModel register);

        // ше ни трябва за валидациите при регистрация
        bool IsUsernameAvailable(RegisterInputModel register);

        // ше ни трябва за валидациите при регистрация
        bool IsEmailAvailable(RegisterInputModel registerl);

        // ще ни трябва за login
        string GetUserId(LoginInputModel login);
    }
}

