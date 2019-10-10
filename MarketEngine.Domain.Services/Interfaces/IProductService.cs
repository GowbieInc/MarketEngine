using MarketEngine.Model.DTO.Requests;
using MarketEngine.Model.Models;

namespace MarketEngine.Domain.Service.Interfaces
{
    public interface IProductService
    {
        Product Create(CreateProductRequest request, string userId);
      //  Product GetById(string productId);
    }
}
