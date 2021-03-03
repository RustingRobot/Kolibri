using Kolibri.Engine;
using Kolibri.Engine.Input;
using Kolibri.Source;
using Kolibri.Source.Workspace;
using Kolibri.Source.Workspace.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Kolibri.Source.Workspace
{
    public class Timeline
    {
        public Frame startFrame, actualFrame;
         public List<Frame> frames = new List<Frame>();

        public Timeline()
        {
            frames.Add(new Frame());
            actualFrame = frames[0];
            startFrame = frames[0];
            
        }



        public void addFrame()
        {
            frames.Insert(frames.IndexOf(actualFrame)+1,new Frame());     //erstmal am Ende hinzugefügt
            actualFrame = frames[frames.IndexOf(actualFrame)+1];
        }

        public void deleteFrame()
        {
            frames.RemoveAt(frames.IndexOf(actualFrame));
            
        }


        public void goToFirst()
        {
            actualFrame = startFrame;
        }

        public void swapFrames()
        {
            
        }
         
    }
}