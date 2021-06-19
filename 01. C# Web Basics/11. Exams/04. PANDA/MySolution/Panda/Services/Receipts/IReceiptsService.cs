using Panda.ViewModels.Receipts;

namespace Panda.Services.Receipts
{
    public interface IReceiptsService
    {
        AllReceiptsViewModel GetAllReceipts(string userId);
    }
}
