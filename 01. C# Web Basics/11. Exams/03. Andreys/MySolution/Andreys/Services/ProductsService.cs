using System;
using System.Collections.Generic;
using System.Linq;
using Andreys.Data;
using Andreys.Data.Enums;
using Andreys.ViewModels.Products;


namespace Andreys.Services
{
    public class ProductsService : IProductsService
    {
        private readonly AndreysDbContext db;

        public ProductsService(AndreysDbContext db)
        {
            this.db = db;
        }

        public void Add(AddProductInputModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Gender = Enum.Parse<Gender>(model.Gender),
                Category = Enum.Parse<Category>(model.Category),
                Price = model.Price
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = this.db.Products.FirstOrDefault(x => x.Id == id);
            this.db.Products.Remove(product);
            this.db.SaveChanges();
        }

        public IEnumerable<ViewAllProductsViewModel> GetAll()
        {
            var products = this.db.Products.Select(x => new ViewAllProductsViewModel
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                Price = x.Price
            }).ToList();

            return products;
        }

        public ViewProductDetailsModel GetDetails(int id)
        {
            var product = this.db.Products.FirstOrDefault(x => x.Id == id);
            var details = new ViewProductDetailsModel
            {
                Id = product.Id,
                Category = product.Category.ToString(),
                Gender = product.Gender.ToString(),
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Price = product.Price
            };
            return details;
        }
    }
}