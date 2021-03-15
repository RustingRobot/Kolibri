
using Kolibri.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace Kolibri.Source.Workspace.UIElements
{
    class ColorPickerWindow : Window
    {
        private Button rgb, hsv;
        public Color currentColor;
        Slider[] slider = new Slider[3];
        Label[] sliderDesc = new Label[3];
        Textfield[] fields = new Textfield[3];
        private int redValue, greenValue, blueValue;
        private bool rgbMode = true;
 
        public ColorPickerWindow(Vector2 POS, Vector2 DIM) : base(POS, DIM, "ColorPicker")
        {
            //button
            rgb = new Button(changeMode, "rgb", this, new Vector2(60, 130), new Vector2(150, 20), "rgb mode");
            rgb.txtColor = new Color(200, 200, 200);
            hsv = new Button(changeMode, "hsv", this, new Vector2(215, 130), new Vector2(150, 20), "hsv mode");
            hsv.txtColor = new Color(200, 200, 200);
            //slider red
            slider[0] = new Slider(0,255,1,this,new Vector2(60,40),new Vector2(255,15),""); 
            //slider green
            slider[1] = new Slider(0, 255, 1, this, new Vector2(60, 70), new Vector2(255, 15), ""); 
            //slider blue
            slider[2] = new Slider(0, 255, 1, this, new Vector2(60, 100), new Vector2(255, 15), ""); 
            //textfields
            fields[0] = new Textfield(this,new Vector2(325,38),new Vector2(40,20),"") { defaultContent = "0" };
            fields[1] = new Textfield(this, new Vector2(325, 68), new Vector2(40, 20), "") { defaultContent = "0" };
            fields[2] = new Textfield(this, new Vector2(325, 98), new Vector2(40, 20), "") { defaultContent = "0" };
            fields[0].numberField = true;
            fields[1].numberField = true;
            fields[2].numberField = true;
            //slider descriptions
            sliderDesc[0] = new Label(this, new Vector2(50, 48), Globals.fontSize, "R");
            sliderDesc[1] = new Label(this, new Vector2(50, 78), Globals.fontSize, "G");
            sliderDesc[2] = new Label(this, new Vector2(50, 108), Globals.fontSize, "B");
        }
        
        void changeMode(string mode)
        {
            if(mode == "rgb")
            {
                sliderDesc[0].label = "R";
                sliderDesc[1].label = "G";
                sliderDesc[2].label = "B";

                slider[0].end = 255;
                slider[1].end = 255;
                slider[2].end = 255;

                rgbMode = true;

                setColor(currentColor);
            }
            else
            {
                sliderDesc[0].label = "H";
                sliderDesc[1].label = "S";
                sliderDesc[2].label = "V";

                slider[0].end = 360;
                slider[1].end = 100;
                slider[2].end = 100;

                rgbMode = false;

                float[] colorValues = RGBtoHSV((float)currentColor.R, (float)currentColor.G, (float)currentColor.B);
                slider[0].value = (int)colorValues[0];
                slider[1].value = (int)(colorValues[1] * 100);
                slider[2].value = (int)(colorValues[2] * 100);
            }
        }

        public void setColor(Color color)
        {
            if (rgbMode)
            {
                slider[0].value = color.R;
                slider[1].value = color.G;
                slider[2].value = color.B;
            }
            else
            {
                float[] colorValues = RGBtoHSV((float)color.R, (float)color.G, (float)color.B);
                slider[0].value = (int)colorValues[0];
                slider[1].value = (int)(colorValues[1] * 100);
                slider[2].value = (int)(colorValues[2] * 100);
            }
        }
        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
            slider[0].Update(OFFSET); slider[1].Update(OFFSET); slider[2].Update(OFFSET);
            fields[0].Update(OFFSET); fields[1].Update(OFFSET); fields[2].Update(OFFSET);
            sliderDesc[0].Update(OFFSET); sliderDesc[1].Update(OFFSET); sliderDesc[2].Update(OFFSET);

            if (rgbMode)
                currentColor = new Color(slider[0].getValue(), slider[1].getValue(), slider[2].getValue()); //Chosen color with sliders
            else
                currentColor = HSVtoRGB(slider[0].getValue(), slider[1].getValue() / 100.0, slider[2].getValue() / 100.0);
            if (fields[0].selected || fields[1].selected || fields[2].selected) setColor(new Color(int.Parse(fields[0].content), int.Parse(fields[1].content), int.Parse(fields[2].content)));
            if (!fields[0].selected) fields[0].content = Convert.ToString(slider[0].getValue());
            if (!fields[1].selected) fields[1].content = Convert.ToString(slider[1].getValue());
            if (!fields[2].selected) fields[2].content = Convert.ToString(slider[2].getValue());

            rgb.Update(OFFSET);
            hsv.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            beginWindowContent();
            slider[0].Draw(OFFSET); slider[1].Draw(OFFSET); slider[2].Draw(OFFSET);
            fields[0].Draw(OFFSET); fields[1].Draw(OFFSET); fields[2].Draw(OFFSET);
            sliderDesc[0].Draw(OFFSET); sliderDesc[1].Draw(OFFSET); sliderDesc[2].Draw(OFFSET);
            Globals.primitives.DrawRect(new Vector2(10,40) + pos, new Vector2(30, 75), currentColor);
            rgb.Draw(OFFSET);
            hsv.Draw(OFFSET);
            endWindowContent();
        }

        public Color HSVtoRGB(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);
            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));
            if (hi == 0)
                return new Color(v, t, p);
            else if (hi == 1)
                return new Color(q, v, p);
            else if (hi == 2)
                return new Color(p, v, t);
            else if (hi == 3)
                return new Color(p, q, v);
            else if (hi == 4)
                return new Color(t, p, v);
            else
                return new Color(v, p, q);
        }

        private float[] RGBtoHSV(float r, float g, float b)
        {
            r /= 255;
            g /= 255;
            b /= 255;

            float Cmax = new float[] { r, g, b }.Max();
            float Cmin = new float[] { r, g, b }.Min();
            float delta = Cmax - Cmin;
            float h;

            if (delta == 0)
            {
                h = 0;
            }
            else if (Cmax == r)
            {
                h = 60 * ((g - b) / delta % 6);
            }
            else if (Cmax == g)
            {
                h = 60 * ((b - r) / delta + 2);
            }
            else
            {
                h = 60 * ((r - g) / delta + 4);
            }

            float s = (Cmax == 0) ? 0 : delta / Cmax;
            float v = Cmax;

            return new float[] { h, s, v };
        }
    }
}

