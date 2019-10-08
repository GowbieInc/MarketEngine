using MarketEngine.Domain.Services.Status.Interfaces;

namespace MarketEngine.Domain.Services.Status.Services
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
