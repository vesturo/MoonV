using AltV.Net.Data;

namespace MoonV.Utils
{
    class Constants
    {
        public static class DatabaseConfig
        {
            public static string Host = "localhost";
            public static string User = "root";
            public static string Password = "";
            public static string Port = "3306";
            public static string Database = "moonv";
        }
        public static string db = DatabaseConfig.Database;

        public partial class Positions
        {
            //Airport
            public static readonly Position spawnPosition_Airport = new Position(-1045.2131f, -2750.8748f, 21.360474f);
            public static readonly Position spawnPosition_PaletoBay = new Position(-264.932f, 6621.522f, 7f);         
        }
    }
}
