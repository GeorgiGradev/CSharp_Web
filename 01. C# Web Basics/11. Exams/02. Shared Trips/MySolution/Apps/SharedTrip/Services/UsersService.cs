using SharedTrip.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SharedTrip.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        // описваме в конструктора неговите DEPENDENCY-та (всяко нещо което ни трябва ще го получаваме в конструктора)
        {
            this.db = db;
        }
        //01. Започваме да пишем методите
        //02. Добавяме най-долу метода за хеширане на паролата


        //03. Започваме със създавнето на USER и вкарването му в базата данни
        public void Create(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = ComputeHash(password) // хешираме паролата с метода по-долу
            };

            this.db.Users.Add(user); // добавяме го в базата
            this.db.SaveChanges(); // запазваме промените

        }

        public string GetUserId(string username, string password)
        {
            var hashPassword = ComputeHash(password);
            var user = db.Users.FirstOrDefault(x => x.Username == username && x.Password == hashPassword);

            if (user == null)
            {
                return null;
            }
            else
            {
                return user.Id;
            }

            // може да се опише и така
            // =>  return user == null ? null : user.Id;

            // или така
            // => return user?.Id;

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

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);

                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));

                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
