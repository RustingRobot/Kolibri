using Kolibri.Engine;
using Kolibri.Source.Workspace;
using Kolibri.Source.Workspace.UIElements;
using Kolibri.Source.Workspace.Windows;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kolibri.Source
{
    public class ObjManager //adds and removes all objects on screen
    {
        public Vector2 offset;
        public static List<Window> Windows = new List<Window>();
        public Canvas canvas;
        public Menu menu;
        public ObjManager()
        {
            Globals.PassWindow = AddWindow;
            offset = Vector2.Zero;
            //add windows
            Globals.PassWindow(new Window(new Vector2(500,50),new Vector2(300,250),"I am a blank window"));
            Globals.PassWindow(new TestWindow(new Vector2(500, 350), new Vector2(400, 200)));
            Globals.PassWindow(new CanvasWindow(new Vector2(50, 350), new Vector2(400, 400)));
            //canvas = new Canvas(new Vector2(50,50), new Vector2(400,400));
            menu = new Menu(Vector2.Zero, new Vector2(1000, 24));
        }
        public virtual void Update()
        {
            for (int i = 0; i < Windows.Count; i++) //dynamic windows update loop
            {
                Windows[i].Update(offset);
                if (Windows[i].delete)
                {
                    Windows.RemoveAt(i);
                    i--;
                }
            }
            menu.Update(offset);
        }

        public virtual void Draw()
        {
            for (int i = 0; i < Windows.Count; i++) //dynamic windows draw loop
            {
                Windows[i].Draw(offset, new Color(56, 58, 60));
            }
            menu.Draw(offset, new Color(39, 44, 48));
        }

        public virtual void AddWindow(object INFO)
        {
            Windows.Add((Window)INFO);
        }

        public static void toFront(Window WINDOW)
        {
            Windows.Remove(WINDOW);
            Windows.Add(WINDOW);
        }
    }
}
