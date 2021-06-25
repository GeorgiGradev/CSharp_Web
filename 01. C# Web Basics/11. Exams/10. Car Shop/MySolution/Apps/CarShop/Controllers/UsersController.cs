using CarShop.Services;
using CarShop.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Controllers
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

            return this.Redirect("/Cars/All");
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel register)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(register.Username)
                || register.Username.Length < 4
                || register.Username.Length > 20)
            {
                return this.Error("Name should be between 4 and 20 characters");
            }

            if (string.IsNullOrEmpty(register.Email)
                || !new EmailAddressAttribute().IsValid(register.Email))
            {
                return this.Error("Email is required");
            }

            if (string.IsNullOrEmpty(register.Password)
                || register.Password.Length < 5
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
