using SharedTrip.Data;
using SharedTrip.Services;
using SharedTrip.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService) // контролерът има нужда от IUserService, за да работи коректно
        {
            this.usersService = usersService;
        }


        public HttpResponse Login() // така (без атрибут) работи при GET и връша VIEW-то. Във View-тата има форма LOGIN (Login.cshtml)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel input)
        {
            var userId = this.usersService.GetUserId(input.Username, input.Password);

            if (userId == null) // ако няма такъв USER
            {
                return this.Error("Invalid username or password.");
            }
            //ако успеем да се log-нем викаме базовият клас с метода за SignIn
            this.SignIn(userId);
            // след това правим това, което се иска по условие 
            return this.Redirect("/Trips/All");

        } 


        public HttpResponse Register() //така (без атрибут) работи при GET и връша VIEW-то. Във View-тата има форма REGISTER (Register.cshtml)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (string.IsNullOrEmpty(input.Username)
                || input.Username.Length < 5
                || input.Username.Length > 20)
            {
                return this.Error("Username should be between 5 and 20 characters");
            }
            if (string.IsNullOrEmpty(input.Email)
                || !new EmailAddressAttribute().IsValid(input.Email))
            {
                return this.Error("Email is required");
            }
            if (string.IsNullOrEmpty(input.Password)
                || input.Password.Length < 6 
                || input.Password.Length > 20)
            {
                return this.Error("Password should be between 6 and 20 characters");
            }
            if (!this.usersService.IsUsernameAvailable(input.Username))
            {
                return this.Error("Username not available");
            }
            if (!this.usersService.IsEmailAvailable(input.Email))
            {
                return this.Error("Email not available");
            }
            if (input.ConfirmPassword != input.Password)
            {
                return this.Error("Passwords do not match");
            }


            //след всички проверки създаваме потребителя
            this.usersService.Create(input.Username, input.Email, input.Password);

            //след това изпълняваме изискването да го REDIRECT-нем
            return this.Redirect("/");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("Users/Login");
            }

            this.SignOut();
            return this.Redirect("/");
        }
    }
}
