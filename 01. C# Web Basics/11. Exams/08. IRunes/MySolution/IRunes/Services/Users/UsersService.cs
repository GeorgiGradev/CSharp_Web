using IRunes.Data;
using IRunes.Data.Models;
using IRunes.ViewModels.Home;
using IRunes.ViewModels.Users;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IRunes.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(RegisterInputModel register)
        {
            var user = new User
            {
                Username = register.Username,
                Email = register.Email,
                Password = ComputeHash(register.Password)
            };
            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public HomeViewModel GetHomeViewModel(string userId)
        {
            var username = this.db.Users
                .Where(x => x.Id == userId)
                .Select(x => x.Username)
                .FirstOrDefault();

            var viewModel = new HomeViewModel
            {
                Username = username
            };

            return viewModel;
        }

        public string GetUserId(LoginInputModel login)
        {
            var hashPassword = ComputeHash(login.Password);
            var user = db.Users.FirstOrDefault(x => x.Username == login.Username && x.Password == hashPassword);
            return user?.Id;
        }

        public bool IsEmailAvailable(RegisterInputModel register)
        {
            return !db.Users.Any(x => x.Email == register.Email);
        }

        public bool IsUsernameAvailable(RegisterInputModel register)
        {
            return !db.Users.Any(x => x.Username == register.Username);
        }

        private string ComputeHash(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);

            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
