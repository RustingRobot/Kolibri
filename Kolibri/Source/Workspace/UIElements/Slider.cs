using System;
using Kolibri.Engine;
using Microsoft.Xna.Framework;
using Kolibri.Source.Workspace;
using System.Diagnostics;

namespace Kolibri.Source.Workspace.UIElements
{
    public class Slider : ESprite2d
    {
        private Window window;
        public Vector2 posMarker, relativePos;
        public Color color;
        public int value = 0, handleWidth = 20;
        bool dragged = false;

        public int start, end, steplength;
        public Slider(int START, int END, int STEPLENGTH, Window WINDOW, Vector2 POS, Vector2 DIM, string LABEL) :base("Square", POS, DIM)
        {
            start = START;
            end = END;
            steplength = STEPLENGTH;
            window = WINDOW;
            relativePos = POS;
            color = new Color(100, 100, 100);
            posMarker = relativePos + window.pos;
           
        }

        public int getValue()
        {
            return value;
        }
        public void SetValue(int VALUE)
        {
            value = VALUE;
           
        }

       public override void Update(Vector2 OFFSET)
       {
            pos = relativePos + window.pos;
            posMarker.Y = pos.Y;
            posMarker.X = pos.X + (dim.X - handleWidth) / (end - start) * value;
            if (dragged || (Globals.GetBoxOverlap(posMarker, new Vector2(posMarker.X + handleWidth, dim.Y), Globals.mouse.newMousePos, Vector2.Zero) && Globals.mouse.LeftClickHold() && window.MouseInWindow()))
            {
                value = (int)(Globals.mouse.newMousePos.X - pos.X);
                value = Math.Clamp(value, start, end);
                dragged = true;
            }

            if (Globals.mouse.LeftClickRelease())
            {
                dragged = false;
            }

            base.Update(OFFSET);

       }

       public override void Draw(Vector2 OFFSET)
       {
            base.Draw(OFFSET);
            Globals.primitives.DrawRect(pos, new Vector2(dim.X,dim.Y), color);

            //Marker
            Globals.primitives.DrawRect(posMarker, new Vector2(handleWidth, dim.Y), new Color(39, 44, 48));
            Globals.primitives.DrawTxt(value.ToString(), pos + new Vector2(dim.X / 2 - (Globals.font.MeasureString(value.ToString()).X * Globals.fontSize.X) / 2, 0), Globals.fontSize * 0.9f, new Color(245, 255, 250));

        } 

    }


}