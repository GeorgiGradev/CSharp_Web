namespace Suls.Services
{
    public interface IUsersService
    {
        void CreateUser(string userName, string email, string password);
        //ще ни трябва за създаването на потребител

        string GetUserId(string username, string password); 
        // ще ни трябва за login

        bool isUserNameAvailable(string username); 
        // ше ни трябва за валидациите при регистрация

        bool isEmailAvailable(string email); 
        // ше ни трябва за валидациите при регистрация
    }
}
