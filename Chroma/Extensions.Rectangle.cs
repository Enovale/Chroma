using System.Drawing;
using System.Numerics;

namespace Chroma
{
    public static partial class Extensions
    {
        public static bool Contains(this Rectangle r, Vector2 v)
            => r.Contains((int)v.X, (int)v.Y);
    }
}