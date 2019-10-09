using MarketEngine.Domain.Service.Interfaces;
using MarketEngine.Model.Models;
using MarketEngine.Repository.Interfaces;
using System;

namespace MarketEngine.Domain.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public Product Create(Product product)
        {
            if (product == null)
                throw new InvalidOperationException("Cannot create a null product");

            return productRepository.Create(product);
        }

        public Product
    }
}
