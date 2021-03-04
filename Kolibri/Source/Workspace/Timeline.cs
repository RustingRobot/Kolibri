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
using System.Diagnostics;

namespace Kolibri.Source.Workspace
{
    public class Timeline
    {
        public int currentFrame = 0, selectEndFrame;
        public List<Frame> frames = new List<Frame>();

        Window window;

        public Timeline(Window WINDOW)
        {
            window = WINDOW;
            frames.Add(new Frame(window, this));
        }

        public void Draw()
        {
            Globals.primitives.DrawTxt("Layer 1", new Vector2(10, 58) + window.pos,Globals.fontSize, Color.Gray);
            for (int i = 0; i < frames.Count; i++)
            {
                frames[i].Draw(new Vector2(70 + (i * (frames[i].dim.X + 2)), 55));
            }
        }

        public void Update()
        {
            if (Globals.keyboard.OnPress("Left") && currentFrame > 0 && selectEndFrame > 0) 
            {
                selectEndFrame--;
                if (!Globals.keyboard.GetPress("LeftShift"))
                {
                    currentFrame--;
                    Globals.canvas.pixels = frames[currentFrame].pixels;
                }
            }
            else if (Globals.keyboard.OnPress("Right") && currentFrame < frames.Count - 1 && selectEndFrame < frames.Count - 1) 
            {
                selectEndFrame++;
                if (!Globals.keyboard.GetPress("LeftShift"))
                {
                    currentFrame++;
                    Globals.canvas.pixels = frames[currentFrame].pixels;
                }
            }

            for (int i = 0; i < frames.Count; i++)
            {
                frames[i].Update(new Vector2(70 + (i * (frames[i].dim.X + 2)), 55));
            }

            if(window.dim.X / (frames[0].dim.X + 2) > frames.Count)
            {
                frames.Add(new Frame(window, this));
            }
        }

        public void clearFrames()
        {
            int i = currentFrame;
            frames[i].clearFrame();
            while (selectEndFrame != i)
            {
                i += (selectEndFrame > i) ? 1 : -1;
                frames[i].clearFrame();
                
            }
        }
    }
}