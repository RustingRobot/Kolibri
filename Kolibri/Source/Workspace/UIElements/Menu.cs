using System;
using Kolibri.Engine;
using Kolibri.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Kolibri.Source.Workspace;
using Kolibri.Source.Workspace.UIElements;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;
using Kolibri.Source.Workspace.Windows;

namespace Kolibri.Source.Workspace.UIElements
{
    public class Menu : ESprite2d
    {
        Button[] menuitems = new Button[3];
        List<Button>[] subItems = new List<Button>[4];
        int visibleSubMenu = -1;

        string[] menuItemNames = new string[4] { "", "File", "View", "Help" };
        List<string>[] subItemNames = new List<string>[3]
        {
                new List<string>() {"Save", "New" },
                new List<string>() {"Canvas","Tools", "Timeline", "ColorPicker", "Playback" },
                new List<string>() { "GitHub" }
        };

        public Menu(Vector2 POS, Vector2 DIM) :base("square", POS,DIM)    
        {
            float pointer = 0;
            for (int i = 1; i < menuitems.Length + 1; i++)
            {
                menuitems[i - 1] = new Button(open, menuItemNames[i], null, new Vector2(pointer + (i - 1) * 15, 0), new Vector2(Globals.font.MeasureString(menuItemNames[i]).X * Globals.fontSize.X + 15, 24), menuItemNames[i]) 
                { normColor = Color.Transparent, txtColor = new Color(200, 200, 200), hoverColor = new Color(55, 60, 65) };
                pointer += Globals.font.MeasureString(menuItemNames[i]).X * Globals.fontSize.X;
            }

            for (int i = 0; i < subItemNames.Length; i++)
            {
                subItems[i] = new List<Button>();
                string longestString = subItemNames[i].Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);
                for (int j = 0; j < subItemNames[i].Count; j++)
                {
                    subItems[i].Add(new Button(subItemClick, subItemNames[i][j], null, menuitems[i].pos + new Vector2(0, 24 * (j + 1)), new Vector2(Globals.font.MeasureString(longestString).X * Globals.fontSize.X + 15, 24), subItemNames[i][j])
                    { normColor = new Color(39, 44, 48), txtColor = new Color(200, 200, 200), hoverColor = new Color(55, 60, 65), leftBound = true });
                }
            }
        }
        
        public void open(string type) { visibleSubMenu = (visibleSubMenu == Array.IndexOf(menuItemNames, type) - 1)? -1 : Array.IndexOf(menuItemNames, type) - 1; }

        public void subItemClick(string type)
        {
            Vector2 defaultSize = new Vector2(300, 250);
            Vector2 defaultPos = new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2) - new Vector2(defaultSize.X / 2, defaultSize.Y / 2);
            if (subItemNames[1].Contains(type) && ObjManager.Windows.Find(x => x.GetType().Name == type + "Window") != null)
            {
                Window tempWindow = ObjManager.Windows.Find(x => x.GetType().Name == type + "Window");
                if(tempWindow.docked) return;
                tempWindow.pos = defaultPos;
                tempWindow.dim = defaultSize;
                return;
            }

            switch (type)
            {
                case "Canvas":
                    CanvasWindow cw = new CanvasWindow(defaultPos, defaultSize);
                    Globals.canvas.window = cw;
                    Globals.PassWindow(cw);
                    break;
                case "Tools":
                    Globals.PassWindow(new ToolsWindow(defaultPos, defaultSize));
                    break;
                case "Timeline":
                    Globals.PassWindow(new TimelineWindow(defaultPos, defaultSize));
                    break;
                case "ColorPicker":
                    Globals.PassWindow(new ColorPickerWindow(defaultPos, defaultSize));
                    break;
                case "Playback":
                    Globals.PassWindow(new PlaybackWindow(defaultPos, defaultSize));
                    break;
                case "New":
                    Globals.PassWindow(new NewWindow());
                    break;
                case "GitHub":
                    var psi = new ProcessStartInfo
                    {
                        FileName = "https://github.com/RustingRobot/Kolibri",
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                    break;
                default:
                    break;
            }
        }


        public override void Update(Vector2 OFFSET)
        {

            base.Update(OFFSET);
            for (int i = 0; i < menuitems.Length; i++)
            {
                menuitems[i].Update(OFFSET);
            }

            dim.X = Globals.screenWidth;
            if (visibleSubMenu == -1) return;
            for (int i = 0; i < subItems[visibleSubMenu].Count; i++)
            {
                if(visibleSubMenu == 1)
                {
                    if(ObjManager.Windows.Find(x => x.GetType().Name == subItemNames[visibleSubMenu][i] + "Window") != null)
                    {
                        subItems[visibleSubMenu][i].normColor = new Color(51, 56, 61);
                    }
                    else
                    {
                        subItems[visibleSubMenu][i].normColor = new Color(39, 44, 48);
                    }
                }
                subItems[visibleSubMenu][i].Update(OFFSET);
            }
            if (Globals.mouse.LeftClick() && Globals.mouse.newMousePos.Y > 24) visibleSubMenu = -1;
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            for (int i = 0; i < menuitems.Length; i++)
            {
                menuitems[i].Draw(OFFSET);
            }
            if (visibleSubMenu == -1) return;
            for (int i = 0; i < subItems[visibleSubMenu].Count; i++)
            {
                subItems[visibleSubMenu][i].Draw(OFFSET);
            }
        }


    }
    
}