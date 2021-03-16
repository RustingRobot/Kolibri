using Kolibri.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Kolibri.Source.Workspace.UIElements
{
    class DockIcon : ESprite2d
    {
        string type;
        private Color color, normColor, selectedColor;
        Vector2 rotDim, rotPos;
        public DockIcon(string TYPE) : base("DockIcon", Vector2.Zero, Vector2.Zero)
        {
            type = TYPE;
            switch (type) // rotate instead of using a new texture
            {
                case "top":
                    rot = (float)Math.PI;
                    break;
                case "left":
                    rot = (float)Math.PI / 2.0f;
                    break;
                case "right":
                    rot = -(float)Math.PI / 2.0f;
                    break;
                default:
                    break;
            }
            dim = new Vector2(model.Width / 60, model.Height / 60);    //35
            normColor = new Color(60, 104, 148); ;
            selectedColor = new Color(120, 164, 228);
            color = normColor;
        }

        public void applyDocking()
        {
            if (Globals.GetBoxOverlap(rotPos, rotDim, Globals.mouse.newMousePos, Vector2.Zero))
            {
                Window[] tempWindowFraming = DockSpace.WindowFraming;
                if (type != "left") Globals.interactWindow.RightCW = tempWindowFraming[2];
                if (type != "right") Globals.interactWindow.LeftCW = tempWindowFraming[3];
                if (type != "bottom") Globals.interactWindow.TopCW = tempWindowFraming[0];
                if (type != "top") Globals.interactWindow.BottomCW = tempWindowFraming[1];
                if (type == "left") DockSpace.WindowFraming[3] = Globals.interactWindow;
                if (type == "right") DockSpace.WindowFraming[2] = Globals.interactWindow;
                if (type == "bottom") DockSpace.WindowFraming[1] = Globals.interactWindow;
                if (type == "top") DockSpace.WindowFraming[0] = Globals.interactWindow;
                Globals.interactWindow.docked = true;
                ObjManager.toBack(Globals.interactWindow);
                Debug.WriteLine("docking");
            }
        }

        public override void Update(Vector2 OFFSET)
        {
            if (Globals.interactWindow != null && !Globals.interactWindow.docked && !Globals.interactWindow.popupWindow)
            {
                color = (Globals.GetBoxOverlap(rotPos, rotDim, Globals.mouse.newMousePos, Vector2.Zero)) ? selectedColor : normColor;
                switch (type)
                {
                    case "top":
                        rotDim = dim + new Vector2(10);
                        rotPos = pos - dim - new Vector2(5);
                        break;
                    case "left":
                        rotDim = new Vector2(dim.Y + 10, dim.X + 10);
                        rotPos = pos + new Vector2(-dim.Y - 5, -5);
                        break;
                    case "right":
                        rotDim = new Vector2(dim.Y + 10, dim.X + 10);
                        rotPos = pos + new Vector2(-5, -dim.X - 5);
                        break;
                    case "bottom":
                        rotDim = dim + new Vector2(10);
                        rotPos = pos - new Vector2(5);
                        break;
                }
            }
            base.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET)
        {
            if (Globals.interactWindow != null && !Globals.interactWindow.docked && !Globals.interactWindow.popupWindow)
            {
                Globals.primitives.DrawRect(rotPos, rotDim, color);
                base.Draw(OFFSET);
            }
        }
    }
}