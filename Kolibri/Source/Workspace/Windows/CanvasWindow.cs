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
        public Canvas canvas;

        public CanvasWindow(Vector2 POS, Vector2 DIM) : base(POS, DIM, "Canvas")
        {
            canvas = new Canvas(this, new Vector2(20, 35), new Vector2(200, 400));
        }

        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
            if (Globals.GetBoxOverlap(pos, dim, Globals.mouse.newMousePos, Vector2.Zero))
            {
                if (Globals.keyboard.GetPress("LeftShift"))
                    canvas.offset.X += (Globals.mouse.newMouse.ScrollWheelValue - Globals.mouse.oldMouse.ScrollWheelValue) * 0.2f;
                else if (Globals.keyboard.GetPress("LeftControl") && Globals.mouse.newMouse.ScrollWheelValue - Globals.mouse.oldMouse.ScrollWheelValue != 0)
                    canvas.zoom += (Globals.mouse.newMouse.ScrollWheelValue - Globals.mouse.oldMouse.ScrollWheelValue > 0) ? 0.1f : -0.1f;
                else
                    canvas.offset.Y += (Globals.mouse.newMouse.ScrollWheelValue - Globals.mouse.oldMouse.ScrollWheelValue) * 0.2f;
            }
            canvas.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            beginWindowContent();
            canvas.Draw(OFFSET);
            endWindowContent();
        }
    }
}
