using BattleCards.Services.Users;
using BattleCards.ViewModels.Users;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.ComponentModel.DataAnnotations;

namespace BattleCards.Controllers
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
            if (IsUserLoggedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        public HttpResponse Register()
        {
            if (IsUserLoggedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel model)
        {
            if (IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.usersService.GetUserId(model);
            if (userId == null)
            {
                Console.WriteLine("User is null");
                return this.View();
            }

            this.SignIn(userId);
            return this.Redirect("/Cards/All");
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel model)
        {
            if (string.IsNullOrEmpty(model.Username)
                || model.Username.Length < 5
                || model.Username.Length > 20)
            {
                return this.Error("Name should be between 5 and 20 characters");
            }

            if (string.IsNullOrEmpty(model.Email)
                || !new EmailAddressAttribute().IsValid(model.Email))
            {
                return this.Error("Email is required");
            }

            if (string.IsNullOrEmpty(model.Password)
                || model.Password.Length < 6
                || model.Password.Length > 20)
            {
                return this.Error("Password should be between 6 and 20 characters");
            }

            if (!this.usersService.IsUsernameAvailable(model))
            {
                return this.Error("Username not available");
            }

            if (!this.usersService.IsEmailAvailable(model))
            {
                return this.Error("Email not available");
            }

            if (model.ConfirmPassword != model.Password)
            {
                return this.Error("Passwords do not match");
            }

            this.usersService.Create(model);
            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            this.SignOut();
            return this.Redirect("/");
        }
    }
}
