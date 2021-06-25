using MUSACA.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace MUSACA.Controllers
{
    class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public HttpResponse Cashout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();
            this.ordersService.CompleteOrder(userId);
            return this.Redirect("/");
        }
    }
}
