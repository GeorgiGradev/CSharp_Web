using Git.Services;
using Git.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;

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
            return this.View();
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(UserLoginInputModel model)
        {
            var userId = this.usersService.GetUserId(model.Username, model.Password);

            if (userId == null)
            {
               return this.Error("No user");
            }

            this.SignIn(userId);
            return this.Redirect("/Repositories/All");
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterInputModel model)
        {
            if (string.IsNullOrEmpty(model.Username)
                || model.Username.Length < 5 
                || model.Username.Length > 20)
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(model.Email))
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(model.Password)
                || model.Password.Length < 6
                || model.Password.Length > 20)
            {
                return this.View();
            }

            this.usersService.Create(model.Username, model.Email, model.Password);

            return this.Redirect("/Users/Login");
        }


        public HttpResponse Logout()
        {
            this.SignOut();
            return this.Redirect("/");
        }

    }
}
