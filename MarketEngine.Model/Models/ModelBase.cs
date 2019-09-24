using MarketEngine.Model.Interfaces.Models;

namespace MarketEngine.Model.Models
{
    public class ModelBase: ISimpleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
