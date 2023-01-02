using AltV.Net;
using AltV.Net.Elements.Entities;
using System;

namespace MoonV.Factories
{
    public class ClassicPlayer : Player
    {
        public int accountId { get; set; } = 0;
        public string accountName { get; set; } = "undefined";
        public int adminlevel { get; set; } = 0;
        public int jailtime { get; set; } = 0;
        public bool positionShouldBeSaved { get; set; } = false;

        public ClassicPlayer(ICore server, IntPtr nativePointer, ushort id) : base(server, nativePointer, id)
        {
        }
    }
}
