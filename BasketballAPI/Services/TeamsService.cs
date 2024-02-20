using BasketballAPI.Context;
using BasketballDataModel;

namespace BasketballAPI.Services
{
    public class TeamsService : Service<TeamModel>, ITeamsService
    {
        public TeamsService(BasketballContext dbContext, ILogger<TeamModel> logger) : base(dbContext, logger)
        {
        }
    }
}
