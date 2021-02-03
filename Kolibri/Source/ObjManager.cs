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
        public List<Window> Windows = new List<Window>();
        public Canvas canvas;
        public ObjManager()
        {
            Globals.PassWindow = AddWindow;
            offset = Vector2.Zero;
            //add windows
            Globals.PassWindow(new Window(new Vector2(500,50),new Vector2(300,250),"I am a blank window"));
            Globals.PassWindow(new TestWindow(new Vector2(500, 350), new Vector2(400, 200)));

            canvas = new Canvas(new Vector2(50,50), new Vector2(400,400));

            Globals.PassWindow(new Menu(new Vector2(0,0), new Vector2 (1000, 16)));
        }
        public virtual void Update()
        {
            canvas.Update(offset);  //temporary
            for (int i = 0; i < Windows.Count; i++) //dynamic windows update loop
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
            canvas.Draw(offset);    //temporary
            for (int i = 0; i < Windows.Count; i++) //dynamic windows draw loop
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
