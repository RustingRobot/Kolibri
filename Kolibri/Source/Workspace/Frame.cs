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
        UInt32[] pixels;

        public Frame(Window WINDOW): base(WINDOW, Vector2.Zero, new Vector2(15, 25))
        {
            pixels = new uint[(int)Globals.canvas.dim.X*(int)Globals.canvas.dim.Y];
            /*wenn ein Frame hinzugefügt wird, ist der erstmal Weiß, 
            dann kann man ja wenn man die Timeline hat, sich in einem 
            bestimmten Frame befindet(also der aktuelle), und auf 
            einen Button klickt der dann die Pixels vom Canvas auf den 
            aktuellen Frame kopiert*/
            for (int i = 0; i < pixels.Length; i++) 
            {
                pixels[i] = 0xFFFFFF00;
            }
        }

        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
        }
        public override void Draw(Vector2 OFFSET)
        {
            Globals.primitives.DrawRect(OFFSET + window.pos, dim, Color.Gray);
            Debug.WriteLine("hi");
        }
    }
}