using Kolibri.Engine;
using Kolibri.Source.Workspace.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Kolibri.Source.Workspace.Windows
{
    class CanvasWindow : Window
    {

        public CanvasWindow(Vector2 POS, Vector2 DIM) : base(POS, DIM, "Canvas")
        {
            
        }

        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
            if (Globals.GetBoxOverlap(pos, dim, Globals.mouse.newMousePos, Vector2.Zero))
            {
                if (Globals.keyboard.GetPress("LeftShift"))
                    Globals.canvas.offset.X += (Globals.mouse.newMouse.ScrollWheelValue - Globals.mouse.oldMouse.ScrollWheelValue) * 0.2f;
                else if (Globals.keyboard.GetPress("LeftControl") && Globals.mouse.newMouse.ScrollWheelValue - Globals.mouse.oldMouse.ScrollWheelValue != 0)
                {
                    if (Globals.mouse.newMouse.ScrollWheelValue - Globals.mouse.oldMouse.ScrollWheelValue > 0) {
                        Globals.canvas.zoom += 0.1f;
                        Globals.canvas.offset += (Globals.mouse.newMousePos - Globals.canvas.pos - Globals.canvas.offset) - (Globals.mouse.newMousePos - (Globals.canvas.pos + Globals.canvas.offset));
                    }
                    else
                    {
                        Globals.canvas.zoom -= 0.1f;
                        Globals.canvas.offset += (Globals.mouse.newMousePos - Globals.canvas.pos - Globals.canvas.offset);
                    }
                }
                else
                    Globals.canvas.offset.Y += (Globals.mouse.newMouse.ScrollWheelValue - Globals.mouse.oldMouse.ScrollWheelValue) * 0.2f;
            }
            Globals.canvas.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            beginWindowContent();
            Globals.canvas.Draw(OFFSET);
            endWindowContent();
        }
    }
}
