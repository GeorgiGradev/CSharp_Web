using Andreys.ViewModels.Products;
using System.Collections.Generic;

namespace Andreys.Services
{
    public interface IProductsService
    {
        public void Add(AddProductInputModel model);

        public IEnumerable<ViewAllProductsViewModel> GetAll();

        public ViewProductDetailsModel GetDetails(int id);

        public void DeleteProduct(int id);
    }
}