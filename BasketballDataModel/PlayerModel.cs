using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketballDataModel
{
    [Table("Players")]
    [Index("Name", IsUnique = false)]
    public class PlayerModel : BaseModel
    {
       
        private int nationalRank;
        private DateTime birthdate;
        private bool isActive;
        private decimal salary;
        private int teamId;

       
        public int NationalRank { get => nationalRank; set => nationalRank = value; }
        public DateTime Birthdate { get => birthdate; set => birthdate = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public decimal Salary { get => salary; set => salary = value; }
        public int TeamId { get => teamId; set => teamId = value; }

        [ForeignKey("TeamId")]
        public virtual TeamModel? Team { get;set; }
        public int Age
        {
            get
            {
                return DateTime.Now.Year - Birthdate.Year;
            }
        }
    }
}
