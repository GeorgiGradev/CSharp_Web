using MUSACA.Data;
using MUSACA.Data.Models;
using System;
using System.Linq;

namespace MUSACA.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext db;

        public OrdersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CompleteOrder(string userId)
        {
            var order = this.db.Orders
                .Where(x => x.Status == OrderStatus.Active
                    && x.Products.Any(y => y.Order.UserId == userId))
                .FirstOrDefault();

            order.Status = OrderStatus.Completed;
            db.SaveChanges();
        }
    }
}
