using Kolibri.Engine;
using Kolibri.Source.Workspace;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kolibri.Source
{
    public class ObjManager
    {
        public Vector2 offset;
        public List<Window> Windows = new List<Window>();
        public Canvas canvas;
        public ObjManager()
        {
            Globals.PassWindow = AddWindow;
            offset = Vector2.Zero;
            Globals.PassWindow(new Window(new Vector2(500,50),new Vector2(300,250),"test"));
            Globals.PassWindow(new Window(new Vector2(500, 350), new Vector2(400, 200), "window"));
            canvas = new Canvas(new Vector2(50,50), new Vector2(400,400));
        }
        public virtual void Update()
        {
            canvas.Update(offset);
            for (int i = 0; i < Windows.Count; i++)
            {
                Windows[i].Update(offset);
                if (Windows[i].delete)
                {
                    Windows.RemoveAt(i);
                    i--;
                }
            }
        }

        public virtual void Draw()
        {
            canvas.Draw(offset);
            for (int i = 0; i < Windows.Count; i++)
            {
                Windows[i].Draw(offset, new Color(56, 58, 60));
            }
        }

        public virtual void AddWindow(object INFO)
        {
            Windows.Add((Window)INFO);
        }
    }
}
