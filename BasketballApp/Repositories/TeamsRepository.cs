﻿using BasketballDataModel;

namespace BasketballApp.Repositories
{
    public class TeamsRepository : BaseRepository<TeamModel>, ITeamRepository
    {
        public TeamsRepository(IConfiguration configuration, HttpClient client, ILogger logger) 
            : base("Teams", configuration, client, logger)
        {
        }
    }
}
