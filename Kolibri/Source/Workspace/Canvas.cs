using Kolibri.Engine;
using Kolibri.Engine.Input;
using Kolibri.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Kolibri.Source.Workspace
{
    public class Canvas //pixel grid to draw on
    {
        Texture2D canvas;
        Vector2 pos, dim;
        UInt32[] pixels;
        Random rnd = new Random();
        //List<Vector2> mousePoints = new List<Vector2>();
        public Canvas(Vector2 POS, Vector2 DIM)
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
        public void Update(Vector2 OFFSET)
        {
            //drawLine(new Vector2(65, 65), Globals.mouse.newMousePos, Color.Red);
            Globals.graphicsDevice.Textures[0] = null;
            if (Globals.GetBoxOverlap(pos, dim, Globals.mouse.newMousePos, Vector2.Zero) && Globals.mouse.LeftClickHold() && !Globals.dragging) //only calculate if the mouse is supposed to draw on the canvas
            {
                drawLine(Globals.mouse.oldMousePos, Globals.mouse.newMousePos, Color.CornflowerBlue);
            }
            canvas.SetData<UInt32>(pixels, 0, (int)dim.X * (int)dim.Y);
        }

        public void setPixel(Vector2 position, Color color)
        {
            if((int)(position.Y - pos.Y) * (int)dim.X + (int)(position.X - pos.X) < pixels.Length && (int)(position.Y - pos.Y) * (int)dim.X + (int)(position.X - pos.X) > 0)
                pixels[(int)(position.Y - pos.Y) * (int)dim.X + (int)(position.X - pos.X)] = (uint)((color.A << 24) | (color.B << 16) | (color.G << 8) | (color.R << 0));
        }

        public void drawLine(Vector2 pos0, Vector2 pos1, Color color)   //implementation of bresenham's line algorithm
        {
            float x = pos0.X, y = pos0.Y;
            float dx = pos1.X - pos0.X;
            float dy = pos1.Y - pos0.Y;
            bool xFaster = dx > dy;
            float f = (xFaster) ? dx / 2 : dy / 2;
            setPixel(pos0, color);

            while (x != pos1.X && y != pos1.Y) 
            {
                if (xFaster)
                {
                    x++;
                    f -= dy;
                    if (f < 0)
                    {
                        y++;
                        f += dx;
                    }
                }
                else
                {
                    y++;
                    f -= dx;
                    if (f < 0)
                    {
                        x++;
                        f += dy;
                    }
                }
            setPixel(new Vector2(x, y), color);
            }
        }

        public void Draw(Vector2 OFFSET)
        {
            Globals.spriteBatch.Draw(canvas, new Rectangle( (int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
        }
    }
}
