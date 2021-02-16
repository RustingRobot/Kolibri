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
    public class Canvas : UIElement //pixel grid to draw on
    {
        Texture2D canvas;
        UInt32[] pixels;

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
            if (Globals.GetBoxOverlap(pos, dim, Globals.mouse.newMousePos, Vector2.Zero) && Globals.mouse.LeftClickHold() && !Globals.dragging) //only calculate if the mouse is supposed to draw on the canvas
            {
                drawLine(Globals.mouse.oldMousePos, Globals.mouse.newMousePos, Color.Gray);
            }
            canvas.SetData<UInt32>(pixels, 0, (int)dim.X * (int)dim.Y);
            base.Update(OFFSET);
        }

        public void setPixel(Vector2 position, Color color)
        {
            if((int)(position.Y - pos.Y) * (int)dim.X + (int)(position.X - pos.X) < pixels.Length && (int)(position.Y - pos.Y) * (int)dim.X + (int)(position.X - pos.X) > 0)
                pixels[(int)(position.Y - pos.Y) * (int)dim.X + (int)(position.X - pos.X)] = (uint)((color.A << 24) | (color.B << 16) | (color.G << 8) | (color.R << 0));
        }

        public void drawLine(Vector2 pos0, Vector2 pos1, Color color)   //implementation of Bresenham's line algorithm
        {
            int dx = Math.Abs((int)pos1.X - (int)pos0.X);   //delta x
            int dy = -Math.Abs((int)pos1.Y - (int)pos0.Y);  //delta y
            int sx = pos0.X < pos1.X ? 1 : -1, sy = pos0.Y < pos1.Y ? 1 : -1;   //quatrant adjust
            int e = dx + dy;    //error
            while (true)
            {
                setPixel(new Vector2(pos0.X, pos0.Y), color);
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

        public override void Draw(Vector2 OFFSET)
        {
            Globals.spriteBatch.Draw(canvas, new Rectangle( (int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
        }
    }
}
