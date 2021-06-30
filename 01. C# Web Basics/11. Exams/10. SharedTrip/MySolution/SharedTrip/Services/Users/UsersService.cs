namespace SharedTrip.Services.Users
{
    using System.Linq;
    using System.Text;
    using System.Security.Cryptography;

    using SharedTrip.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)

        {
            this.db = db;
        }

        public void Create(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = ComputeHash(password) 
            };

            this.db.Users.Add(user); 
            this.db.SaveChanges(); 
        }

        public string GetUserId(string username, string password)
        {
            var hashPassword = ComputeHash(password);
            var user = db.Users.FirstOrDefault(x => x.Username == username && x.Password == hashPassword);

            return user?.Id;
        }
        public bool IsEmailAvailable(string email)
        {
            return !db.Users.Any(x => x.Email == email);
        }

        public bool IsUsernameAvailable(string username)
        {
            return !db.Users.Any(x => x.Username == username);
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