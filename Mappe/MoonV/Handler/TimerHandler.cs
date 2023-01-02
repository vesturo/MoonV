using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using MoonV.Factories;
using MoonV.Models;
using System.Linq;
using System.Timers;
using System;
using MoonV.Utils;

namespace MoonV.Handler
{
    class TimerHandler
    {
        public static void LoadTimers()
        {
            Timer entityTimer = new ();
            entityTimer.Elapsed += new ElapsedEventHandler(OnEntityTimer);
            entityTimer.Interval += 60000;
            entityTimer.Enabled = true;
        }

        public static void OnEntityTimer(object sender, ElapsedEventArgs e)
        {
            foreach (IPlayer player in Alt.GetAllPlayers().ToList())
            {
                if (player == null) continue;
                if (player == null || !player.Exists) continue;
                lock (player)
                {
                    if (((ClassicPlayer)player).accountId != 0)
                    {
                        Accounts.ChangeLastPosition(((ClassicPlayer)player).accountId, player.Position, player.Dimension);
                        Characters.SetCharacterHealth(((ClassicPlayer)player).accountId, player.Health - 100);
                        Characters.SetCharacterArmor(((ClassicPlayer)player).accountId, player.Armor);
                    }
                }
            }
        }
    }
}
