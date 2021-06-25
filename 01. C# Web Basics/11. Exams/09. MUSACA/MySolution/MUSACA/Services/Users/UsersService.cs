namespace MUSACA.Services.Users
{
    using System.Linq;
    using System.Text;
    using System.Security.Cryptography;

    using MUSACA.Data;
    using MUSACA.Data.Models;
    using MUSACA.ViewModels.Users;

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

        public AllUserProfileViewModel GetAllUsersOrders(string userId)
        {
            var viewModel = new AllUserProfileViewModel
            {
                Username = this.db.Users
                    .Where(x => x.Id == userId)
                    .Select(x => x.Username)
                    .FirstOrDefault(),
                Profiles = this.db.Orders
                    .Where(x => x.Products.Any(y => y.Order.UserId == userId) && x.Status == OrderStatus.Completed)
                    .Select(x => new UserProfileViewModel
                    {
                        Id = x.Id,
                        IssuedOn = x.IssuedOn.ToString("dd/MM/yyyy"),
                        Cashier = x.User.Username,
                        Total = x.Products.Sum(x => x.Product.Price).ToString("f2")
                    }).ToList()
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
