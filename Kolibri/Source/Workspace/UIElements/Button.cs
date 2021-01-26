using System;
using Kolibri.Engine;
using Microsoft.Xna.Framework;
using Kolibri.Source.Workspace;

namespace Kolibri.Source.Workspace.UIElements
{
    public class Button : ESprite2d
    {
        public delegate void Del();
        private string label;
        
        private bool ButtonClicked;
        private Window window;
        public delegate void Event();
        public static Event ClickEvent;
        public Vector2 strSize, relativePos;
        public Color color;
       public Button(Event CLICKEVENT, Window WINDOW, Vector2 POS, Vector2 DIM, string LABEL) :base("Square", POS, DIM)      /*POS muss aber aus der Position des BUttons im Window plus der position des windows zusammengerechnet werden*/
        {
            ClickEvent = CLICKEVENT;
            label=LABEL;
            window = WINDOW;
            relativePos = POS;
            strSize = Globals.font.MeasureString(label) * 0.6f;
            color = new Color(100, 100, 100);
        }

       public override void Update(Vector2 OFFSET)
       {
            pos = relativePos + window.pos;
            if (Globals.mouse.LeftClick()&&Globals.GetBoxOverlap(pos, dim, Globals.mouse.newMousePos, Vector2.Zero))
            {
                ClickEvent();
            };
           base.Update(OFFSET);
       }

       public override void Draw(Vector2 OFFSET)
       {
            base.Draw(OFFSET);
            //field
            Globals.primitives.DrawRect(pos,dim, color);
            //label
            Globals.primitives.DrawTxt(label, new Vector2(pos.X + dim.X / 2 - strSize.X / 2, pos.Y + dim.Y / 2 - strSize.Y / 2), new Vector2(0.6f, 0.6f), new Color(245,255,250));
           
       } 

    }


}