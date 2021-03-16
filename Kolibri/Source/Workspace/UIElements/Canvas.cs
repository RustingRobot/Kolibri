using Kolibri.Engine;
using Kolibri.Engine.Input;
using Kolibri.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Kolibri.Source.Workspace.UIElements;
using Kolibri.Source.Workspace.Windows;
using System.Linq;

namespace Kolibri.Source.Workspace.UIElements
{
    public class Canvas : UIElement //pixel grid to draw on
    {
        public TimelineWindow timelineWindow;
        public Vector2 offset = Vector2.Zero;
        public float zoom = 1;
        public int BrushSize = 1, EraserSize = 1;
        public ColorPickerWindow cp;

        Texture2D background;

        public List<Texture2D> textures = new List<Texture2D>(), predFrames = new List<Texture2D>(), succFrames = new List<Texture2D>();
        public List<UInt32[]> pixelsList = new List<UInt32[]>();

        public Canvas(Window WINDOW, Vector2 POS, Vector2 DIM) : base(WINDOW, POS, DIM)
        {
            pos = POS;
            dim = DIM;

            UInt32[] pixels = new uint[(int)dim.X * (int)dim.Y];
            for (int i = 0; i < pixels.Length; i++) //set all pixels to white (background will not change color)
            {
                pixels[i] = 0xFFFFFFFF;
            }
            background = new Texture2D(Globals.graphicsDevice, (int)dim.X, (int)dim.Y, false, SurfaceFormat.Color);
            background.SetData<UInt32>(pixels, 0, (int)dim.X * (int)dim.Y);
        }

        public override void Update(Vector2 OFFSET)
        {
            //access to TimelineWindow
            if(timelineWindow==null)
            {
                timelineWindow = (TimelineWindow)ObjManager.Windows.Find(x => x.GetType().Name =="TimelineWindow");
                if (timelineWindow != null && timelineWindow.delete) timelineWindow = null; //prevent reasignment before old window could be deleted
            }

            if (Globals.mouse.LeftClickHold() && Globals.interactWindow == null)
            {
                //access to ColorPickerWindow
                if (cp == null)
                {
                    cp = (ColorPickerWindow)ObjManager.Windows.Find(x => x.GetType().Name == "ColorPickerWindow");
                    if (cp != null && cp.delete) cp = null; //prevent reasignment before old window could be deleted
                }
                else
                {
                    Vector2 newDrawPos = new Vector2((float)Math.Ceiling((Globals.mouse.newMousePos.X - offset.X - pos.X) / zoom), (float)Math.Ceiling((Globals.mouse.newMousePos.Y - offset.Y - pos.Y) / zoom));
                    Vector2 oldDrawPos = new Vector2((float)Math.Ceiling((Globals.mouse.oldMousePos.X - offset.X - pos.X) / zoom), (float)Math.Ceiling((Globals.mouse.oldMousePos.Y - offset.Y - pos.Y) / zoom));

                    switch (Globals.activeTool)
                    {
                        case ("Brush"):
                            drawLine(oldDrawPos, newDrawPos, cp.currentColor);  //draw a line since the mouse "teleports" across the screen
                            break;
                        case ("Eraser"):
                            drawLine(oldDrawPos, newDrawPos, Color.Transparent);
                            break;
                        case ("BucketFill"):
                            FloodFill(newDrawPos, cp.currentColor);
                            break;
                        case ("EyeDropper"):
                            if(window.MouseInWindow())
                                cp.setColor(getPixel(newDrawPos));
                            break;
                        default:
                            break;
                    }
                }
            }
            for (int i = 0; i < textures.Count; i++)    //turn pixels (from the frames) into textures
            {
                textures[i].SetData<UInt32>(pixelsList[i], 0, (int)dim.X * (int)dim.Y);
            }
            if (timelineWindow != null && timelineWindow.osActive)  //get onion skin textures
            {
                predFrames = Enumerable.Repeat<Texture2D>(new Texture2D(Globals.graphicsDevice, (int)Globals.canvas.dim.X, (int)Globals.canvas.dim.Y, false, SurfaceFormat.Color), textures.Count).ToList();
                succFrames = Enumerable.Repeat<Texture2D>(new Texture2D(Globals.graphicsDevice, (int)Globals.canvas.dim.X, (int)Globals.canvas.dim.Y, false, SurfaceFormat.Color), textures.Count).ToList();

                if (timelineWindow.timeline.currentFrame > 0)
                {
                    for (int i = 0; i < predFrames.Count; i++)
                    {
                        predFrames[i].SetData<UInt32>(timelineWindow.timeline.Layers[i].Frames[timelineWindow.timeline.currentFrame - 1].pixels);
                    }
                }
                try
                {
                    if (timelineWindow.timeline.currentFrame < timelineWindow.timeline.Layers[0].Frames.Count - 1)
                    {
                        for (int i = 0; i < succFrames.Count; i++)
                        {
                            succFrames[i].SetData<UInt32>(timelineWindow.timeline.Layers[i].Frames[timelineWindow.timeline.currentFrame + 1].pixels);
                        }
                    }
                }
                catch { }
            }
            else
            {
                predFrames = new List<Texture2D>();
                succFrames = new List<Texture2D>();
            }
            base.Update(OFFSET + offset);
        }

        public void setPixel(Vector2 position, Color color)
        {
            if (position.X < dim.X && position.X >= 0 && position.Y < dim.Y && position.Y >= 0 && timelineWindow != null)
                pixelsList[timelineWindow.timeline.currentLayer][(int)(position.Y * dim.X + position.X)] = (uint)((color.A << 24) | (color.B << 16) | (color.G << 8) | (color.R << 0));

        }

        public Color getPixel(Vector2 position)
        {
            if ((int)(position.Y * dim.X + position.X) < pixelsList[0].Length && (int)(position.Y * dim.X + position.X) >= 0 && timelineWindow != null)
                return new Color(pixelsList[timelineWindow.timeline.currentLayer][(int)(position.Y * dim.X + position.X)]);
            else
                return Color.Transparent;
        }

        //drawing 
        public void drawLine(Vector2 pos0, Vector2 pos1, Color color)   //implementation of Bresenham's line algorithm
        {
            int dx = Math.Abs((int)pos1.X - (int)pos0.X);   //delta x
            int dy = -Math.Abs((int)pos1.Y - (int)pos0.Y);  //delta y
            int sx = pos0.X < pos1.X ? 1 : -1, sy = pos0.Y < pos1.Y ? 1 : -1;   //quatrant adjust
            int e = dx + dy;    //error
            while (true)
            {
                int drawSize = (Globals.activeTool == "Brush") ? BrushSize : EraserSize;
                if(drawSize == 1) setPixel(pos0, color);
                else if(drawSize == 2){
                    setPixel(pos0, color);
                    setPixel(pos0 + new Vector2(0,-1), color);
                    setPixel(pos0 + new Vector2(-1, -1), color);
                    setPixel(pos0 + new Vector2(-1, 0), color);
                }
                else drawFilledCircle(pos0, drawSize - 1, color);
                if (pos0.X == pos1.X && pos0.Y == pos1.Y) break;    //step end
                int e2 = 2 * e;
                if (e2 >= dy)
                {
                    e += dy;
                    pos0.X += sx;
                }
                if (e2 <= dx)
                {
                    e += dx;
                    pos0.Y += sy;
                }
            }
        }

        void drawCircle(Vector2 p, int x, int y, Color color)
        {
            setPixel(new Vector2(p.X + x, p.Y + y), color);
            setPixel(new Vector2(p.X - x, p.Y + y), color);
            setPixel(new Vector2(p.X + x, p.Y - y), color);
            setPixel(new Vector2(p.X - x, p.Y - y), color);
            setPixel(new Vector2(p.X + y, p.Y + x), color);
            setPixel(new Vector2(p.X - y, p.Y + x), color);
            setPixel(new Vector2(p.X + y, p.Y - x), color);
            setPixel(new Vector2(p.X - y, p.Y - x), color);
        }

        public void drawFilledCircle(Vector2 p, int r, Color color)
        {
            for (int y = -r; y <= r; y++)
                for (int x = -r; x <= r; x++)
                    if (x * x + y * y < r * r + r)
                        setPixel(new Vector2(p.X + x, p.Y + y), color);
        }
        private void FloodFill(Vector2 pt, Color color)
        {
            Stack<Vector2> pixels = new Stack<Vector2>();
            Color targetColor = getPixel(pt);
            if (targetColor == color) return;
            pixels.Push(pt);

            while (pixels.Count > 0)
            {
                Vector2 a = pixels.Pop();
                if (a.X < dim.X && a.X >= 0 && a.Y < dim.Y && a.Y >= 0) //in bounds
                {
                    if (getPixel(a) == targetColor)
                    {
                        setPixel(a, color);
                        pixels.Push(new Vector2(a.X - 1, a.Y));
                        pixels.Push(new Vector2(a.X + 1, a.Y));
                        pixels.Push(new Vector2(a.X, a.Y - 1));
                        pixels.Push(new Vector2(a.X, a.Y + 1));
                    }
                }
            }
            return;
        }



        public override void Draw(Vector2 OFFSET)
        {
            Globals.spriteBatch.Draw(background, new Rectangle( (int)(pos.X + offset.X), (int)(pos.Y + offset.Y), (int)(dim.X * zoom), (int)(dim.Y * zoom)), Color.White);
            for (int i = 0; i < predFrames.Count; i++)  //onion skin previous frames
            {
                Globals.spriteBatch.Draw(predFrames[i], new Rectangle((int)(pos.X + offset.X), (int)(pos.Y + offset.Y), (int)(dim.X * zoom), (int)(dim.Y * zoom)), Color.White * 0.2f);
            }
            for (int i = 0; i < succFrames.Count; i++) //onion skin next frames
            {
                Globals.spriteBatch.Draw(succFrames[i], new Rectangle((int)(pos.X + offset.X), (int)(pos.Y + offset.Y), (int)(dim.X * zoom), (int)(dim.Y * zoom)), Color.White * 0.2f);
            }
            for (int i = 0; i < textures.Count; i++)    //all Frames of all Layers in that are active
            {
                Globals.spriteBatch.Draw(textures[i], new Rectangle((int)(pos.X + offset.X), (int)(pos.Y + offset.Y), (int)(dim.X * zoom), (int)(dim.Y * zoom)), Color.White);
            }

        }
    }
}