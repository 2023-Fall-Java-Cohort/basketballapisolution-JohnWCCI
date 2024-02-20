using BasketballDataModel;

namespace BasketballAPI.Services
{
    public interface IPlayersService : IService<PlayerModel>
    {
        ValueTask<List<PlayerModel>> GetPlayersPaging(int pageIndex, int pageSize, CancellationToken cancellation = default(CancellationToken));
    }
}
