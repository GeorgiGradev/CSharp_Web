using Panda.Data;
using Panda.ViewModels.Receipts;
using System.Linq;

namespace Panda.Services.Receipts
{
    public class ReceiptsService : IReceiptsService
    {
        private readonly ApplicationDbContext db;

        public ReceiptsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public AllReceiptsViewModel GetAllReceipts(string userId)
        {
            var viewModel = new AllReceiptsViewModel
            {
                AllReceipts = this.db.Receipts
                    .Where(x => x.RecipientId == userId)
                    .Select(x => new ReceiptViewModel()
                    {
                        RecipientName = x.Recipient.Username,
                        Fee = x.Fee,
                        IssuedOn = x.IssuedOn.ToString("yyyy-MM-dd hh:mm:ss"),
                        Id = x.Id
                    }).ToList()
            };

            return viewModel;
        }
    }
}


