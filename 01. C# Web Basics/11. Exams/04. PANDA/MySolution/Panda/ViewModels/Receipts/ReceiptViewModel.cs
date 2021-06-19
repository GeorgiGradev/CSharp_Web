namespace Panda.ViewModels.Receipts
{
    public class ReceiptViewModel
    {
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public string IssuedOn { get; set; }

        public string RecipientName { get; set; }
    }
}