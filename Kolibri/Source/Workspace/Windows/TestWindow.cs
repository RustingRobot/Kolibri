using Kolibri.Engine;
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

        public TestWindow(Vector2 POS, Vector2 DIM) : base(POS, DIM, "Test Window")
        {
            testBtn = new Button(click, this, new Vector2(20, 40), new Vector2(90, 30), "click me!");
            testField = new Textfield(this, new Vector2(20, 80), new Vector2(220, 30), "click to edit text");
        }
        public void click()
        {
            if (testBtn.color.R == 200)
            {
                testBtn.color = new Color(100, 100, 100);
            }
            else
            {
                testBtn.color = new Color(200, 100, 100);
            }
        }

        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
            testBtn.Update(OFFSET);
            testField.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            testBtn.Draw(OFFSET);
            testField.Draw(OFFSET);
        }
    }
}
