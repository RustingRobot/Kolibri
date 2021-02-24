﻿using Kolibri.Engine;
using Kolibri.Source.Workspace.UIElements;
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
        public int? LeftCI, RightCI, TopCI, BottomCI; // CI = Constraint Index
        public bool docked = false;

        float handleHeight = 20, border = 3, initXPos, initXWidth;
        bool[] dragged = new bool[4];    //0 = Handle; 1 = Bottom; 2 = Right; 3 = Left
        string title;
        Vector2 clickOffset;
        Color defaultBorder, selectedBorder;
        Color[] borderColors;
        Button deleteButton;


        public Window(Vector2 POS, Vector2 DIM, string TITLE) : base("Square", POS, DIM)
        { 
            title = TITLE;
            minDim = new Vector2((float)(Globals.font.MeasureString(title).X * Globals.fontSize.X + 16 + 15), border + handleHeight);
            defaultBorder = new Color(39, 44, 48);
            selectedBorder = new Color(60, 104, 148);
            borderColors = new Color[4] { defaultBorder, defaultBorder, defaultBorder, defaultBorder };
            deleteButton = new Button(closeWindow, this, new Vector2(dim.X - 17.5f, 2.5f), new Vector2(15, 15), "x");
            deleteButton.normColor = Color.Transparent;
            deleteButton.hoverColor = Color.Transparent;
        }

        public override void Update(Vector2 OFFSET)
        {
            //Handle movement
            if(dragged[0] || Globals.GetBoxOverlap(new Vector2(pos.X, pos.Y), new Vector2(dim.X, handleHeight), new Vector2(Globals.mouse.newMouse.X, Globals.mouse.newMouse.Y), Vector2.Zero))
            {
                if(Globals.interactWindow == null) borderColors[0] = selectedBorder;
                if (TopCI == null && dragged[0] || (Globals.interactWindow == null && Globals.mouse.LeftClickHold()))
                {
                    Globals.interactWindow = this;
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
                if (Globals.interactWindow == null) borderColors[1] = selectedBorder;
                if (BottomCI == null && dragged[1] || (Globals.interactWindow == null && Globals.mouse.LeftClickHold()))
                {
                    Globals.interactWindow = this;
                    dragged[1] = true;
                    dim.Y = Globals.mouse.newMousePos.Y - pos.Y;
                    if (dim.Y < minDim.Y) dim.Y = minDim.Y;
                }
            }
            else { borderColors[1] = defaultBorder; }
            //right border
            if (dragged[2] || Globals.GetBoxOverlap(new Vector2(pos.X + dim.X - border, pos.Y), new Vector2(border, dim.Y), new Vector2(Globals.mouse.newMouse.X, Globals.mouse.newMouse.Y), Vector2.Zero)) 
            {
                if (Globals.interactWindow == null) borderColors[2] = selectedBorder;
                if (RightCI == null && dragged[2] || (Globals.interactWindow == null && Globals.mouse.LeftClickHold()))
                {
                    Globals.interactWindow = this;
                    dragged[2] = true;
                    dim.X = Globals.mouse.newMousePos.X - pos.X;
                    if (dim.X < minDim.X) dim.X = minDim.X;
                }
            }
            else { borderColors[2] = defaultBorder; }
            //left border
            if (dragged[3] || Globals.GetBoxOverlap(new Vector2(pos.X, pos.Y), new Vector2(border, dim.Y), new Vector2(Globals.mouse.newMouse.X, Globals.mouse.newMouse.Y), Vector2.Zero)) 
            {
                if (Globals.interactWindow == null)
                {
                    borderColors[3] = selectedBorder;
                    initXWidth = dim.X;
                    initXPos = pos.X;
                }
                if (LeftCI == null && dragged[3] || (Globals.interactWindow == null && Globals.mouse.LeftClickHold()))
                {
                    Globals.interactWindow = this;
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

            if (docked)
            {
                if (LeftCI != null) pos.X = DockSpace.XConstraintValues[(int)LeftCI];
                if (RightCI != null) dim.X = DockSpace.XConstraintValues[(int)RightCI];
                if (TopCI != null) pos.Y = DockSpace.YConstraintValues[(int)TopCI];
                if (BottomCI != null) dim.Y = DockSpace.YConstraintValues[(int)BottomCI];
            }

            if (!Globals.mouse.LeftClickHold() && Globals.interactWindow == this)
            {
                DockSpace.applyDocking();
                Globals.interactWindow = null;
                for (int i = 0; i < dragged.Length; i++) dragged[i] = false;
            }
            deleteButton.relativePos.X = dim.X - 17.5f;
            deleteButton.Update(OFFSET);
            base.Update(OFFSET);
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
            deleteButton.Draw(OFFSET);
        }

        public void beginWindowContent()    //needed for a new sprite batch that's masking what's inside
        {
            Globals.spriteBatch.End();
            RasterizerState rs = new RasterizerState();
            rs.ScissorTestEnable = true;
            Globals.spriteBatch.GraphicsDevice.RasterizerState = rs;
            Globals.spriteBatch.GraphicsDevice.ScissorRectangle = new Rectangle((int)(pos.X + border), (int)(pos.Y + handleHeight), (int)(dim.X - border * 2), (int)(dim.Y - border - handleHeight));
            Globals.spriteBatch.Begin(rasterizerState: rs);
        }

        public void endWindowContent()  //switch back to normal sprite batch
        {
            Globals.spriteBatch.End();
            Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
        }

        public void closeWindow()
        {
            delete = true;
        }
    }
}
