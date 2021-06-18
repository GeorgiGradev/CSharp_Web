using Git.Services.Users;
using Git.ViewModels;
using Git.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System.ComponentModel.DataAnnotations;

namespace Git.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Login()
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel login)
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            // когато дойде POST заявка тя се извиква към адрес LOGIN (LoginInputModel model)
            var userId = this.usersService.GetUserId(login);

            if (userId == null) // ако няма такъв USER
            {
                return this.Error("Invalid username or password.");
            }
            //ако успеем да се log-нем викаме базовият клас с метода за SignIn
            this.SignIn(userId);
            // след това правим това, което се иска по условие 

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse Register()
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel register)
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            // когато дойде POST заявка тя се извиква към адрес REGISTER (RegisterInputModel register)
            if (string.IsNullOrEmpty(register.Username)
                || register.Username.Length < 5
                || register.Username.Length > 20)
            {
                return this.Error("Name should be between 5 and 20 characters");
            }

            if (string.IsNullOrEmpty(register.Email)
                || !new EmailAddressAttribute().IsValid(register.Email))
            {
                return this.Error("Email is required");
            }

            if (string.IsNullOrEmpty(register.Password)
                || register.Password.Length < 6
                || register.Password.Length > 20)
            {
                return this.Error("Password should be between 6 and 20 characters");
            }

            if (!this.usersService.IsUsernameAvailable(register))
            {
                return this.Error("Username not available");
            }

            if (!this.usersService.IsEmailAvailable(register))
            {
                return this.Error("Email not available");
            }

            if (register.ConfirmPassword != register.Password)
            {
                return this.Error("Passwords do not match");
            }

            //след всички проверки създаваме потребителя
            this.usersService.Create(register);

            //след това изпълняваме изискването да го REDIRECT-нем
            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (IsUserSignedIn())
            {
                this.SignOut();
            }

            return this.Redirect("/");
        }
    }
}
