using Panda.Services;
using Panda.ViewModels.Receipts;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Linq;

namespace Panda.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly IReceiptsService receiptsService;

        public ReceiptsController(IReceiptsService receiptsService)
        {
            this.receiptsService = receiptsService;
        }

        public HttpResponse Index()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.receiptsService.GetAll()
                .Select(x => new ReceiptViewModel
                {
                    Id = x.Id,
                    Fee = x.Fee,
                    IssuedOn = x.IssuedOn,
                    RecipientName = x.Recipient.Username
                }).ToList();

            return this.View(viewModel);
        }
    }
}
