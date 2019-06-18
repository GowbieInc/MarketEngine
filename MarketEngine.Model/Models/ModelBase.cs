using MarketEngine.Model.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketEngine.Model.Models
{
    public class ModelBase: IModelBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
