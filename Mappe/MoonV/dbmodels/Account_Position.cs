using AltV.Net.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoonV.dbmodels
{
    public partial class Account_Position
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int accId { get; set; }
        public Position position { get; set; }
        public int dimension { get; set; }
    }
}
