namespace SharedTrip.Services
{
    public interface IUsersService
    {
        string GetUserId(string username, string passoword);
        // ще ни трябва за login

        void Create(string username, string email, string password);
        //ще ни трябва за създаването на потребител

        bool IsUsernameAvailable(string username);
        // ше ни трябва за валидациите при регистрация

        bool IsEmailAvailable(string email);
        // ше ни трябва за валидациите при регистрация

    }
}
 