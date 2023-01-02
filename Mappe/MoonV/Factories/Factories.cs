using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using System;

namespace MoonV.Factories
{
    public class AccountsFactory : IEntityFactory<IPlayer>
    {
        public IPlayer Create(ICore server, IntPtr playerPointer, ushort id)
        {
            return new ClassicPlayer(server, playerPointer, id);
        }
    }

    public class ColshapeFactory : IBaseObjectFactory<IColShape>
    {
        public IColShape Create(ICore server, IntPtr entityPointer)
        {
            return new ClassicColshape(server, entityPointer);
        }
    }
}
