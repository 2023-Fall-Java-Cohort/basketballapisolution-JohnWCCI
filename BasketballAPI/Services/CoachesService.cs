using BasketballAPI.Context;
using BasketballDataModel;

namespace BasketballAPI.Services
{
    public class CoachesService : Service<CoachModel>, ICoachesService
    {
        public CoachesService(BasketballContext dbContext, ILogger<CoachModel> logger) : base(dbContext, logger)
        {
        }
    }
}
