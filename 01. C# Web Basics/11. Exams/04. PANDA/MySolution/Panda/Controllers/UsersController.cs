using Panda.Services;
using Panda.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;

namespace Panda.Controllers
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

            var userId = this.usersService.GetUserId(model.Username, model.Password);

            if (userId == null)
            {
                return this.Redirect("/Users/Login");
            }
            this.SignIn(userId);
            return this.Redirect("/");
        }
         

        [HttpPost]
        public HttpResponse Register(RegisterInputModel model)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }


            if (string.IsNullOrEmpty(model.Username)
                || model.Username.Length < 5
                || model.Username.Length > 20)
            {
                Console.WriteLine("Wrong username");
                return this.Redirect("/Users/Register");
            }

            if (string.IsNullOrEmpty(model.Email)
                || model.Email.Length < 5
                || model.Email.Length > 20)
            {
                Console.WriteLine("Wrong email");
                return this.Redirect("/Users/Register");
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                Console.WriteLine("Wrong password");
                return this.Redirect("/Users/Register");
            }

            if (model.ConfirmPassword != model.Password)
            {
                Console.WriteLine("Wrong confirmPassword");
                return this.Redirect("/Users/Register");
            }
             

            this.usersService.Create(model.Username, model.Email, model.Password);

            var userId = this.usersService.GetUserId(model.Username, model.Password);
            this.SignIn(userId);

            return this.Redirect("/");
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
