using System.Collections;
using System.Collections.Generic;

namespace Panda.ViewModels.Receipts
{
    public class AllReceiptsViewModel
    {
        public IEnumerable<ReceiptViewModel> AllReceipts { get; set; }
    }
}
