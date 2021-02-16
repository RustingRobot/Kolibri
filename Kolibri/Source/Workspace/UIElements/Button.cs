using System;
using Kolibri.Engine;
using Microsoft.Xna.Framework;
using Kolibri.Source.Workspace;

namespace Kolibri.Source.Workspace.UIElements
{
    public class Button : UIElement
    {
  
        private string label;
        
        private bool ButtonClicked;
        public delegate void Event();
        public Event ClickEvent;
        public Vector2 strSize, relativePos;
        public Color color;
       public Button(Event CLICKEVENT, Window WINDOW, Vector2 POS, Vector2 DIM, string LABEL) :base(WINDOW, POS, DIM)
        {
            ClickEvent = CLICKEVENT;
            label=LABEL;
            window = WINDOW;
            strSize = Globals.font.MeasureString(label) * Globals.fontSize.X;
            color = new Color(100, 100, 100);
        }

       public override void Update(Vector2 OFFSET)
       {
            if (Globals.mouse.LeftClick()&&Globals.GetBoxOverlap(pos, dim, Globals.mouse.newMousePos, Vector2.Zero))
            {
                ClickEvent();   //let instantiator decide what function gets called
            };
           base.Update(OFFSET);
       }

       public override void Draw(Vector2 OFFSET)
       {
            base.Draw(OFFSET);
            //field
            Globals.primitives.DrawRect(pos,dim, color);
            //label
            Globals.primitives.DrawTxt(label, new Vector2(pos.X + dim.X / 2 - strSize.X / 2, pos.Y + dim.Y / 2 - strSize.Y / 2), Globals.fontSize, new Color(245,255,250));
           
       } 

    }


}