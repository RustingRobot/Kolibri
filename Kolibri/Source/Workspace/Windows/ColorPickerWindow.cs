
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
        Slider AlphaSlider;
        Label[] sliderDesc = new Label[4];
        Textfield[] fields = new Textfield[4];
        private bool rgbMode = true;
 
        public ColorPickerWindow(Vector2 POS, Vector2 DIM) : base(POS, DIM, "ColorPicker")
        {
            //buttons 
            rgb = new Button(changeMode, "rgb", this, new Vector2(60, 160), new Vector2(150, 20), "rgb mode");
            rgb.txtColor = new Color(200, 200, 200);
            hsv = new Button(changeMode, "hsv", this, new Vector2(215, 160), new Vector2(150, 20), "hsv mode");
            hsv.txtColor = new Color(200, 200, 200);
            //slider red
            slider[0] = new Slider(0,255,1,this,new Vector2(60,40),new Vector2(255,15),""); 
            //slider green
            slider[1] = new Slider(0, 255, 1, this, new Vector2(60, 70), new Vector2(255, 15), ""); 
            //slider blue
            slider[2] = new Slider(0, 255, 1, this, new Vector2(60, 100), new Vector2(255, 15), "");
            //slider alpha
            AlphaSlider = new Slider(0, 255, 1, this, new Vector2(60, 130), new Vector2(255, 15), "") { value = 255 };
            //textfields
            fields[0] = new Textfield(this,new Vector2(325,38),new Vector2(40,20),"") { defaultContent = "0", numberField = true };
            fields[1] = new Textfield(this, new Vector2(325, 68), new Vector2(40, 20), "") { defaultContent = "0", numberField = true };
            fields[2] = new Textfield(this, new Vector2(325, 98), new Vector2(40, 20), "") { defaultContent = "0", numberField = true };
            fields[3] = new Textfield(this, new Vector2(325, 128), new Vector2(40, 20), "") { defaultContent = "0", numberField = true };
            //slider descriptions
            sliderDesc[0] = new Label(this, new Vector2(50, 48), Globals.fontSize, "R");
            sliderDesc[1] = new Label(this, new Vector2(50, 78), Globals.fontSize, "G");
            sliderDesc[2] = new Label(this, new Vector2(50, 108), Globals.fontSize, "B");
            sliderDesc[3] = new Label(this, new Vector2(50, 138), Globals.fontSize, "A");
        }
        
        void changeMode(string mode)
        {
            //RBG mode
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
             //HSV mode
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
                if (slider[0].value < 0) slider[0].value += 360;
            }
        }

        public void setColor(Color color)
        {
            if (rgbMode)
            {
                slider[0].value = color.R;
                slider[1].value = color.G;
                slider[2].value = color.B;
                AlphaSlider.value = color.A;
            }
            else
            {
                float[] colorValues = RGBtoHSV((float)color.R, (float)color.G, (float)color.B);
                slider[0].value = (int)colorValues[0];
                slider[1].value = (int)(colorValues[1] * 100);
                slider[2].value = (int)(colorValues[2] * 100);
                AlphaSlider.value = color.A;

                if (slider[0].value < 0) slider[0].value += 360;
            }
        }
        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
            slider[0].Update(OFFSET); slider[1].Update(OFFSET); slider[2].Update(OFFSET);
            fields[0].Update(OFFSET); fields[1].Update(OFFSET); fields[2].Update(OFFSET); fields[3].Update(OFFSET);
            sliderDesc[0].Update(OFFSET); sliderDesc[1].Update(OFFSET); sliderDesc[2].Update(OFFSET); sliderDesc[3].Update(OFFSET);
            AlphaSlider.Update(OFFSET);
            if (rgbMode)                //chosen color with sliders for RGB mode
                currentColor = new Color(slider[0].getValue(), slider[1].getValue(), slider[2].getValue(), AlphaSlider.getValue());
            else                //chosen color with sliders for HSV mode
                currentColor = new Color(HSVtoRGB(slider[0].getValue(), slider[1].getValue() / 100.0, slider[2].getValue() / 100.0), AlphaSlider.getValue());
            //setting color through textfields
            if (fields[0].selected || fields[1].selected || fields[2].selected || fields[3].selected)
            {
                if (rgbMode)
                {
                    setColor(new Color(int.Parse(fields[0].content), int.Parse(fields[1].content), int.Parse(fields[2].content), int.Parse(fields[3].content)));
                }
                else
                {
                    slider[0].value = int.Parse(fields[0].content);
                    slider[1].value = int.Parse(fields[1].content);
                    slider[2].value = int.Parse(fields[2].content);
                    if (slider[0].value > slider[0].end) slider[0].value = slider[0].end;
                    if (slider[1].value > slider[1].end) slider[1].value = slider[1].end;
                    if (slider[2].value > slider[2].end) slider[2].value = slider[2].end;

                    if (slider[0].value < 0) slider[0].value += 360;
                }
            }
            //setting color through sliders
            if (!fields[0].selected) fields[0].content = Convert.ToString(slider[0].getValue());
            if (!fields[1].selected) fields[1].content = Convert.ToString(slider[1].getValue());
            if (!fields[2].selected) fields[2].content = Convert.ToString(slider[2].getValue());
            if (!fields[3].selected) fields[3].content = Convert.ToString(AlphaSlider.getValue());

            rgb.Update(OFFSET);
            hsv.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            beginWindowContent();
            slider[0].Draw(OFFSET); slider[1].Draw(OFFSET); slider[2].Draw(OFFSET);
            fields[0].Draw(OFFSET); fields[1].Draw(OFFSET); fields[2].Draw(OFFSET); fields[3].Draw(OFFSET);
            sliderDesc[0].Draw(OFFSET); sliderDesc[1].Draw(OFFSET); sliderDesc[2].Draw(OFFSET); ; sliderDesc[3].Draw(OFFSET);
            AlphaSlider.Draw(OFFSET);
            //currentColor rectangle
            Globals.primitives.DrawRect(new Vector2(10,40) + pos, new Vector2(30, 105), currentColor);
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

        public Color packageColor(float[] color)
        {
            return new Color(color[0], color[1], color[2]);
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

