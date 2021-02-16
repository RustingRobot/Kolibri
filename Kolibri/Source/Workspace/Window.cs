using Kolibri.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kolibri.Source.Workspace
{
    public class Window : ESprite2d //handles stuff like dragging and resizing of windows
    {
        public bool delete;
        public Vector2 minDim;
        private float handleHeight = 20, border = 3, initXPos, initXWidth;
        private bool[] dragged = new bool[4];    //0 = Handle; 1 = Bottom; 2 = Right; 3 = Left
        private string title;
        private Vector2 clickOffset;
        private Color defaultBorder, selectedBorder;
        private Color[] borderColors;
        public Window(Vector2 POS, Vector2 DIM, string TITLE) : base("Square", POS, DIM)
        { 
            title = TITLE;
            minDim = new Vector2((float)(Globals.font.MeasureString(title).X * Globals.fontSize.X + 16), border + handleHeight);
            defaultBorder = new Color(39, 44, 48);
            selectedBorder = new Color(60, 104, 148);
            borderColors = new Color[4] { defaultBorder, defaultBorder, defaultBorder, defaultBorder };
        }

        public override void Update(Vector2 OFFSET)
        {
            //Handle movement
            if(dragged[0] || Globals.GetBoxOverlap(new Vector2(pos.X, pos.Y), new Vector2(dim.X, handleHeight), new Vector2(Globals.mouse.newMouse.X, Globals.mouse.newMouse.Y), Vector2.Zero))
            {
                if(!Globals.dragging) borderColors[0] = selectedBorder;
                if (dragged[0] || (!Globals.dragging && Globals.mouse.LeftClickHold()))
                {
                    Globals.dragging = true;
                    ObjManager.toFront(this);
                    dragged[0] = true;
                    clickOffset = Globals.mouse.oldMousePos - pos;
                    pos = Globals.mouse.newMousePos - clickOffset;
                }
            }
            else { borderColors[0] = defaultBorder; }
            //bottom border
            if (dragged[1] || Globals.GetBoxOverlap(new Vector2(pos.X, pos.Y + dim.Y - border), new Vector2(dim.X, border), new Vector2(Globals.mouse.newMouse.X, Globals.mouse.newMouse.Y), Vector2.Zero)) 
            {
                if (!Globals.dragging) borderColors[1] = selectedBorder;
                if (dragged[1] || (!Globals.dragging && Globals.mouse.LeftClickHold()))
                {
                    Globals.dragging = true;
                    dragged[1] = true;
                    dim.Y = Globals.mouse.newMousePos.Y - pos.Y;
                    if (dim.Y < minDim.Y) dim.Y = minDim.Y;
                }
            }
            else { borderColors[1] = defaultBorder; }
            //right border
            if (dragged[2] || Globals.GetBoxOverlap(new Vector2(pos.X + dim.X - border, pos.Y), new Vector2(border, dim.Y), new Vector2(Globals.mouse.newMouse.X, Globals.mouse.newMouse.Y), Vector2.Zero)) 
            {
                if (!Globals.dragging) borderColors[2] = selectedBorder;
                if (dragged[2] || (!Globals.dragging && Globals.mouse.LeftClickHold()))
                {
                    Globals.dragging = true;
                    dragged[2] = true;
                    dim.X = Globals.mouse.newMousePos.X - pos.X;
                    if (dim.X < minDim.X) dim.X = minDim.X;
                }
            }
            else { borderColors[2] = defaultBorder; }
            //left border
            if (dragged[3] || Globals.GetBoxOverlap(new Vector2(pos.X, pos.Y), new Vector2(border, dim.Y), new Vector2(Globals.mouse.newMouse.X, Globals.mouse.newMouse.Y), Vector2.Zero)) 
            {
                if (!Globals.dragging)
                {
                    borderColors[3] = selectedBorder;
                    initXWidth = dim.X;
                    initXPos = pos.X;
                }
                if (dragged[3] || (!Globals.dragging && Globals.mouse.LeftClickHold()))
                {
                    Globals.dragging = true;
                    dragged[3] = true;
                    if (dim.X > minDim.X || (Globals.mouse.newMousePos.X - Globals.mouse.oldMousePos.X < 0 && Globals.mouse.oldMousePos.X < pos.X))
                    {
                        clickOffset = (Globals.mouse.oldMousePos.X > pos.X)? Globals.mouse.oldMousePos - pos: Vector2.Zero;
                        pos.X = Globals.mouse.newMousePos.X - clickOffset.X;
                        dim.X = initXWidth - (pos.X - initXPos);
                    }
                    if (dim.X < minDim.X)
                    {
                        dim.X = minDim.X;
                        pos.X = initXPos + (initXWidth - minDim.X);
                        
                    }
                    
                }
            }
            else { borderColors[3] = defaultBorder; }

            if (!Globals.mouse.LeftClickHold() && Globals.dragging)
            {
                Globals.dragging = false;
                for (int i = 0; i < dragged.Length; i++) dragged[i] = false;
            }
            base.Update(OFFSET);
        }

        public void beginWindowContent()
        {
            Globals.spriteBatch.End();
            RasterizerState rs = new RasterizerState();
            rs.ScissorTestEnable = true;
            Globals.spriteBatch.GraphicsDevice.RasterizerState = rs;
            Globals.spriteBatch.GraphicsDevice.ScissorRectangle = new Rectangle((int)(pos.X + border), (int)(pos.Y + handleHeight), (int)(dim.X - border * 2), (int)(dim.Y - border - handleHeight));
            Globals.spriteBatch.Begin(rasterizerState: rs);
        }

        public void endWindowContent()
        {
            Globals.spriteBatch.End();
            //Globals.spriteBatch.GraphicsDevice.RasterizerState.ScissorTestEnable = false;
            Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            //borders
            Globals.primitives.DrawRect(new Vector2(pos.X, pos.Y), new Vector2(border, dim.Y), borderColors[3]);  //left
            Globals.primitives.DrawRect(new Vector2(pos.X + dim.X - border, pos.Y), new Vector2(border, dim.Y), borderColors[2]);  //right
            Globals.primitives.DrawRect(new Vector2(pos.X, pos.Y + dim.Y - border), new Vector2(dim.X, border), borderColors[1]);  //bottom
            Globals.primitives.DrawRect(new Vector2(pos.X, pos.Y), new Vector2(dim.X, handleHeight), borderColors[0]);    //handle
            //title
            Globals.primitives.DrawTxt(title, new Vector2(pos.X + 8, pos.Y + 2), Globals.fontSize, new Color(200, 200, 200));
        }
    }
}
