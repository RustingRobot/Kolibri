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
    public class Canvas
    {
        Texture2D canvas;
        Vector2 pos, dim;
        UInt32[] pixels;
        Random rnd = new Random();
        //List<Vector2> mousePoints = new List<Vector2>();
        Vector2 delta;
        public Canvas(Vector2 POS, Vector2 DIM)
        {
            pos = POS;
            dim = DIM;
            canvas = new Texture2D(Globals.graphicsDevice, (int)dim.X, (int)dim.Y, false, SurfaceFormat.Color);
            pixels = new UInt32[(int)dim.X * (int)dim.Y];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = 0xFFFFFFFF;
            }
        }
        public void Update(Vector2 OFFSET)
        {
            Globals.graphicsDevice.Textures[0] = null;
            delta = Globals.mouse.newMousePos - Globals.mouse.oldMousePos;
            if (Globals.GetBoxOverlap(pos, dim, Globals.mouse.newMousePos, Vector2.Zero) && Globals.mouse.LeftClickHold() && !Globals.dragging)
            {
                pixels[(int)(Globals.mouse.newMousePos.Y - pos.Y) * (int)dim.X + (int)(Globals.mouse.newMousePos.X - pos.X)] = 0xFF000000;
            }
            canvas.SetData<UInt32>(pixels, 0, (int)dim.X * (int)dim.Y);
        }

        public void Draw(Vector2 OFFSET)
        {
            Globals.spriteBatch.Draw(canvas, new Rectangle( (int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
        }
    }
}
