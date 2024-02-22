using BasketballApp.Controllers;
using BasketballApp.Repositories;
using BasketballDataModel;

namespace BasketballApp
{
    public static class Startup
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<ICoachesRepository, CoachesRepository>();
            services.AddScoped<IPlayerRepository,PlayersRepository>();
            services.AddScoped<ITeamRepository, TeamsRepository>();
            services.AddScoped<IPositionRepository, PositionsRepository>();
            services.AddSingleton<IErrorHandler, ErrorHandler>();
            return services;
        }
    }
}
