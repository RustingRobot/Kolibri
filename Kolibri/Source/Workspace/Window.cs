using Kolibri.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kolibri.Source.workspace
{
    public class Window : ESprite2d
    {
        public bool delete, dragged;
        public int handleHeight = 30, border = 4;
        private string title;
        private Vector2 clickOffset;
        public Window(Vector2 POS, Vector2 DIM, string TITLE) : base("Square", POS, DIM)
        {
            title = TITLE;
            
        }

        public override void Update(Vector2 OFFSET)
        {
            if (dragged || (Globals.mouse.LeftClickHold() && Globals.GetBoxOverlap(new Vector2(pos.X - dim.X / 2, pos.Y - dim.Y / 2), new Vector2(dim.X, handleHeight), new Vector2(Globals.mouse.newMouse.X, Globals.mouse.newMouse.Y), Vector2.Zero)))
            {
                dragged = true;
                clickOffset = Globals.mouse.oldMousePos - pos;
                pos = Globals.mouse.newMousePos - clickOffset;
            }
            if (!Globals.mouse.LeftClickHold())
            {
                dragged = false;
            }
            base.Update(OFFSET);
        }
        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            Globals.primitives.DrawRect(new Vector2(pos.X - dim.X / 2, pos.Y - dim.Y / 2), new Vector2(dim.X, handleHeight), new Color(22, 24, 26));
            Globals.primitives.DrawRect(new Vector2(pos.X - dim.X / 2 + border, pos.Y - dim.Y / 2 + border), new Vector2(dim.X - border*2, handleHeight - border*2), new Color(32, 34, 36));
            Globals.primitives.DrawTxt(title, new Vector2(pos.X - dim.X / 2 + 8, pos.Y - dim.Y / 2 + 2), new Vector2(0.7f, 0.7f), new Color(62, 64, 66));
        }
    }
}
