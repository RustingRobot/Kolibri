using Kolibri.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kolibri.Source.workspace
{
    public class Window : ESprite2d
    {
        public bool delete;
        public bool[] dragged = new bool[4];    //0 = Handle; 1 = Bottom; 2 = Right; 3 = Left
        public float handleHeight = 24, border = 4;
        private string title;
        private Vector2 clickOffset;
        public Window(Vector2 POS, Vector2 DIM, string TITLE) : base("Square", POS, DIM)
        {
            title = TITLE;
            
        }

        public override void Update(Vector2 OFFSET)
        {
            if (dragged[0] || (!Globals.dragging && Globals.mouse.LeftClickHold() && Globals.GetBoxOverlap(new Vector2(pos.X - dim.X / 2, pos.Y - dim.Y / 2), new Vector2(dim.X, handleHeight), new Vector2(Globals.mouse.newMouse.X, Globals.mouse.newMouse.Y), Vector2.Zero)))
            {
                Globals.dragging = true;
                dragged[0] = true;
                clickOffset = Globals.mouse.oldMousePos - pos;
                pos = Globals.mouse.newMousePos - clickOffset;
            }
            if (dragged[1] || (!Globals.dragging && Globals.mouse.LeftClickHold() && Globals.GetBoxOverlap(new Vector2(pos.X - dim.X / 2, pos.Y + dim.Y / 2 - border), new Vector2(dim.X, border), new Vector2(Globals.mouse.newMouse.X, Globals.mouse.newMouse.Y), Vector2.Zero)))
            {
                Globals.dragging = true;
                dragged[1] = true;
                dim.Y += Globals.mouse.newMousePos.Y - Globals.mouse.oldMousePos.Y;
                pos.Y += (Globals.mouse.newMousePos.Y - Globals.mouse.oldMousePos.Y) / 2;
            }
            if (dragged[2] || (!Globals.dragging && Globals.mouse.LeftClickHold() && Globals.GetBoxOverlap(new Vector2(pos.X + dim.X / 2 - border, pos.Y - dim.Y / 2), new Vector2(border, dim.Y), new Vector2(Globals.mouse.newMouse.X, Globals.mouse.newMouse.Y), Vector2.Zero)))
            {
                Globals.dragging = true;
                dragged[2] = true;
                dim.X += Globals.mouse.newMousePos.X - Globals.mouse.oldMousePos.X;
                pos.X += (Globals.mouse.newMousePos.X - Globals.mouse.oldMousePos.X) / 2;
            }
            if (dragged[3] || (!Globals.dragging && Globals.mouse.LeftClickHold() && Globals.GetBoxOverlap(new Vector2(pos.X - dim.X / 2, pos.Y - dim.Y / 2), new Vector2(border, dim.Y), new Vector2(Globals.mouse.newMouse.X, Globals.mouse.newMouse.Y), Vector2.Zero)))
            {
                Globals.dragging = true;
                dragged[3] = true;
                dim.X -= Globals.mouse.newMousePos.X - Globals.mouse.oldMousePos.X;
                pos.X += (Globals.mouse.newMousePos.X - Globals.mouse.oldMousePos.X) / 2;
            }
            if (!Globals.mouse.LeftClickHold())
            {
                Globals.dragging = false;
                for (int i = 0; i < dragged.Length; i++) dragged[i] = false;
            }
            base.Update(OFFSET);
        }
        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            //Globals.primitives.DrawRect(new Vector2(pos.X - dim.X / 2, pos.Y - dim.Y / 2), new Vector2(dim.X, handleHeight), new Color(22, 24, 26)); //outer
            //Globals.primitives.DrawRect(new Vector2(pos.X - dim.X / 2 + border, pos.Y - dim.Y / 2 + border + handleHeight), new Vector2(dim.X - border*2, dim.Y - border*2 - handleHeight), new Color(56, 58, 60));   //inner

            Globals.primitives.DrawRect(new Vector2(pos.X - dim.X / 2, pos.Y - dim.Y / 2), new Vector2(dim.X, handleHeight), new Color(39, 44, 48));    //handle
            Globals.primitives.DrawRect(new Vector2(pos.X - dim.X / 2, pos.Y - dim.Y / 2), new Vector2(border, dim.Y), new Color(39, 44, 48));  //left
            Globals.primitives.DrawRect(new Vector2(pos.X - dim.X / 2, pos.Y + dim.Y / 2 - border), new Vector2(dim.X, border), new Color(39, 44, 48));  //bottom
            Globals.primitives.DrawRect(new Vector2(pos.X + dim.X / 2-border, pos.Y - dim.Y / 2), new Vector2(border, dim.Y), new Color(39, 44, 48));  //right

            Globals.primitives.DrawTxt(title, new Vector2(pos.X - dim.X / 2 + 8, pos.Y - dim.Y / 2 + 2), new Vector2(0.6f, 0.6f), new Color(100, 100, 100));   //title
        }
    }
}
