using MarketEngine.Model.Models;
using MarketEngine.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace MarketEngine.Repository.Persistence
{
    public class ProductRepository : SimpleRepository<Product>, IProductRepository
    {
        private readonly ILogger<Product> logger;

        public ProductRepository(ILogger<Product> logger)
        {
            this.logger = logger;
        }

        
    }
}
