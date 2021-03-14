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
    public class Frame: UIElement
    {
        public UInt32[] pixels;
        Timeline timeline;

        Color color;
        int border = 2, index;
        int layerIndex;

        public Frame(int LAYERINDEX,Window WINDOW, Timeline TIMELINE): base(WINDOW, Vector2.Zero, new Vector2(15, 25))
        {
            layerIndex = LAYERINDEX;
            color = Color.Gray;
            timeline = TIMELINE;
            pixels = new uint[(int)Globals.canvas.dim.X*(int)Globals.canvas.dim.Y];
            clearFrame();
        }

        public override void Update(Vector2 OFFSET)
        {
            if(timeline.layer.currentLayer == true)
            {
                if(layerIndex==Globals.canvas.pixelsList.Count-1)
                {
                    pos = OFFSET + window.pos;
                    index = timeline.frames.IndexOf(this);
                    if (timeline.frames[timeline.currentFrame] == this)
                    {
                        color = new Color(60, 104, 148);
                        pixels = Globals.canvas.pixelsList[layerIndex];
                        
                    } 
                    else if (MouseHover())
                    {
                        color = Color.LightGray;

                        if (Clicked())
                        {
                            timeline.selectEndFrame = index;
                            if (!Globals.keyboard.GetPress("LeftShift"))
                            {
                                timeline.currentFrame = index;
                                Globals.canvas.pixelsList[layerIndex] = pixels;
                            }
                        }
                    }
                    else if ((index <= timeline.selectEndFrame && index > timeline.currentFrame) || (index >= timeline.selectEndFrame && index < timeline.currentFrame))
                    {
                        color = new Color(104, 152, 165);
                    }
                    else color = Color.Gray;
                }
            base.Update(OFFSET);
            }
        }
        public override void Draw(Vector2 OFFSET)
        {
            if (Array.TrueForAll(pixels, y => y == 0xFFFFFFFF))
            {
            
                Globals.primitives.DrawRect(OFFSET + window.pos + new Vector2(0, 30*layerIndex), new Vector2(dim.X,border), color);
                Globals.primitives.DrawRect(OFFSET + window.pos +  new Vector2(0,30*layerIndex), new Vector2(border, dim.Y), color);
                Globals.primitives.DrawRect(new Vector2(OFFSET.X + window.pos.X + dim.X - border , OFFSET.Y + window.pos.Y+ 30*layerIndex), new Vector2(border, dim.Y), color);
                Globals.primitives.DrawRect(new Vector2(OFFSET.X + window.pos.X , OFFSET.Y + window.pos.Y + dim.Y - border+ 30*layerIndex), new Vector2(dim.X, border), color);
            }
            else Globals.primitives.DrawRect(OFFSET + window.pos, dim, color);
        }

        public void clearFrame()
        {
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = 0xFFFFFFFF;
            }
        }
    }
}