using MarketEngine.Model.Models;

namespace MarketEngine.Domain.Service.Interfaces
{
    public interface IProductService
    {
        Product Create(Product product);
        Product GetById(string productId);
    }
}
