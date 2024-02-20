using BasketballAPI.Context;
using BasketballDataModel;

namespace BasketballAPI.Services
{
    public class PositionsService : Service<PositionModel>, IPositionsService
    {
        public PositionsService(BasketballContext dbContext, ILogger<PositionModel> logger) : base(dbContext, logger)
        {
        }
    }
}
