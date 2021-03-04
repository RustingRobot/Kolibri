using Kolibri.Engine;
using Kolibri.Engine.Input;
using Kolibri.Source;
using Kolibri.Source.Workspace;
using Kolibri.Source.Workspace.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Kolibri.Source.Workspace
{
    public class Frame: UIElement
    {
        UInt32[] pixels;
        Timeline timeline;

        Color color;
        int border = 2;

        public Frame(Window WINDOW, Timeline TIMELINE): base(WINDOW, Vector2.Zero, new Vector2(15, 25))
        {
            color = Color.Gray;
            timeline = TIMELINE;
            pixels = new uint[(int)Globals.canvas.dim.X*(int)Globals.canvas.dim.Y];
            /*wenn ein Frame hinzugefügt wird, ist der erstmal Weiß, 
            dann kann man ja wenn man die Timeline hat, sich in einem 
            bestimmten Frame befindet(also der aktuelle), und auf 
            einen Button klickt der dann die Pixels vom Canvas auf den 
            aktuellen Frame kopiert*/
            clearFrame();
        }

        public override void Update(Vector2 OFFSET)
        {
            pos = OFFSET + window.pos;
            if (timeline.frames[timeline.currentFrame] == this)
            {
                color = new Color(60, 104, 148);
                pixels = Globals.canvas.pixels;
            } 
            else if (MouseHover())
            {
                color = Color.LightGray;
                if (Clicked())
                {
                    timeline.currentFrame = timeline.frames.IndexOf(this);
                    Globals.canvas.pixels = pixels;
                }
            }
            else color = Color.Gray;
            base.Update(OFFSET);
        }
        public override void Draw(Vector2 OFFSET)
        {
            if (Array.TrueForAll(pixels, y => y == 0xFFFFFFFF))
            {
                Globals.primitives.DrawRect(OFFSET + window.pos, new Vector2(dim.X,border), color);
                Globals.primitives.DrawRect(OFFSET + window.pos, new Vector2(border, dim.Y), color);
                Globals.primitives.DrawRect(new Vector2(OFFSET.X + window.pos.X + dim.X - border, OFFSET.Y + window.pos.Y), new Vector2(border, dim.Y), color);
                Globals.primitives.DrawRect(new Vector2(OFFSET.X + window.pos.X, OFFSET.Y + window.pos.Y + dim.Y - border), new Vector2(dim.X, border), color);
            }
            else Globals.primitives.DrawRect(OFFSET + window.pos, dim, color);
        }

        public void clearFrame()
        {
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = 0xFFFFFFFF;
            }
        }
    }
}