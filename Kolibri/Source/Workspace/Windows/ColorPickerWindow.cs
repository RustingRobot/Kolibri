using System;
using Kolibri.Engine;
using Microsoft.Xna.Framework;
using Kolibri.Source.Workspace;



namespace Kolibri.Source.Workspace.UIElements
{
    class ColorPickerWindow : Window
    {
        private Button r, g, b, rgb, b1;
        public Color currentColor;
        Slider[] colors = new Slider[3];
        private Label colorLabel;
        Textfield[] fields = new Textfield[3];
        private int red, blue, green;
 
        public ColorPickerWindow(Vector2 POS, Vector2 DIM) : base(POS, DIM, "ColorPicker")
        {
            //button for sliders
            rgb = new Button(PickColorRGB, this, new Vector2(10, 155), new Vector2(100, 100), "");
            rgb.normColor = new Color(0, 0, 0);
            //slider red
            colors[0] = new Slider(0,255,1,this,new Vector2(10,40),new Vector2(255,5),""); 
            colors[0].color = new Color(255, 0, 0);
            //slider green
            colors[1] = new Slider(0, 255, 1, this, new Vector2(10, 80), new Vector2(255, 5), ""); 
            colors[1].color = new Color(15, 105, 15);
            //slider blue
            colors[2] = new Slider(0, 255, 1, this, new Vector2(10, 120), new Vector2(255, 5), ""); 
            colors[2].color = new Color(0, 0, 255);
            //lable
            colorLabel = new Label(this, new Vector2(32, 252), new Vector2(90, 25), "Color:" + colors[0].getValue() + "," + colors[1].getValue() + "," + colors[2].getValue());
            colorLabel.color = new Color(255, 255, 255);
            //textfields
            fields[0] = new Textfield(this,new Vector2(320,40),new Vector2(40,18),"") { defaultContent = "0" };
            fields[1] = new Textfield(this, new Vector2(320, 80), new Vector2(40, 18), "") { defaultContent = "0" };
            fields[2] = new Textfield(this, new Vector2(320, 120), new Vector2(40, 18), "") { defaultContent = "0" };
            fields[0].numberField = true;
            fields[1].numberField = true;
            fields[2].numberField = true;
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
            rgb.Update(OFFSET);
            colors[0].Update(OFFSET);
            colors[1].Update(OFFSET);
            colors[2].Update(OFFSET);
            rgb.Update(OFFSET);
            colorLabel.Update(OFFSET);
            colorLabel.label = "Color: " + red + "," + green + "," + blue;
            fields[0].Update(OFFSET); fields[1].Update(OFFSET); fields[2].Update(OFFSET);
            currentColor = new Color(colors[0].getValue(), colors[1].getValue(), colors[2].getValue()); //Chosen color with sliders
            rgb.normColor = currentColor;
            if (fields[0].content != "") red = int.Parse(fields[0].content);
            if (fields[1].content != "") green = int.Parse(fields[1].content);
            if (fields[2].content != "") blue = int.Parse(fields[2].content);
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            beginWindowContent();
            rgb.Draw(OFFSET);
            colorLabel.Draw(OFFSET);
            colors[0].Draw(OFFSET);
            colors[1].Draw(OFFSET);
            colors[2].Draw(OFFSET);
            fields[0].Draw(OFFSET); fields[1].Draw(OFFSET); fields[2].Draw(OFFSET);

            endWindowContent();
        }
    }
}
