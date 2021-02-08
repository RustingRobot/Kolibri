using Kolibri.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kolibri.Source.Workspace
{
    public class UIElement : ESprite2d
    {
        public Vector2 relativePos;
        public Window window;

        public UIElement(Window WINDOW, Vector2 POS, Vector2 DIM) : base("Square", POS, DIM)
        {
            window = WINDOW;
            relativePos = POS;
        }
        public override void Update(Vector2 OFFSET)
        {
            if(window != null) pos = relativePos + window.pos; //pos needs to be a combination of the windows position and an offset. It also has to be updated every frame
            base.Update(OFFSET);
        }
    }
}
