using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoonV.dbmodels
{
    public partial class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string username { get; set; }
        public string password { get; set; }
        public ulong socialId { get; set; }
        public int adminlevel { get; set; }
        public bool isFirstLogin { get; set; }
    }

    public partial class Alphakeys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Alphakey { get; set; }
    }
}
