using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.EntitySync;
using AltV.Net.EntitySync.ServerEvent;
using AltV.Net.EntitySync.SpatialPartitions;
using MoonV.Database;
using MoonV.Factories;
using MoonV.Handler;
using MoonV.Utils;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading;

namespace MoonV
{
    internal class Main : AsyncResource
    {
        public static int mainThreadId = Thread.CurrentThread.ManagedThreadId;
        public static bool serverIsStarting = true;

        public override IEntityFactory<IPlayer> GetPlayerFactory()
        {
            return new AccountsFactory();
        }

        public override IBaseObjectFactory<IColShape> GetColShapeFactory()
        {
            return new ColshapeFactory();
        }

        public override void OnStart()
        {
            // Timer

            AltEntitySync.Init(7, (threadId) => 200, (threadId) => false,
                (threadCount, repository) => new ServerEventNetworkLayer(threadCount, repository),
                (entity, threadCount) => entity.Type,
                (entityId, entityType, threadCount) => entityType,
                (threadId) =>
                {
                    return threadId switch
                    {
                        // Marker
                        0 => new LimitedGrid3(50_000, 50_000, 75, 10_000, 10_000, 64),
                        // Text
                        1 => new LimitedGrid3(50_000, 50_000, 75, 10_000, 10_000, 32),
                        // Props
                        2 => new LimitedGrid3(50_000, 50_000, 100, 10_000, 10_000, 1500),
                        // Help Text
                        3 => new LimitedGrid3(50_000, 50_000, 100, 10_000, 10_000, 1),
                        // Blips
                        4 => new EntityStreamer.GlobalEntity(),
                        // Dynamic Blip
                        5 => new LimitedGrid3(50_000, 50_000, 175, 10_000, 10_000, 200),
                        // Ped
                        6 => new LimitedGrid3(50_000, 50_000, 175, 10_000, 10_000, 64),
                        _ => new LimitedGrid3(50_000, 50_000, 175, 10_000, 10_000, 115),
                    };
                },
            new IdProvider());      

            DbHandler.LoadDatabase();
            TimerHandler.LoadTimers();
            HelperMethods.LogColored("~lg~[MoonV] ~w~Server gestartet.");
            HelperMethods.SendDiscordLog("Server Status", "Der Server wurde erfolgreich gestartet.", "green");
            serverIsStarting = false;
        }

        public override void OnStop()
        {
            Console.WriteLine("Stopped");
        }        
    }
}
