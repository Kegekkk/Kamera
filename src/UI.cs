using Terraria;
using Terraria.UI;
using Terraria.GameContent;
using ReLogic.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;

namespace Kamera.src
{
    class TheUI : ModSystem 
    {
        internal UserInterface userInterface;
        public static UIState kameraUI,rec;

        public override void Load()
        {
            // UI
            if (!Main.dedServ)
            {
                userInterface = new UserInterface();
                userInterface.SetState(null);

                kameraUI = new KameraUI();
                kameraUI.Activate();

            }
        }

        public override void Unload()
        {
            userInterface = null;
            kameraUI = null;
        }

        private GameTime _lastUpdateUiGameTime;
        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;

            if (userInterface?.CurrentState != null)
            {
                userInterface.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Kamera: userInterface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && userInterface?.CurrentState != null)
                        {
                            userInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));
            }
        }
    }

    class KameraUI : UIState
    {
        public static Number frame,ticks;
        public static UIText qs, tb;
        public static bool transparent = true;
        public override void OnInitialize()
        {
            //实例化一个面板
            DragableUIPanel panel = new("Kamera",500,300);
            Append(panel);

            UIText f = new("连续截图次数:" ,1.1f);
            f.VAlign = 0.3f;
            f.HAlign = 0.1f;
            panel.Append(f);

            UIText fE = new("How much times you want to snapshoot:", 1f);
            fE.VAlign = 0.2f;
            fE.HAlign = 0.1f;
            panel.Append(fE);

            UIText t = new("截图间隔tick数:", 1.1f);
            t.VAlign = 0.5f;
            t.HAlign = 0.1f;
            panel.Append(t);

            UIText tE = new("ticks between two snapshot:", 1f);
            tE.VAlign = 0.4f;
            tE.HAlign = 0.1f;
            panel.Append(tE);

            qs = new("快速截屏/QuickScreenhost - OFF", 1.1f);
            qs.Width.Set(24, 0);
            qs.Height.Set(60, 0);
            qs.VAlign = 0.8f;
            qs.HAlign = 0.5f;
            qs.OnClick += Qs_OnClick;
            panel.Append(qs);

            tb = new("透明背景/TransparentBackground - ON", 1.1f);
            tb.Width.Set(24, 0);
            tb.Height.Set(60, 0);
            tb.VAlign = 0.9f;
            tb.HAlign = 0.5f;
            tb.OnClick += Tb_OnClick;
            panel.Append(tb);

            frame = new(14);
            frame.VAlign = 0.29f;
            frame.HAlign = 0.76f;
            panel.Append(frame);

            ticks = new(29);
            ticks.VAlign = 0.47f;
            ticks.HAlign = 0.77f;
            panel.Append(ticks);

            UIImage button1 = new(ModContent.Request<Texture2D>("Kamera/UI/subtract-button"));
            button1.Width.Set(24, 0);
            button1.Height.Set(24, 0);
            button1.OnClick += Button1_OnClick;
            button1.VAlign = 0.3f;
            button1.HAlign = 0.7f;
            panel.Append(button1);

            UIImage button2 = new(ModContent.Request<Texture2D>("Kamera/UI/plus-button"));
            button2.Width.Set(24, 0);
            button2.Height.Set(24, 0);
            button2.OnClick += Button2_OnClick;
            button2.VAlign = 0.3f;
            button2.HAlign = 0.9f;
            panel.Append(button2);

            UIImage button3 = new(ModContent.Request<Texture2D>("Kamera/UI/subtract-button"));
            button3.Width.Set(24, 0);
            button3.Height.Set(24, 0);
            button3.OnClick += Button3_OnClick;
            button3.VAlign = 0.5f;
            button3.HAlign = 0.7f;
            panel.Append(button3);

            UIImage button4 = new(ModContent.Request<Texture2D>("Kamera/UI/plus-button"));
            button4.Width.Set(24, 0);
            button4.Height.Set(24, 0);
            button4.OnClick += Button4_OnClick;
            button4.VAlign = 0.5f;
            button4.HAlign = 0.9f;
            panel.Append(button4);

            base.OnInitialize();
        }

        private void Tb_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if(transparent == true)
            {
                transparent = false;
                tb.SetText("透明背景/TransparentBackground - OFF");
            }
            else
            {
                transparent = true;
                tb.SetText("透明背景/TransparentBackground - ON");
            }
        }

        private void Qs_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if(Capture1.IsScreenhots == false)
            {
                Capture1.IsScreenhots = true;
                qs.SetText("快速截屏/QuickScreenhost - On");
            }
            else
            {
                Capture1.IsScreenhots = false;
                qs.SetText("快速截屏/QuickScreenhost - OFF");
            }
        }

        private void Button4_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            ticks.number++;
            Capture1.ticks = ticks.number;
        }

        private void Button3_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (ticks.number == 6)
            {
                Main.NewText("间隔tick数小于6可能导致截图次数减少",250, 82, 127);
                Main.NewText("ticks is below 6 now,you cannot get as many images as you want", 250, 82, 127);
            }
            if (ticks.number > 1)
            {
                ticks.number--;
            }
            Capture1.ticks = ticks.number;
        }

        private void Button2_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            frame.number++;
            Capture1.frame = frame.number;
        }

        private void Button1_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (frame.number > 1)
            {
                frame.number--;
            }
            Capture1.frame = frame.number;
        }
    }

    class Number : UIElement
    {
        public int number { get; set; }

        public Number(int i)
        {
            number = i;
        }

        UIText num;

        public override void OnInitialize()
        {
            num = new (number.ToString(),1.1f);
            Append(num);

            base.OnInitialize();
        }

        public override void Update(GameTime gameTime)
        {
            num.SetText(number.ToString());
            Append(num);
            base.Update(gameTime);
        }

    }


    public class DragableUIPanel : UIPanel
    {
        public bool active = false;
        public event Action OnCloseBtnClicked;
        internal UIPanel header;

        public DragableUIPanel(string headingtext, float width, float height)
        {
            Width.Set(width, 0f);
            Height.Set(height, 0f);
            SetPadding(0);

            header = new UIPanel();
            header.SetPadding(0);
            header.Width = Width;
            header.Height.Set(30, 0f);
            header.BackgroundColor.A = 255;
            header.OnMouseDown += Header_OnMouseDown;
            header.OnMouseUp += Header_OnMouseUp;
            Append(header);

            var heading = new UIText(headingtext, 0.9f);
            heading.VAlign = 0.5f;
            heading.MarginLeft = 16f;
            header.Append(heading);
        }
        #region Drag code yoiked from ExampleMod 

        private Vector2 offset;
        public bool dragging;
        public static Vector2 lastPos = new Vector2(600, 200);
        public void Header_OnMouseDown(UIMouseEvent evt, UIElement elm)
        {
            base.MouseDown(evt);
            offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
            dragging = true;
        }

        public void Header_OnMouseUp(UIMouseEvent evt, UIElement elm)
        {
            base.MouseUp(evt);
            dragging = false;

            Left.Set(evt.MousePosition.X - offset.X, 0f);
            Top.Set(evt.MousePosition.Y - offset.Y, 0f);
            Recalculate();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); // don't remove.

            // Checking ContainsPoint and then setting mouseInterface to true is very common. This causes clicks on this UIElement to not cause the player to use current items. 
            if (ContainsPoint(Main.MouseScreen))
                Main.LocalPlayer.mouseInterface = true;


            if (dragging)
            {
                Left.Set(Main.mouseX - offset.X, 0f);
                Top.Set(Main.mouseY - offset.Y, 0f);
                Recalculate();

                lastPos = new Vector2(Left.Pixels, Top.Pixels);
            }

            // Here we check if the DragableUIPanel is outside the Parent UIElement rectangle. 
            // (In our example, the parent would be ExampleUI, a UIState. This means that we are checking that the DragableUIPanel is outside the whole screen)
            // By doing this and some simple math, we can snap the panel back on screen if the user resizes his window or otherwise changes resolution.
            var parentSpace = Parent.GetDimensions().ToRectangle();
            if (!GetDimensions().ToRectangle().Intersects(parentSpace))
            {
                Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
                Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
                // Recalculate forces the UI system to do the positioning math again.
                Recalculate();
            }
        }
        #endregion
    }

    class RanDom : Random
    {
        public static Random r = new();
        public static Color randomcolor = new Color(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
    }

}
