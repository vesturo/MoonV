using AltV.Net;
using AltV.Net.Async;
using AltV.Net.ColoredConsole;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using MoonV.Factories;
using System.Threading;
using System.Threading.Tasks;

namespace MoonV.Utils
{
    public class HelperMethods : IScript
    {

        [AsyncClientEvent("Server:CEF:setCefStatus")]
        public async static Task ClientEvent_setCefStatus(ClassicPlayer player, bool state)
        {
            if (player == null || !player.Exists) return;
            await AltAsync.Do(() =>
            {
                player.SetSyncedMetaData("IsCefOpen", state);
            });
        }

        public static void LogColored(string msg)
        {
            ColoredMessage cMessage = new ColoredMessage();
            cMessage += $"{msg}";
            Alt.LogColored(cMessage);
        }

        public static void SendDiscordLog(string title, string desc, string color)
        {
            Alt.Emit("DiscordBot:DiscordLog", $"{title}", $"{desc}", $"{color}");
        }

        internal static void TriggerClientEvent(IPlayer player, string eventName, params object[] args)
        {
            if (player == null) return;
            if (Thread.CurrentThread.ManagedThreadId != Main.mainThreadId) player.EmitLocked(eventName, args);
            else player.Emit(eventName, args);
        }

        internal static ClassicColshape CreateColShapeSphere(Position position, int dimension, float range)
        {
            if(Main.mainThreadId != Thread.CurrentThread.ManagedThreadId)
            {
                return AltAsync.Do(() =>
                {
                    ClassicColshape colshape = (ClassicColshape)Alt.CreateColShapeSphere(position, range);
                    colshape.Dimension = dimension;
                    //colshape.IsPlayersOnly = isPlayerOnly;
                    return colshape;
                }).Result;
            }
            else
            {
                ClassicColshape colshape = (ClassicColshape)Alt.CreateColShapeSphere(position, range);
                colshape.Dimension = dimension;
                //colshape.IsPlayersOnly = isPlayerOnly;
                return colshape;
            }
        }
    }
}
