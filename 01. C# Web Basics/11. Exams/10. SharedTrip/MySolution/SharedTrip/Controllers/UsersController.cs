namespace SharedTrip.Controllers
{
    using System.ComponentModel.DataAnnotations;

    using SharedTrip.Services.Users;
    using SharedTrip.ViewModels.Users;
    using SUS.HTTP;
    using SUS.MvcFramework;

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

            var userId = this.usersService.GetUserId(login.Username, login.Password);
            if (userId == null)
            {
                return this.View();
            }

            this.SignIn(userId);
            return this.Redirect("/Trips/All");
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
                return this.Redirect("/Users/Register");
            }

            if (string.IsNullOrEmpty(input.Email)
                || !new EmailAddressAttribute().IsValid(input.Email))
            {
                return this.Redirect("/Users/Register");
            }

            if (string.IsNullOrEmpty(input.Password)
                || input.Password.Length < 6
                || input.Password.Length > 20)
            {
                return this.Redirect("/Users/Register");
            }

            if (!this.usersService.IsUsernameAvailable(input.Username))
            {
                return this.Redirect("/Users/Register");
            }

            if (!this.usersService.IsEmailAvailable(input.Email))
            {
                return this.Redirect("/Users/Register");
            }

            if (input.ConfirmPassword != input.Password)
            {
                return this.Redirect("/Users/Register");
            }

            this.usersService.Create(input.Username, input.Email, input.Password);
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
