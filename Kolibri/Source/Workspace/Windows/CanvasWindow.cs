using Kolibri.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kolibri.Source.Workspace.Windows
{
    class CanvasWindow : Window
    {
        public Canvas canvas;

        public CanvasWindow(Vector2 POS, Vector2 DIM) : base(POS, DIM, "Canvas")
        {
            canvas = new Canvas(this, new Vector2(50, 50), new Vector2(400, 400));
        }

        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
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
