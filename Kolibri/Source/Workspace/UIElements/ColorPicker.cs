using System;
using Kolibri.Engine;
using Microsoft.Xna.Framework;
using Kolibri.Source.Workspace;


namespace Kolibri.Source.Workspace.UIElements
{
    class ColorPicker : Window
    {
        private Button r, g, b;
        private Color currentColor;

        public ColorPicker()
        {
            r = new Button(pickColorRed, this, new Vector2(0, 0), new Vector2(1,1),"red");
            r.normColor = new Color(255, 0, 0);
            g = new Button(pickColorGreen, this, new Vector2(0, 10), new Vector2(1, 1), "blue");
            g.normColor = new Color(0, 255, 0);
            b = new Button(pickColorBlue, this, new Vector2(0, 20), new Vector2(1, 1), "green");
            b.normColor = new Color(0, 0, 255);
        }
        public void pickColorRed()
        {
            currentColor = new Color(255, 0, 0); //red
        }
        public void pickColorGreen()
        {
            currentColor = new Color(0, 255, 0); //green
        }
        public void pickColorBlue()
        {
            currentColor = new Color(0, 0, 255); //blue
        }
        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
            r.Update(OFFSET);
            g.Update(OFFSET);
            b.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            beginWindowContent();
            r.Draw(OFFSET);
            g.Draw(OFFSET);
            b.Draw(OFFSET);
            endWindowContent();
        }
    }
}
