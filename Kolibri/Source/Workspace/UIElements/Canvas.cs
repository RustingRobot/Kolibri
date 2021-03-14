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


namespace Kolibri.Source.Workspace.UIElements
{
    public class Canvas : UIElement //pixel grid to draw on
    {
        public Vector2 offset = Vector2.Zero;
        public float zoom = 1;
        public int BrushSize = 1, EraserSize = 1;
        public UInt32[] pixels;
        private ColorPickerWindow cp;

        Texture2D canvas;

        public Canvas(Window WINDOW, Vector2 POS, Vector2 DIM) : base(WINDOW, POS, DIM)
        {
            pos = POS;
            dim = DIM;
            canvas = new Texture2D(Globals.graphicsDevice, (int)dim.X, (int)dim.Y, false, SurfaceFormat.Color);
            pixels = new UInt32[(int)dim.X * (int)dim.Y];
            for (int i = 0; i < pixels.Length; i++) //set all pixels to white
            {
                pixels[i] = 0xFFFFFFFF;
            }
        }
        public override void Update(Vector2 OFFSET)
        {
            Globals.graphicsDevice.Textures[0] = null;
            if (Globals.mouse.LeftClickHold() && Globals.interactWindow == null)
            {
                if (cp == null)
                {
                    cp = (ColorPickerWindow)ObjManager.Windows.Find(x => x.GetType().Name == "ColorPicker");
                }
                else
                {
                    Vector2 newDrawPos = new Vector2((float)Math.Ceiling((Globals.mouse.newMousePos.X - offset.X - pos.X) / zoom), (float)Math.Ceiling((Globals.mouse.newMousePos.Y - offset.Y - pos.Y) / zoom));
                    Vector2 oldDrawPos = new Vector2((float)Math.Ceiling((Globals.mouse.oldMousePos.X - offset.X - pos.X) / zoom), (float)Math.Ceiling((Globals.mouse.oldMousePos.Y - offset.Y - pos.Y) / zoom));

                    switch (Globals.activeTool)
                    {
                        case ("Brush"):
                            drawLine(oldDrawPos, newDrawPos, cp.currentColor);
                            break;
                        case ("Eraser"):
                            drawLine(oldDrawPos, newDrawPos, Color.White);
                            break;
                        case ("BucketFill"):
                            FloodFill(newDrawPos, cp.currentColor);
                            break;
                        default:
                            break;
                    }
                }
            }
            canvas.SetData<UInt32>(pixels, 0, (int)dim.X * (int)dim.Y);
            base.Update(OFFSET + offset);
        }

        public void setPixel(Vector2 position, Color color)
        {
            //if((int)(position.Y * dim.X + position.X) < pixels.Length && (int)(position.Y * dim.X + position.X) >= 0)
            if (position.X < dim.X && position.X >= 0 && position.Y < dim.Y && position.Y >= 0)
                pixels[(int)(position.Y * dim.X + position.X)] = (uint)((color.A << 24) | (color.B << 16) | (color.G << 8) | (color.R << 0));
        }

        public Color getPixel(Vector2 position)
        {
            if ((int)(position.Y * dim.X + position.X) < pixels.Length && (int)(position.Y * dim.X + position.X) >= 0)
                return new Color(pixels[(int)(position.Y * dim.X + position.X)]);
            else
                return Color.Transparent;
        }

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

        public void drawCircle(Vector2 p, int r, Color color)
        {
            int x = 0, y = r;
            int d = 3 - 2 * r;
            drawCircle(p, x, y, color);
            while (y >= x)
            {
                x++;
                if (d > 0)
                {
                    y--;
                    d = d + 4 * (x - y) + 10;
                }
                else
                    d = d + 4 * x + 6;
                drawCircle(p, x, y, color);
            }
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
                if (a.X < dim.X && a.X >= 0 && a.Y < dim.Y && a.Y >= 0)//in bounds
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
            Globals.spriteBatch.Draw(canvas, new Rectangle( (int)(pos.X + offset.X), (int)(pos.Y + offset.Y), (int)(dim.X * zoom), (int)(dim.Y * zoom)), Color.White);
        }
    }
}