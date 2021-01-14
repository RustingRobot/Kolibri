using Kolibri.Engine;
using Kolibri.Source.workspace;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kolibri.Source
{
    public class Workspace
    {
        public Vector2 offset;
        public List<Window> Windows = new List<Window>();

        public Workspace()
        {
            Globals.PassWindow = AddWindow;
            offset = Vector2.Zero;
            Globals.PassWindow(new Window(new Vector2(200,200),new Vector2(300,250),"test"));
            Globals.PassWindow(new Window(new Vector2(400, 250), new Vector2(300, 250), "window"));
        }
        public virtual void Update()
        {
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
            for (int i = 0; i < Windows.Count; i++)
            {
                Windows[i].Draw(offset, new Color(32,34,36));
            }
        }

        public virtual void AddWindow(object INFO)
        {
            Windows.Add((Window)INFO);
        }
    }
}
