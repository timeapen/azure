using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}