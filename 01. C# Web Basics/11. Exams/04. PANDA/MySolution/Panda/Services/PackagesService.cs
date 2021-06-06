using Panda.Data;
using Panda.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Panda.Services
{
    public class PackagesService : IPackagesService
    {
        private readonly ApplicationDbContext db;
        private readonly IReceiptsService receiptsService;

        public PackagesService(ApplicationDbContext db, IReceiptsService receiptsService)
        {
            this.db = db;
            this.receiptsService = receiptsService;
        }

        public void CreatePackage(string description, decimal weight, string shippingAddress, string recipientName)
        {
            var recipientId = this.db.Users.Where(x => x.Username == recipientName).Select(x => x.Id).FirstOrDefault();

            if (recipientId == null)
            {
                return;
            }

            var package = new Package
            {
                Description = description,
                Weight = weight,
                Status = PackageStatus.Pending,
                ShippingAddress = shippingAddress,
                RecipientId = recipientId,
            };

            this.db.Packages.Add(package);
            this.db.SaveChanges();
        }

        public void Deliver(string id)
        {
            var package = this.db.Packages.FirstOrDefault(x => x.Id == id);
            if (package == null)
            {
                return;
            }

            package.Status = PackageStatus.Delivered;
            this.db.SaveChanges();

            this.receiptsService.Create(package.Weight, package.Id, package.RecipientId);
        }

        public IQueryable<Package> GetAllByStatus(PackageStatus status)
        {
            var packages = this.db.Packages.Where(x => x.Status == status);
            return packages;
        }
    }
}
