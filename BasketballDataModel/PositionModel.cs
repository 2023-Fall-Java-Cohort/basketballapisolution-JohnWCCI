using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketballDataModel
{
    [Table("Positions")]
    [Index("Name", IsUnique = true)]
    public class PositionModel : BaseModel
    {
        public virtual List<PlayerModel>?  Players { get; set; } 
    }
}
