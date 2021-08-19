using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Capture;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Kamera.src
{
    class Timer : ModSystem
    {
        bool F = false;
        bool T = true;
        public static int a, i;

           public override void PostUpdateTime()
        {
            if (Fix.IFPlayerFixed == T)
            {
                Main.LocalPlayer.position = Fix.fixedposition;
            }

            if(Fix.IFPointsFixed == T)
            {
                CaptureInterface.EdgeA = Main.screenPosition.ToTileCoordinates() + new Point(Fix.x, Fix.y);
                CaptureInterface.EdgeB = CaptureInterface.EdgeA + new Point(Fix.w, Fix.h);
            }

            base.PostUpdateTime();
            if(Capture1.IsFirst == T)
            {
                i++;
                if(i==Capture1.ticks)
                {
                    i = 0;
                    a++;

                    if (Capture1.IsScreenhots == F)
                    {
                        if (Fix.IFPointsFixed == F)
                        {
                            CaptureInterface.EdgeA = Capture1.TmpEdgeA;
                            CaptureInterface.EdgeB = Capture1.TmpEdgeB;
                        }

                        CaptureInterface.EdgeAPinned = CaptureInterface.EdgeBPinned = true;
                        Capture3.Settings();
                        CaptureInterface.StartCamera(Capture3.captureSettings);
                        while (Capture1.IsC == T)
                            CaptureInterface.EndCamera();
                    }
                    if (Capture1.IsScreenhots == T)
                    {
                        CaptureInterface.Settings.TransparentBackground = true;
                        CaptureInterface.QuickScreenshot();
                    }

                    if (a==Capture1.frame)
                    {
                        a = 0;
                        Capture1.IsFirst = F;
                    }
                }
            }
        }
    }
}
