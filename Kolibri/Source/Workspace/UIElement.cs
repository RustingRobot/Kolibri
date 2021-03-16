using Kolibri.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kolibri.Source.Workspace
{
    public class UIElement : ESprite2d //general Class for all UI Elements
    {
        public Vector2 relativePos;
        public Window window;
        public string tag;

        public UIElement(Window WINDOW, Vector2 POS, Vector2 DIM) : base(null, POS, DIM)
        {
            window = WINDOW;
            relativePos = POS;
        }
        public override void Update(Vector2 OFFSET)
        {
            if(window != null) pos = relativePos + window.pos; //pos needs to be a combination of the windows position and an offset. It also has to be updated every frame
            base.Update(OFFSET);
        }

        public bool Clicked()
        {
            if (window != null)
                return Globals.mouse.LeftClick() && Globals.GetBoxOverlap(pos, dim, Globals.mouse.newMousePos, Vector2.Zero) && Globals.GetBoxOverlap(new Vector2(window.pos.X + window.border, window.pos.Y + window.border), new Vector2(window.dim.X - window.border * 2, window.dim.Y - window.border * 2), Globals.mouse.newMousePos, Vector2.Zero);
            else
                return Globals.mouse.LeftClick() && Globals.GetBoxOverlap(pos, dim, Globals.mouse.newMousePos, Vector2.Zero);
        }

        public bool MouseHover()
        {
            if (window != null)
                return Globals.GetBoxOverlap(pos, dim, Globals.mouse.newMousePos, Vector2.Zero) && Globals.GetBoxOverlap(new Vector2(window.pos.X + window.border, window.pos.Y + window.border), new Vector2(window.dim.X - window.border * 2, window.dim.Y - window.border * 2), Globals.mouse.newMousePos, Vector2.Zero);
            else
                return Globals.GetBoxOverlap(pos, dim, Globals.mouse.newMousePos, Vector2.Zero);
        }
    }
}
