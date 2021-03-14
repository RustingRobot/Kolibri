using System;
using Kolibri.Engine;
using Microsoft.Xna.Framework;
using Kolibri.Source.Workspace;



namespace Kolibri.Source.Workspace.UIElements
{
    class ColorPickerWindow : Window
    {
        private Button r, g, b, rgb;
        public Color currentColor;
        Slider[] slider = new Slider[3];
        private Label colorLabel;
        Textfield[] fields = new Textfield[3];
        private int redValue, blueValue, greenValue;
 
        public ColorPickerWindow(Vector2 POS, Vector2 DIM) : base(POS, DIM, "ColorPicker")
        {
            //button for sliders
            rgb = new Button(PickColorRGB, this, new Vector2(10, 155), new Vector2(100, 100), "");
            rgb.normColor = new Color(0, 0, 0);
            //slider red
            slider[0] = new Slider(0,255,1,this,new Vector2(10,40),new Vector2(255,5),""); 
            slider[0].color = new Color(255, 0, 0);
            //slider green
            slider[1] = new Slider(0, 255, 1, this, new Vector2(10, 80), new Vector2(255, 5), ""); 
            slider[1].color = new Color(15, 105, 15);
            //slider blue
            slider[2] = new Slider(0, 255, 1, this, new Vector2(10, 120), new Vector2(255, 5), ""); 
            slider[2].color = new Color(0, 0, 255);
            //lable
            colorLabel = new Label(this, new Vector2(32, 252), new Vector2(90, 25), "Color:" + slider[0].getValue() + "," + slider[1].getValue() + "," + slider[2].getValue());
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
            slider[0].SetValue(255);
            slider[1].SetValue(0);
            slider[2].SetValue(0);
        }
        public void PickColorGreen()
        {
            currentColor = new Color(15, 105, 15); //green
            slider[0].SetValue(15);
            slider[1].SetValue(105);
            slider[2].SetValue(15);
        }
        public void PickColorBlue()
        {
            currentColor = new Color(0, 0, 255); //blue
            slider[0].SetValue(0);
            slider[1].SetValue(0);
            slider[2].SetValue(255);
        }
        public void PickColorRGB()
        {
            currentColor = new Color(slider[0].getValue(), slider[1].getValue(), slider[2].getValue()); //Chosen color with sliders
            rgb.normColor = currentColor;
        }
        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
            rgb.Update(OFFSET);
            slider[0].Update(OFFSET);
            slider[1].Update(OFFSET);
            slider[2].Update(OFFSET);
            rgb.Update(OFFSET);
            colorLabel.Update(OFFSET);
            colorLabel.label = "Color: " + redValue + "," + greenValue + "," + blueValue;
            fields[0].Update(OFFSET); fields[1].Update(OFFSET); fields[2].Update(OFFSET);
            currentColor = new Color(slider[0].getValue(), slider[1].getValue(), slider[2].getValue()); //Chosen color with sliders
            rgb.normColor = currentColor;
            if (fields[0].content != "") redValue = int.Parse(fields[0].content);
            if (fields[1].content != "") greenValue = int.Parse(fields[1].content);
            if (fields[2].content != "") blueValue = int.Parse(fields[2].content);
            slider[0].posMarker.X = 510 + redValue;
            slider[1].posMarker.X = 510 + greenValue;
            slider[2].posMarker.X = 510 + blueValue;
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            beginWindowContent();
            rgb.Draw(OFFSET);
            colorLabel.Draw(OFFSET);
            slider[0].Draw(OFFSET);
            slider[1].Draw(OFFSET);
            slider[2].Draw(OFFSET);
            fields[0].Draw(OFFSET); fields[1].Draw(OFFSET); fields[2].Draw(OFFSET);
            endWindowContent();
        }
    }
}
