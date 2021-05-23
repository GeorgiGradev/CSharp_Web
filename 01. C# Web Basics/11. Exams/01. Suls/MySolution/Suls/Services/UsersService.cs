using Suls.Data;
using SulsApp.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Suls.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db) //всяко нещо което ни трябва ще го получаваме в конструктора
        {
            this.db = db;
        }

        public void CreateUser(string userName, string email, string password) 
        {
            var user = new User // когато ни е подаден потребител създаваме new User
            {
                Email = email,
                Username = userName,
                Password = ComputeHash(password) // хешираме паролата с метода по-долу
            };
            db.Users.Add(user); // добавяме го в базата
            db.SaveChanges();
        }

        public string GetUserId(string username, string password) //  бърка в базата данни, търси дали има такъв USER и му връща Id-то.
        {
            string hashedPassword = ComputeHash(password);
            var user = db.Users.FirstOrDefault(x => x.Username == username && hashedPassword == x.Password);
            if (user != null)
            {
                return user.Id;
            }
            else
            {
                return null;
            }

            // може да се опише и така
            // =>  return user == null ? null : user.Id;

            // или така
            // => return user?.Id;

        }

        public bool isEmailAvailable(string email)
        {
            return !db.Users.Any(x => x.Email == email);
        }

        public bool isUserNameAvailable(string username)
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
