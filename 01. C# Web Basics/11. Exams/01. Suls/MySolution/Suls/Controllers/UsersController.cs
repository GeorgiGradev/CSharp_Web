using Suls.Services.Users;
using Suls.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System.ComponentModel.DataAnnotations;

namespace Suls.Controllers
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

            var userId = this.usersService.GetUserId(login);

            if (userId == null)
            {
                return this.Redirect("/Users/Login");
            }
            this.SignIn(userId);

            return this.Redirect("/"); 
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel register)
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(register.Username)
                || register.Username.Length < 5
                || register.Username.Length > 20)
            {
                return this.Redirect("/Users/Register");
            }

            if (string.IsNullOrEmpty(register.Email)
                || !new EmailAddressAttribute().IsValid(register.Email))
            {
                System.Console.WriteLine(1);
                return this.Redirect("/Users/Register");
                
            }

            if (string.IsNullOrEmpty(register.Password)
                || register.Password.Length < 6
                || register.Password.Length > 20)
            {
                System.Console.WriteLine(2);
                return this.Redirect("/Users/Register");
            }

            if (!this.usersService.IsUsernameAvailable(register))
            {
                System.Console.WriteLine(3);
                return this.Redirect("/Users/Register");
            }

            if (!this.usersService.IsEmailAvailable(register))
            {
                System.Console.WriteLine(4);
                return this.Redirect("/Users/Register");
            }

            if (register.ConfirmPassword != register.Password)
            {
                System.Console.WriteLine(5);
                return this.Redirect("/Users/Register");
            }

            this.usersService.Create(register);

            return this.Redirect("/Users/Login"); 
        }

        public HttpResponse Logout()
        {
            if (this.IsUserSignedIn())
            {
                this.SignOut();
            }

            return this.Redirect("/"); 
        }
    }
}
