using Kolibri.Engine;
using Kolibri.Source.Workspace.UIElements;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Kolibri.Source.Workspace.Windows
{
    class ToolsWindow : Window
    {
        string[] tools = new string[3] { "Brush", "Erasor", "BucketFill" };
        List<Button> toolBtns = new List<Button>();
        public ToolsWindow(Vector2 POS, Vector2 DIM) : base(POS, DIM, "Tools")
        {
            for (int i = 0; i < tools.Length; i++)
            {
                toolBtns.Add(new Button(ToolClick, tools[i], this, new Vector2(16, 30 + i * 35), new Vector2(30,30), tools[i]));
            }
        }

        public void ToolClick(string tool)
        {
            Globals.activeTool = tool;
            Debug.WriteLine(tool);
        }

        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
            for (int i = 0; i < toolBtns.Count; i++)
            {
                toolBtns[i].Update(OFFSET);
            }
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            beginWindowContent();
            for (int i = 0; i < toolBtns.Count; i++)
            {
                toolBtns[i].Draw(OFFSET);
            }
            endWindowContent();
        }
    }
}