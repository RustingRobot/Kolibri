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
        public int handleHeight = 24, border = 3;
        private string title;
        private Vector2 clickOffset;
        public Window(Vector2 POS, Vector2 DIM, string TITLE) : base("Square", POS, DIM)
        {
            title = TITLE;
            
        }

        public override void Update(Vector2 OFFSET)
        {
            if (dragged || (!Globals.dragging && Globals.mouse.LeftClickHold() && Globals.GetBoxOverlap(new Vector2(pos.X - dim.X / 2, pos.Y - dim.Y / 2), new Vector2(dim.X, handleHeight), new Vector2(Globals.mouse.newMouse.X, Globals.mouse.newMouse.Y), Vector2.Zero)))
            {
                Globals.dragging = true;
                dragged = true;
                clickOffset = Globals.mouse.oldMousePos - pos;
                pos = Globals.mouse.newMousePos - clickOffset;
            }
            if (!Globals.mouse.LeftClickHold())
            {
                Globals.dragging = false;
                dragged = false;
            }
            base.Update(OFFSET);
        }
        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            //Globals.primitives.DrawRect(new Vector2(pos.X - dim.X / 2, pos.Y - dim.Y / 2), new Vector2(dim.X, handleHeight), new Color(22, 24, 26)); //outer
            Globals.primitives.DrawRect(new Vector2(pos.X - dim.X / 2 + border, pos.Y - dim.Y / 2 + border + handleHeight), new Vector2(dim.X - border*2, dim.Y - border*2 - handleHeight), new Color(56, 58, 60));   //inner
            Globals.primitives.DrawTxt(title, new Vector2(pos.X - dim.X / 2 + 8, pos.Y - dim.Y / 2 + 2), new Vector2(0.6f, 0.6f), new Color(100, 100, 100));   //txt
        }
    }
}
