using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Kamera.src
{
    class Points : ModPlayer
    {
        public static Vector2 player;
        public static Point GetTopLeftPoint(Point pointa,Point pointb)
        {
            int smallX = Math.Min(pointa.X, pointb.X);
            int smallY = Math.Min(pointa.Y, pointb.Y);
            return new Point(smallX, smallY);
        }

        public static Point GetButtonRightPoint(Point pointa, Point pointb)
        {
            int X = Math.Max(pointa.X, pointb.X);
            int Y = Math.Max(pointa.Y, pointb.Y);
            return new Point(X, Y);
        }
    }
}
