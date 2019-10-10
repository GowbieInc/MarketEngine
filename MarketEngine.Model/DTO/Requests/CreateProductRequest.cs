using MarketEngine.Model.Enums;

namespace MarketEngine.Model.DTO.Requests
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public UnitType UnitType { get; set; }
    }
}
