using Suls.Services;
using Suls.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System.ComponentModel.DataAnnotations;

namespace Suls.Controllers
{
    public class UsersController : Controller
    {
        private IUsersService usersService;

        public UsersController(IUsersService usersService) // нашият контролер има нужда от IUserService, за да работи коректно
        {
            this.usersService = usersService;
        }

        public HttpResponse Login() // работи при GET и връща VIEW-то
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/");
            }
            return this.View();
        }

        //	Когато някой изпрати LOGIN формата с Http POST заявка на същият LOGIN адрес и изпрати userName и password (тези параметри ги знаем от VIEW-то)
        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            var userId = this.usersService.GetUserId(username, password);

            if (userId == null) // ако няма такъв USER
            {
                return this.Error("Invalid username or password.");
            }
            //ако успеем да се log-нем викаме базовият клас с метода за SignIn
            this.SignIn(userId);

            // след това правим това, което се иска по условие (При успешен Login на User-a го Redirect-ваме към HomePage
            return this.Redirect("/");
        }



        public HttpResponse Register() // работи при GET и връша VIEW-то
        {
            return this.View();
        }

        // При регистрация правим един HttpReponse метод, който ще приеме RegisterInputModel, който ще прима само POST заяки пратени на този адрес
        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            // тук се правят входните валидации
            if (string.IsNullOrEmpty(input.Username) || input.Username.Length < 5 || input.Username.Length > 20)
            {
                return this.Error("Username should be between 5 and 20 characters.");
            }

            if (!this.usersService.isUserNameAvailable(input.Username))
            {
                return this.Error("Username already taken.");
            }

            if (string.IsNullOrEmpty(input.Email) 
                || !new EmailAddressAttribute().IsValid(input.Email))
            {
                return this.Error("Invalid Email address.");
            }

            if (!this.usersService.isEmailAvailable(input.Email))
            {
                return this.Error("Email already taken.");
            }

            if (string.IsNullOrEmpty(input.Password) 
                || input.Password.Length < 6 
                || input.Password.Length > 20)
            {
                return this.Error("Password should be between 6 and 20 characters.");
            }

            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Passwords do not match.");
            }

            //след всички проверки създаваме потребителя
            this.usersService.CreateUser(input.Username, input.Email, input.Password);

            //след това изпълняваме изискването да го REDIRECT-нем
            // •	Upon successful Registration of a User, you should be redirected to the Login Page.

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {

            if (!this.IsUserSignedIn())
            {
                this.Redirect("/");
            }

            this.SignOut();
            return this.Redirect("/");
        }
    }
}
 