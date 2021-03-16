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
        public int currentLayer = 0, currentFrame = 0, selectEndFrame = 0;
        public List<Layer> Layers = new List<Layer>();
        Window window;

        public Timeline(Window WINDOW)
        {
            window = WINDOW;
            AddLayer();
        }

        public void Draw(Vector2 OFFSET)
        {
            Globals.primitives.DrawRect(window.pos + new Vector2(currentFrame * 17 + 80, 47), new Vector2(15, Layers.Count * 27 + 3), new Color(40,44,50));

            for (int i = 0; i < Layers.Count; i++) 
            {
                Layers[i].Draw(OFFSET);
            }
            
        }

        public void Update(Vector2 OFFSET)
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                Layers[i].Update(OFFSET);
            }

        }

        public void AddLayer()
        {
            Globals.canvas.textures.Add(new Texture2D(Globals.graphicsDevice, (int)Globals.canvas.dim.X, (int)Globals.canvas.dim.Y, false, SurfaceFormat.Color));
            Globals.canvas.pixelsList.Add(new UInt32[(int)Globals.canvas.dim.X * (int)Globals.canvas.dim.Y]);
            Layers.Add(new Layer(Layers.Count, this, window));
        }

        public void DeleteLayer()
        {
            if (currentLayer == 0) return;
            Globals.canvas.textures.RemoveAt(currentLayer);
            Globals.canvas.pixelsList.RemoveAt(currentLayer);
            Layers.RemoveAt(currentLayer);
            for (int i = currentLayer; i < Layers.Count; i++)
            {
                Layers[i].moveUp();
            }
            if (currentLayer == Layers.Count) currentLayer--;
        }

        public void gotoFrame(int FrameIndex)
        {
            currentFrame = FrameIndex;
            selectEndFrame = FrameIndex;
        }

        public void nextFrame()
        {
            currentFrame++;
            selectEndFrame++;
        }

        public void previousFrame()
        {
            currentFrame--;
            selectEndFrame--;
        }
    }
}