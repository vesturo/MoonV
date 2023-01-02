namespace DiscordBot
{
    public static class Configuration
    {
        public partial class ConfigurationData
        {
            public string discordBotToken { get; set; }
            public ulong discordBotChannel { get; set; }
        }

        public static string discordBotToken;
        public static ulong discordBotChannel;

        public static void FetchConfiguration()
        {
            string BotToken = "";
            ulong BotChannel = 123;
            discordBotToken = BotToken;
            discordBotChannel = BotChannel;
        }
    }
}
