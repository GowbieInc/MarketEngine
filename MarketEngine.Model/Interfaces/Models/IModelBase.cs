using System;
using System.Collections.Generic;
using System.Text;

namespace MarketEngine.Model.Interfaces.Models
{
    public interface IModelBase
    {
        string Id { get; set; }
        string Name { get; set; }
    }
}
