using System;
using Kolibri.Engine;
using Microsoft.Xna.Framework;
using Kolibri.Source.Workspace;



namespace Kolibri.Source.Workspace.UIElements
{
    class ColorPickerWindow : Window
    {
        private Button r, g, b,rgb;
        public Color currentColor;
        Slider[] colors = new Slider[3];
        private Label testLabel;
        Textfield[] fields = new Textfield[3];
 
        public ColorPickerWindow(Vector2 POS, Vector2 DIM) : base(POS, DIM, "ColorPicker")
        {
            //red button
            r = new Button(PickColorRed, this, new Vector2(10, 25), new Vector2(20,20),"");
            r.normColor = new Color(255, 0, 0);
            r.hoverColor = new Color(156,20,20);
            r.clickColor = new Color(181, 33, 33);
            //green button
            g = new Button(PickColorGreen, this, new Vector2(40, 25), new Vector2(20, 20), "");
            g.normColor = new Color(15, 105, 15);
            g.hoverColor = new Color(11, 71, 11);
            g.clickColor = new Color(15, 94, 15);
            //blue button
            b = new Button(PickColorBlue, this, new Vector2(70, 25), new Vector2(20, 20), "");
            b.normColor = new Color(0, 0, 255);
            b.hoverColor = new Color(9, 9, 179);
            b.clickColor = new Color(12, 12, 207);
            //button for sliders
            rgb = new Button(PickColorRGB, this, new Vector2(10, 140), new Vector2(100, 100), "");
            rgb.normColor = new Color(0,0,0);
            //slider red
            colors[0] = new Slider(0,255,1,this,new Vector2(10,50),new Vector2(255,5),""); 
            colors[0].color = new Color(255, 0, 0);
            //slider green
            colors[1] = new Slider(0, 255, 1, this, new Vector2(10, 90), new Vector2(255, 5), ""); 
            colors[1].color = new Color(15, 105, 15);
            //slider blue
            colors[2] = new Slider(0, 255, 1, this, new Vector2(10, 130), new Vector2(255, 5), ""); 
            colors[2].color = new Color(0, 0, 255);
            testLabel = new Label(this, new Vector2(30, 245), new Vector2(90, 25), "Color:" + colors[0].getValue() + "," + colors[1].getValue() + "," + colors[2].getValue());
            fields[0] = new Textfield(this,new Vector2(10,280),new Vector2(100,10),"") { defaultContent = "0" };
            fields[1] = new Textfield(this, new Vector2(10, 295), new Vector2(100, 10), "") { defaultContent = "0" };
            fields[2] = new Textfield(this, new Vector2(10, 310), new Vector2(100, 10), "") { defaultContent = "0" };
        }
        
        public void PickColorRed()
        {
            currentColor = new Color(255, 0, 0); //red
            colors[0].SetValue(255);
            colors[1].SetValue(0);
            colors[2].SetValue(0);
        }
        public void PickColorGreen()
        {
            currentColor = new Color(15, 105, 15); //green
            colors[0].SetValue(15);
            colors[1].SetValue(105);
            colors[2].SetValue(15);
        }
        public void PickColorBlue()
        {
            currentColor = new Color(0, 0, 255); //blue
            colors[0].SetValue(0);
            colors[1].SetValue(0);
            colors[2].SetValue(255);
        }
        public void PickColorRGB()
        {
            currentColor = new Color(colors[0].getValue(), colors[1].getValue(), colors[2].getValue()); //Chosen color with sliders
            rgb.normColor = currentColor;
        }
        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
            r.Update(OFFSET);
            g.Update(OFFSET);
            b.Update(OFFSET);
            rgb.Update(OFFSET);
            colors[0].Update(OFFSET);
            colors[1].Update(OFFSET);
            colors[2].Update(OFFSET);
            rgb.Update(OFFSET);
            testLabel.Update(OFFSET);
            fields[0].Update(OFFSET); fields[1].Update(OFFSET); fields[2].Update(OFFSET);

        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            beginWindowContent();
            r.Draw(OFFSET);
            g.Draw(OFFSET);
            b.Draw(OFFSET);
            rgb.Draw(OFFSET);
            testLabel.Draw(OFFSET);
            colors[0].Draw(OFFSET);
            colors[1].Draw(OFFSET);
            colors[2].Draw(OFFSET);
            fields[0].Draw(OFFSET); fields[1].Draw(OFFSET); fields[2].Draw(OFFSET);

            endWindowContent();
        }
    }
}
