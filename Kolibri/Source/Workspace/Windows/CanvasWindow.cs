using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kolibri.Source.Workspace.Windows
{
    class CanvasWindow : Window
    {
        Canvas canvas;

        public CanvasWindow(Vector2 POS, Vector2 DIM) : base(POS, DIM, "Canvas")
        {
            canvas = new Canvas(this, new Vector2(10, 10), new Vector2(200, 200));
        }

        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
            canvas.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
            canvas.Draw(OFFSET);
        }
    }
}
