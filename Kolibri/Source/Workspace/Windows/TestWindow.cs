﻿using Kolibri.Engine;
using Kolibri.Source.Workspace.UIElements;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kolibri.Source.Workspace.Windows
{
    class TestWindow : Window
    {
        private Button testBtn;
        private Textfield testField;
        private int clicks = 0;

        private Slider testslider;
        public TestWindow(Vector2 POS, Vector2 DIM) : base(POS, DIM, "Test Window")
        {
            testBtn = new Button(click, this, new Vector2(10, 25), new Vector2(90, 25), "click me!");
            testField = new Textfield(this, new Vector2(10, 55), new Vector2(220, 25), "click to edit text");
            //testslider = new Slider(0, 100, 10, this, new Vector2(20, 120), new Vector2(200, 3), "Example");
        }
        public void click()
        {
            clicks++;
            testBtn.label = $"clicks: {clicks}";
        }

        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
            testBtn.Update(OFFSET);
            testField.Update(OFFSET);
            //testslider.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            beginWindowContent();
            testBtn.Draw(OFFSET);
            testField.Draw(OFFSET);
            Globals.primitives.DrawTxt($"x: {Globals.screenWidth}", new Vector2(10 + pos.X, 100 + pos.Y), Globals.fontSize, Color.Gray);
            Globals.primitives.DrawTxt($"y: {Globals.screenHeight}", new Vector2(10 + pos.X, 120 + pos.Y), Globals.fontSize, Color.Gray);
            //testslider.Draw(OFFSET);
            endWindowContent();
        }
    }
}
