using System.ComponentModel;

namespace MarketEngine.Model.Enums
{
    public enum UserType
    {
        [Description("Adminstrador do Sistema")]
        Administrator,
        [Description("Vendedor")]
        Seller,
        [Description("Cliente Externo")]
        Client
    }
}
