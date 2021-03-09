using System;
using Kolibri.Engine;
using Microsoft.Xna.Framework;
using Kolibri.Source.Workspace;



namespace Kolibri.Source.Workspace.UIElements
{
    class ColorPicker : Window
    {
        private Button r, g, b;
        public Color currentColor;
     
        public ColorPicker(Vector2 POS, Vector2 DIM) : base(POS, DIM, "ColorPicker")
        {
            r = new Button(pickColorRed, this, new Vector2(10, 25), new Vector2(20,20),"");
            r.normColor = new Color(255, 0, 0);
            r.hoverColor = new Color(156,20,20);
            r.clickColor = new Color(181, 33, 33);
            g = new Button(pickColorGreen, this, new Vector2(40, 25), new Vector2(20, 20), "");
            g.normColor = new Color(15, 105, 15);
            g.hoverColor = new Color(11, 71, 11);
            g.clickColor = new Color(15, 94, 15);
            b = new Button(pickColorBlue, this, new Vector2(70, 25), new Vector2(20, 20), "");
            b.normColor = new Color(0, 0, 255);
            b.hoverColor = new Color(9, 9, 179);
            b.clickColor = new Color(12, 12, 207);
        }
        public void pickColorRed()
        {
            currentColor = new Color(255, 0, 0); //red
        }
        public void pickColorGreen()
        {
            currentColor = new Color(15, 105, 15); //green
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
