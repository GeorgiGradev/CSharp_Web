using MUSACA.ViewModels.Orders;
using MUSACA.ViewModels.Products;

namespace MUSACA.Services.Products
{
    public interface IProductsService
    {
        void CreateProduct(CreateProductInputModel inputModel);

        void OrderProduct(string userId, OrderProductInputModel inputModel);

        bool DoesProductExist(CreateProductInputModel inputModel);

        AllProductsViewModel GetAllProducts();
    }
}
