using Suls.Data;
using Suls.ViewModels.Users;
using SulsApp.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Suls.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateUser(RegisterInputModel model)
        {
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = ComputeHash(model.Password) 
            };

            this.db.Users.Add(user);
            this.db.SaveChanges(); 
        }

        public string GetUserId(LoginInputModel model)
        {
            var hashPassword = ComputeHash(model.Password);
            var user = db.Users.FirstOrDefault(x => x.Username == model.Username && x.Password == hashPassword);

            return user?.Id;
        }

        public bool IsEmailAvailable(RegisterInputModel model)
        {
            return !db.Users.Any(x => x.Email == model.Email);
        }

        public bool IsUsernameAvailable(RegisterInputModel model)
        {
            return !db.Users.Any(x => x.Username == model.Username);
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);

            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }



    }
}
