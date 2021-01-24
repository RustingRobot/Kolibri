using Kolibri.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kolibri.Source.Workspace
{
    public class Window : ESprite2d
    {
        public bool delete;
        public Vector2 minDim;
        private float handleHeight = 24, border = 4;
        private bool[] dragged = new bool[4];    //0 = Handle; 1 = Bottom; 2 = Right; 3 = Left
        private string title;
        private Vector2 clickOffset;
        public Window(Vector2 POS, Vector2 DIM, string TITLE) : base("Square", POS, DIM)
        {
            title = TITLE;
            minDim = new Vector2((float)(Globals.font.MeasureString(title).X * 0.6 + 16), border + handleHeight);
        }

        public override void Update(Vector2 OFFSET)
        {
            if (dragged[0] || (!Globals.dragging && Globals.mouse.LeftClickHold() && Globals.GetBoxOverlap(new Vector2(pos.X, pos.Y), new Vector2(dim.X, handleHeight), new Vector2(Globals.mouse.newMouse.X, Globals.mouse.newMouse.Y), Vector2.Zero)))
            {
                Globals.dragging = true;
                dragged[0] = true;
                clickOffset = Globals.mouse.oldMousePos - pos;
                pos = Globals.mouse.newMousePos - clickOffset;
            }
            if (dragged[1] || (!Globals.dragging && Globals.mouse.LeftClickHold() && Globals.GetBoxOverlap(new Vector2(pos.X, pos.Y + dim.Y - border), new Vector2(dim.X, border), new Vector2(Globals.mouse.newMouse.X, Globals.mouse.newMouse.Y), Vector2.Zero)))
            {
                Globals.dragging = true;
                dragged[1] = true;
                dim.Y = Globals.mouse.newMousePos.Y - pos.Y;
                if (dim.Y < minDim.Y) dim.Y = minDim.Y;
            }
            if (dragged[2] || (!Globals.dragging && Globals.mouse.LeftClickHold() && Globals.GetBoxOverlap(new Vector2(pos.X + dim.X - border, pos.Y), new Vector2(border, dim.Y), new Vector2(Globals.mouse.newMouse.X, Globals.mouse.newMouse.Y), Vector2.Zero)))
            {
                Globals.dragging = true;
                dragged[2] = true;
                dim.X = Globals.mouse.newMousePos.X - pos.X;
                if (dim.X < minDim.X) dim.X = minDim.X;
            }
            if (dragged[3] || (!Globals.dragging && Globals.mouse.LeftClickHold() && Globals.GetBoxOverlap(new Vector2(pos.X, pos.Y), new Vector2(border, dim.Y), new Vector2(Globals.mouse.newMouse.X, Globals.mouse.newMouse.Y), Vector2.Zero)))
            {
                Globals.dragging = true;
                dragged[3] = true;
                clickOffset = Globals.mouse.oldMousePos - pos;
                if (dim.X > minDim.X || (Globals.mouse.newMousePos.X - Globals.mouse.oldMousePos.X < 0 && Globals.mouse.newMousePos.X < pos.X))
                {
                    dim.X -= Globals.mouse.newMousePos.X - Globals.mouse.oldMousePos.X;
                    pos.X = Globals.mouse.newMousePos.X - clickOffset.X;
                }
                else
                {
                    dim.X = minDim.X;
                    pos.X = Globals.mouse.oldMousePos.X - clickOffset.X;
                }
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
            //borders
            Globals.primitives.DrawRect(new Vector2(pos.X, pos.Y), new Vector2(dim.X, handleHeight), new Color(39, 44, 48));    //handle
            Globals.primitives.DrawRect(new Vector2(pos.X, pos.Y), new Vector2(border, dim.Y), new Color(39, 44, 48));  //left
            Globals.primitives.DrawRect(new Vector2(pos.X, pos.Y + dim.Y - border), new Vector2(dim.X, border), new Color(39, 44, 48));  //bottom
            Globals.primitives.DrawRect(new Vector2(pos.X + dim.X - border, pos.Y), new Vector2(border, dim.Y), new Color(39, 44, 48));  //right
            //title
            Globals.primitives.DrawTxt(title, new Vector2(pos.X + 8, pos.Y + 2), new Vector2(0.6f, 0.6f), new Color(100, 100, 100));
        }
    }
}
