using AltV.Net;
using AltV.Net.Elements.Entities;
using System;

namespace MoonV.Factories
{
    public class ClassicColshape : Checkpoint
    {
        public int colshapeId { get; set; } = 0;
        public new float Radius { get; set; }
        public bool isCarDealerShape { get; set; } = false;
        public int carDealerShopId { get; set; } = 0;
        public uint carDealerHash { get; set; } = 0;

        public ClassicColshape(ICore server, IntPtr nativePointer) : base(server, nativePointer)
        {

        }

        public bool IsInRange(ClassicPlayer player)
        {
            lock (player)
            {
                if (!player.Exists) return false;
                return player.Position.Distance(Position) <= Radius;
            }
        }
    }
}
