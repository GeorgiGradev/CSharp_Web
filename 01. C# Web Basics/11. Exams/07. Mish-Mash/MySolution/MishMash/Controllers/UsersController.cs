using MishMash.Services;
using MishMash.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System.ComponentModel.DataAnnotations;

namespace MishMash.Controllers
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
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel login)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.usersService.GetUserId(login);

            if (userId == null)
            {
                return this.Error("Invalid username or password.");
            }
            this.SignIn(userId);

            return this.Redirect("/"); 
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel register)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(register.Username))
            {
                return this.Error("Invalid Username");
            }

            if (string.IsNullOrEmpty(register.Email)
                || !new EmailAddressAttribute().IsValid(register.Email))
            {
                return this.Error("Email is required");
            }

            if (string.IsNullOrEmpty(register.Password))
            {
                return this.Error("Invalid password");
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
