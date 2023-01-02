using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoonV.dbmodels
{
    public partial class Character
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int accountId { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int gender { get; set; } // 0 = Male, 1 = Female
        public string birthday { get; set; }
        public int cash { get; set; }
        public int bank { get; set; }
        public int health { get; set; }
        public int armor { get; set; }
    }
}
