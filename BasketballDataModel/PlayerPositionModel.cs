using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballDataModel
{

    [Table("PlayerPositions")]
    public class PlayerPositionModel
    {
        public int PlayerId { get; set; }
        public int PositionId { get; set; }

        [ForeignKey("PlayerId")]
        public PlayerPositionModel PlayerPosition { get; set; }

        [ForeignKey("PositionId")]
        public PlayerModel Player { get; set; }
    }
}
