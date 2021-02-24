using Kolibri.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
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
            switch (type)
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
                switch (type)
                {
                    case "top":

                        break;
                    case "left":
                        Globals.interactWindow.LeftCI = 0;
                        Globals.interactWindow.TopCI = 0;
                        Globals.interactWindow.BottomCI = 1;
                        Globals.interactWindow.docked = true;
                        break;
                    case "right":

                        break;
                    case "bottom":

                        break;
                }
            }
        }

        public override void Update(Vector2 OFFSET)
        {
            if (Globals.interactWindow != null && !Globals.interactWindow.docked)
            {
                if (Globals.GetBoxOverlap(rotPos, rotDim, Globals.mouse.newMousePos, Vector2.Zero))
                {
                    color = selectedColor;
                }
                else
                {
                    color = normColor;
                }
                switch (type)
                {
                    case "top":
                        rotDim = -dim - new Vector2(10);
                        rotPos = pos + new Vector2(5);
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
            if (Globals.interactWindow != null && !Globals.interactWindow.docked)
            {
                Globals.primitives.DrawRect(rotPos, rotDim, color);
                base.Draw(OFFSET);
            }
        }
    }
}
