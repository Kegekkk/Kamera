using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.UI.Chat;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameInput;
using Terraria.Graphics.Capture;

namespace Kamera.src
{
    class Capture1 : CaptureManager
    {
        public static Point TmpEdgeA, TmpEdgeB;
        public static int frame = 14;
        public static int ticks = 29;
        public static bool IsFirst = false,IsScreenhots =false;


        public static CaptureManager captureManager = new();
        public static bool IsC = captureManager.IsCapturing;
    }
    class Capture2 : CaptureInterface
    {
    }
    class Capture3 : CaptureSettings
    {
        public static CaptureSettings captureSettings = new();
        static Rectangle aera = new Rectangle(1, 1, 100, 100);
        public static void Settings()
        {
            captureSettings.CaptureBackground = !KameraUI.transparent;
            captureSettings.CaptureEntities = true;
            captureSettings.Area = CaptureInterface.GetArea();
            captureSettings.UseScaling = true;
            captureSettings.CaptureMech = false;
            captureSettings.OutputName = DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "-" + DateTime.Now.Millisecond.ToString();
        }
    }
}
