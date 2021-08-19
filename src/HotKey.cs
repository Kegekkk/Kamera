using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.UI.Chat;
using System;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.GameInput;
using Terraria.Graphics.Capture;

namespace Kamera.src
{
    class HotKeys : ModPlayer
    {
        bool F = false;
        bool T = true;
        bool on = false;
        public static ModKeybind Capture, FixPlayer, FixPoints, UI;
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Capture.JustPressed && Capture1.IsC == F)
            {

                if (Capture1.IsScreenhots == F)
                {
                    if (CaptureInterface.EdgeAPinned == T && CaptureInterface.EdgeBPinned == T)
                    {
                        Capture1.TmpEdgeA = CaptureInterface.EdgeA;
                        Capture1.TmpEdgeB = CaptureInterface.EdgeB;
                        Capture1.IsFirst = T;
                    }
                    else
                    {
                        Main.NewText("Kamera:未设置截取框！！！", 250, 82, 127);
                        Main.NewText("Kamera:Please set the two EdgePoints first!", 250, 82, 127);
                    }
                }

                if(Capture1.IsScreenhots == T)
                {
                    CaptureInterface.Settings.PackImage = true;
                    CaptureInterface.Settings.TransparentBackground = KameraUI.transparent;
                    CaptureInterface.QuickScreenshot();
                    Capture1.IsFirst = F;
                }
            }



            if(FixPlayer.JustPressed)
            {
                if (Fix.IFPlayerFixed == F)
                {
                    Fix.fixedposition= Main.LocalPlayer.position;
                    Fix.IFPlayerFixed = true;
                    Main.NewText("Kamera:人物位置已锁定", 82, 183, 250);
                    Main.NewText("Kamera:Character position is fixed", 82, 183, 250);
                }

                else 
                {
                    Fix.IFPlayerFixed = false;
                    Main.NewText("Kamera:人物位置已解锁", 82, 183, 250);
                    Main.NewText("Kamera:Character can move now", 82, 183, 250);
                }
                //*new Color(RanDom.r.Next(0, 256), RanDom.r.Next(0, 256), RanDom.r.Next(0, 256));
                //*Points.GetEdgePoint(0);
                //*string test = Points.player.ToString() + Main.LocalPlayer.Center.ToTileCoordinates().ToString() + Main.LocalPlayer.position.ToString() + Main.LocalPlayer.moveSpeed.ToString();
                //*Main.NewText(test, 255, 240, 20);
            }

            if(FixPoints.JustPressed)
            {
                if (Fix.IFPointsFixed == F)
                {
                    if (CaptureInterface.EdgeAPinned == T && CaptureInterface.EdgeBPinned == T)
                    {
                        Fix.TmpScreenP = Main.screenPosition.ToTileCoordinates();
                        Fix.TmpEdgeTL = Points.GetTopLeftPoint(CaptureInterface.EdgeA, CaptureInterface.EdgeB);
                        Fix.TmpEdgeBR = Points.GetButtonRightPoint(CaptureInterface.EdgeA, CaptureInterface.EdgeB);

                        Fix.x = Fix.TmpEdgeTL.X - Fix.TmpScreenP.X;
                        Fix.y = Fix.TmpEdgeTL.Y - Fix.TmpScreenP.Y;
                        Fix.w = Fix.TmpEdgeBR.X - Fix.TmpEdgeTL.X;
                        Fix.h = Fix.TmpEdgeBR.Y - Fix.TmpEdgeTL.Y;

                        Fix.IFPointsFixed = true;
                        Main.NewText("Kamera:人物与截取框的相对位置已锁定", 82, 183, 250);
                        Main.NewText("Kamera:You can move EdgePoints now", 82, 183, 250);

                    }
                    else
                    {
                        Main.NewText("Kamera:未设置截取框！！！", 250, 82, 127);
                        Main.NewText("Kamera:Please set the two EdgePoints first!", 250, 82, 127);
                    }
                }
                else
                {
                    Fix.IFPointsFixed = false;
                    Main.NewText("Kamera:人物与截取框的相对位置已解锁", 82, 183, 250);
                    Main.NewText("Kamera:You can move EdgePoints now", 82, 183, 250);
                }
            }

            if(UI.JustPressed)
            {
                if (on == F)
                {
                    //*Main.NewText(Capture1.frame.ToString() + "," +Capture1.ticks.ToString());
                    on = true;
                    ModContent.GetInstance<TheUI>().userInterface.SetState(TheUI.kameraUI);
                }
                else
                {
                    on = false;
                    ModContent.GetInstance<TheUI>().userInterface.SetState(null);
                }
            }

        }

        public override void OnEnterWorld(Player player)
        {
            //*ModContent.GetInstance<TheUI>().userInterface.SetState(TheUI.kameraUI);
            base.OnEnterWorld(player);
            CaptureInterface.Settings.MarkedAreaColor = Color.Transparent;
        }
    }
    class HotKeysL : ModSystem
    {
        public override void Load()
        {
            base.Load();
            HotKeys.Capture = KeybindLoader.RegisterKeybind(Mod, "截图/Capture", Keys.J);
            HotKeys.FixPlayer = KeybindLoader.RegisterKeybind(Mod, "固定人物位置/FixCharacterPosition", Keys.K);
            HotKeys.FixPoints = KeybindLoader.RegisterKeybind(Mod, "固定人物和截取框的相对位置//FixEdgePoints", Keys.L);
            HotKeys.UI = KeybindLoader.RegisterKeybind(Mod, "UI界面///ShowUI", Keys.U);
        }
    }
}
