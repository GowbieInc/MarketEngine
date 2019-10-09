using MarketEngine.Domain.Service.Interfaces;

namespace MarketEngine.Domain.Service.Services
{
    public class StatusService : IStatusService
    {
        public StatusService()
        {

        }

        public string TestService()
        {
            return "Pingou :)";
        }
    }
}
