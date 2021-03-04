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
        public int currentFrame = 1;
        public List<Frame> frames = new List<Frame>();

        Window window;

        public Timeline(Window WINDOW)
        {
            window = WINDOW;
            frames.Add(new Frame(window));
            frames.Add(new Frame(window));
            frames.Add(new Frame(window));
        }

        public void Draw()
        {
            for (int i = 0; i < frames.Count; i++)
            {
                frames[i].Draw(new Vector2(30 + (i * (frames[i].dim.X + 5)), 80));
            }
        }


        public void addFrame()
        {
            frames.Insert(currentFrame + 1,new Frame(window));     //erstmal am Ende hinzugefügt
            currentFrame++;
        }

        public void deleteFrame()
        {
            frames.RemoveAt(currentFrame);
        }

        public void swapFrames()
        {
            
        }
         
    }
}