using Kolibri.Engine;
using Kolibri.Source.Workspace.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Kolibri.Source.Workspace.Windows
{
    class ToolsWindow : Window
    {
        string[] tools = new string[3] { "Brush", "Eraser", "BucketFill" };
        List<Texture2D> buttonTextures = new List<Texture2D>();
        List<Button> toolBtns = new List<Button>();
        List<List<UIElement>> toolOptions = new List<List<UIElement>>();
        public ToolsWindow(Vector2 POS, Vector2 DIM) : base(POS, DIM, "Tools")
        {
            for (int i = 0; i < tools.Length; i++)
            {
                buttonTextures.Add(Globals.content.Load<Texture2D>(tools[i]));
            }

            for (int i = 0; i < tools.Length; i++)
            {
                toolBtns.Add(new Button(ToolClick, tools[i], this, new Vector2(16, 30 + i * 35), new Vector2(30, 30), "") { model = buttonTextures[i], imgSize = new Vector2(0.8f), imgOffset = new Vector2(-8) });
            }

            toolOptions.Add(
            new List<UIElement>()   //Brush
            {
                new Label(this, new Vector2(120, 37), Globals.fontSize, "Brush Size:"),
                new Textfield(this, new Vector2(80, 50), new Vector2(57, 25), "1") {defaultContent = "0", numberField = true, tag = "BrushSize"}
            });
            toolOptions.Add(
            new List<UIElement>()   //Eraser
            {
                new Label(this, new Vector2(120, 37), Globals.fontSize, "Eraser Size:"),
                new Textfield(this, new Vector2(80, 50), new Vector2(57, 25), "1") {defaultContent = "0", numberField = true, tag = "EraserSize"}
            });
            toolOptions.Add(
            new List<UIElement>()   //BucketFill
            {
            });
        }

        public void ToolClick(string tool)
        {
            Globals.activeTool = tool;
            for (int i = 0; i < toolBtns.Count; i++)
            {
                toolBtns[i].normColor = new Color(100, 100, 100);
            }
            toolBtns[Array.IndexOf(tools,tool)].normColor = new Color(60, 104, 148);
        }


        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
            for (int i = 0; i < toolBtns.Count; i++)
            {
                toolBtns[i].Update(OFFSET);
            }
            int toolIndex = Array.IndexOf(tools, Globals.activeTool);
            if (toolIndex < 0) return;
            for (int i = 0; i < toolOptions[toolIndex].Count; i++)
            {
                if(toolOptions[toolIndex][i].GetType() == typeof(Textfield))
                {
                    Textfield textfield = (Textfield)toolOptions[toolIndex][i];
                    switch (textfield.tag)
                    {
                        case "BrushSize":
                            textfield.Update(OFFSET, ref Globals.canvas.BrushSize);
                            break;
                        case "EraserSize":
                            textfield.Update(OFFSET, ref Globals.canvas.EraserSize);
                            break;
                    }
                }
                else
                {
                    toolOptions[toolIndex][i].Update(OFFSET);
                }
            }
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            beginWindowContentAA();
            for (int i = 0; i < toolBtns.Count; i++)
            {
                toolBtns[i].Draw(OFFSET);
            }
            int toolIndex = Array.IndexOf(tools, Globals.activeTool);
            if (toolIndex < 0) { endWindowContent(); return; }
            for (int i = 0; i < toolOptions[toolIndex].Count; i++)
            {
                toolOptions[toolIndex][i].Draw(OFFSET);
            }
            endWindowContent();
        }
    }
}