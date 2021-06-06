using Panda.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Panda.Services
{
    public interface IPackagesService
    {
        public void CreatePackage(string description, decimal weight, string shippingAddress, string recipientName);

        public IQueryable<Package> GetAllByStatus(PackageStatus status);

        void Deliver(string id);
    }
}
