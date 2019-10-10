using MarketEngine.Model.DTO.Requests;
using MarketEngine.Model.Enums;
using System;

namespace MarketEngine.Model.Models
{
    public class Product : ModelBase
    {
        public decimal Price { get; private set; }
        public string Description { get; private set; }
        public UnitType UnitType { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public DateTime ModifiedDateTime { get; private set; }
        public string CreatedByUserId { get; private set; }
        public string ModifiedByUserId { get; private set; }

        private Product()
        {
            CreatedDateTime = ModifiedDateTime = DateTime.Now;
        }

        public static Product GenerateProduct(CreateProductRequest productDto, string userId)
        {
            Product result = new Product();

            result.Name = productDto.Name;
            result.Price = productDto.Price;
            result.Description = productDto.Description;
            result.UnitType = productDto.UnitType;
            result.CreatedByUserId = result.ModifiedByUserId = userId;

            return result;
        }
    }
}
