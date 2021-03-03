using Kolibri.Engine;
using Kolibri.Engine.Input;
using Kolibri.Source;
using Kolibri.Source.Workspace;
using Kolibri.Source.Workspace.Windows;
using Kolibri.Source.Workspace.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Kolibri.Source.Workspace
{
    public class Frame
    {
        UInt32[] pixels;

        public Frame predFrame;
        public  Frame succFrame;
        public int index;
        Canvas canvas;
        public Frame()
        {
            pixels = new uint[(int)canvas.dim.X*(int)canvas.dim.Y]; 
            for (int i = 0; i < pixels.Length; i++) 
            {
                pixels[i] = 0xFFFFFFFF;
            }
            Update();
        }

        public void Update()
        {
            if(timeline.actualFrame == this)    //wieso kann der hier nicht auf timeline zugreifen?
            {
                this.pixels = canvas.pixels;
            }
            predFrame = timeline.frames[timeline.frames.IndexOf(timeline.actualFrame)-1];
            succFrame = timeline.frames[timeline.frames.IndexOf(timeline.actualFrame)+1];
            Update();
        }
       
        
    }
}