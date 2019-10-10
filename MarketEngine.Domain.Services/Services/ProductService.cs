using MarketEngine.Domain.Service.Interfaces;
using MarketEngine.Model.DTO.Requests;
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

        public Product Create(CreateProductRequest request, string userId)
        {
            ValidateCreationRequest(request, userId);

            return productRepository.Create(Product.GenerateProduct(request, userId));
        }

        private void ValidateCreationRequest(CreateProductRequest request, string userId)
        {
            if (request == null)
                throw new InvalidOperationException("Cannot create a null product");

            if (string.IsNullOrWhiteSpace(userId)) 
                throw new InvalidOperationException("Cannot create a product (UNIDENTIFIED USER)");
        }
    }
}
