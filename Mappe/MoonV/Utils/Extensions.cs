using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using MoonV.Factories;

namespace MoonV.Utils
{
    public static class Extensions
    {
        public static bool IsInRange(this Position currentPosition, Position otherPosition, float distance)
            => currentPosition.Distance(otherPosition) <= distance;
    }
}
