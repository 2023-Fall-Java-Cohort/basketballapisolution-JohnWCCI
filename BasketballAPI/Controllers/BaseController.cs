using BasketballAPI.Services;
using BasketballDataModel;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAPI.Controllers
{
    [ApiController]
    public abstract class BaseController<TEntity> : ControllerBase where TEntity : class
    {
        private IService<TEntity> service;
        private ILogger logger;

        public BaseController(IService<TEntity> service, ILogger logger)
        {
            this.service = service;
            this.logger = logger;

        }

        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<TEntity>>> GetAll(CancellationToken cancellation = default(CancellationToken))
        {
            logger.LogInformation("call GetAll");
            try
            {
                return await service.GetAllAsync(cancellation);
                    
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Problem($"{ex.Message}:GetAll");
            }
        }
    }
}
