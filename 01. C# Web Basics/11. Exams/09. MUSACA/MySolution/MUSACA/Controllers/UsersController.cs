namespace MUSACA.Controllers
{
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System.ComponentModel.DataAnnotations;

    using MUSACA.Services.Users;
    using MUSACA.ViewModels.Users;

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
                return this.Redirect("/Users/Login");
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

            if (string.IsNullOrEmpty(register.Username)
                || register.Username.Length < 5
                || register.Username.Length > 20)
            {
                return this.Redirect("/Users/Register");
            }

            if (string.IsNullOrEmpty(register.Email)
                || !new EmailAddressAttribute().IsValid(register.Email)
                || register.Email.Length < 5
                || register.Email.Length > 20)
            {
                return this.Redirect("/Users/Register");
            }

            if (string.IsNullOrEmpty(register.Password))
            {
                return this.Redirect("/Users/Register");
            }

            if (!this.usersService.IsUsernameAvailable(register))
            {
                return this.Redirect("/Users/Register");
            }

            if (!this.usersService.IsEmailAvailable(register))
            {
                return this.Redirect("/Users/Register");
            }

            if (register.ConfirmPassword != register.Password)
            {
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

        public HttpResponse Profile()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();
            var viewModel = this.usersService.GetAllUsersOrders(userId);

            return this.View(viewModel);
        }
    }
}
