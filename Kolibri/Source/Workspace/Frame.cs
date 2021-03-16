using Kolibri.Engine;
using Kolibri.Engine.Input;
using Kolibri.Source;
using Kolibri.Source.Workspace;
using Kolibri.Source.Workspace.UIElements;
using Kolibri.Source.Workspace.Windows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Kolibri.Source.Workspace
{
    public class Frame: UIElement
    {
        public UInt32[] pixels;

        Color color;
        int border = 2, index;
        public Layer layer;

        public Frame(Layer LAYER,Window WINDOW, Timeline TIMELINE): base(WINDOW, Vector2.Zero, new Vector2(15, 25))
        {
            layer = LAYER;
            index = layer.Frames.Count;
            color = Color.Gray;
            pixels = new uint[(int)Globals.canvas.dim.X*(int)Globals.canvas.dim.Y];
            clearFrame();
        }

        public override void Update(Vector2 OFFSET)
        {
            if (layer.timeline.currentFrame == index)
            {
                Globals.canvas.pixelsList[layer.layerIndex] = pixels;
            }

            if (layer.timeline.currentFrame == index && layer.timeline.currentLayer == layer.layerIndex)
            {
                color = new Color(60, 104, 148);
                
            } 
            else if (Globals.GetBoxOverlap(OFFSET + window.pos, dim, Globals.mouse.newMousePos, Vector2.Zero))
            {
                color = Color.LightGray;

                if (Globals.mouse.LeftClick())
                {   
                    layer.timeline.selectEndFrame = index;
                    if (!Globals.keyboard.GetPress("LeftShift") || layer.timeline.currentLayer != layer.layerIndex)
                    {
                        layer.timeline.currentFrame = index;
                    }
                    if(layer.timeline.currentLayer != layer.layerIndex)
                    {
                        layer.timeline.currentLayer = layer.layerIndex;
                    }
                }
            }
            else if ((index <= layer.timeline.selectEndFrame && index > layer.timeline.currentFrame) || (index >= layer.timeline.selectEndFrame && index < layer.timeline.currentFrame))
            {
                color = new Color(104, 152, 165);
            }
            else color = Color.Gray;
            base.Update(OFFSET);
        }
        public override void Draw(Vector2 OFFSET)
        {
            if (Array.TrueForAll(pixels, y => y == 0x00000000))
            {
            
                Globals.primitives.DrawRect(OFFSET + window.pos, new Vector2(dim.X,border), color);
                Globals.primitives.DrawRect(OFFSET + window.pos, new Vector2(border, dim.Y), color);
                Globals.primitives.DrawRect(new Vector2(OFFSET.X + window.pos.X + dim.X - border , OFFSET.Y + window.pos.Y), new Vector2(border, dim.Y), color);
                Globals.primitives.DrawRect(new Vector2(OFFSET.X + window.pos.X , OFFSET.Y + window.pos.Y + dim.Y - border), new Vector2(dim.X, border), color);
            }

            else
            {
                Globals.primitives.DrawRect(OFFSET + window.pos, dim, color);
            }
        }

        public void clearFrame()
        {
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = 0x00000000;
            }
        }
    }
}