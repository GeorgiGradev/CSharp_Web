using Panda.Data;
using Panda.Data.Models;
using System;
using System.Linq;

namespace Panda.Services
{
    public class ReceiptsService : IReceiptsService
    {
        private readonly ApplicationDbContext db;

        public ReceiptsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(decimal weight, string packageId, string recepientId)
        {
            var receipt = new Receipt
            {
                Fee = weight * 2.67m,
                PackageId = packageId,
                RecipientId = recepientId,
                IssuedOn = DateTime.UtcNow
            };

            this.db.Receipts.Add(receipt);
            this.db.SaveChanges();
        }

        public IQueryable<Receipt> GetAll()
        {
            return this.db.Receipts;
        }
    }
}
