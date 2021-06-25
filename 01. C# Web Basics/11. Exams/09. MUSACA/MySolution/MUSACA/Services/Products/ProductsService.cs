using MUSACA.Data;
using MUSACA.Data.Models;
using MUSACA.ViewModels.Home;
using MUSACA.ViewModels.Orders;
using MUSACA.ViewModels.Products;
using System;
using System.Linq;

namespace MUSACA.Services.Products
{
    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext db;

        public ProductsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void CreateProduct(CreateProductInputModel inputModel)
        {
            var product = new Product
            {
                Name = inputModel.Name,
                Price = inputModel.Price,
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();
        }

        public bool DoesProductExist(CreateProductInputModel inputModel)
        {
            return !db.Products.Any(x => x.Name == inputModel.Name);
        }

        public AllProductsViewModel GetAllProducts()
        {
            var viewModel = new AllProductsViewModel
            {
                Products = this.db.Products
                .Select(x => new ProductViewModel
                {
                    Name = x.Name,
                    Price = x.Price.ToString("f2")
                }).ToList()
            };

            return viewModel;
        }

        public void OrderProduct(string userId, OrderProductInputModel inputModel)
        {
            var product = this.db.Products
                .Where(x => x.Name == inputModel.Product)
                .FirstOrDefault();

            var order = this.db.Orders
                    .Where(x => x.UserId == userId
                        && x.Status == OrderStatus.Active)
                    .FirstOrDefault();

            if (product != null)
            {
                if (order == null)
                {
                    order = new Order
                    {
                        UserId = userId
                    };

                    var productOrder = new ProductOrder
                    {
                        Order = order,
                        Product = product
                    };

                    this.db.Orders.Add(order);
                    this.db.SaveChanges();
                    this.db.ProductOrders.Add(productOrder);
                    this.db.SaveChanges();
                }
                else
                {
                    var productOrder = new ProductOrder
                    {
                        Order = order,
                        Product = product
                    };
                    this.db.ProductOrders.Add(productOrder);
                    this.db.SaveChanges();
                }
                ;
            }
        }
    }
}
