using Suls.Services;
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
        public HttpResponse Login(LoginInputModel model)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }  

            var userId = this.usersService.GetUserId(model);

            if (userId == null)
            {
                return this.View();
            }

            this.SignIn(userId);
           return this.Redirect("/");
        }
           
        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(input.Username)
                || input.Username.Length < 5
                || input.Username.Length > 20)
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(input.Email)
                || !new EmailAddressAttribute().IsValid(input.Email))
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(input.Password)
                || input.Password.Length < 6
                || input.Password.Length > 20)
            {
                return this.View();
            }

            if (!this.usersService.IsUsernameAvailable(input))
            {
                return this.View();
            }

            if (!this.usersService.IsEmailAvailable(input))
            {
                return this.View();
            }

            if (input.ConfirmPassword != input.Password)
            {
                return this.View();
            }


            this.usersService.CreateUser(input);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.SignOut();
            return this.Redirect("/");
        }

    }
}
