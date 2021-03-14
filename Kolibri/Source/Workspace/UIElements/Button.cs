using System;
using Kolibri.Engine;
using Microsoft.Xna.Framework;
using Kolibri.Source.Workspace;

namespace Kolibri.Source.Workspace.UIElements
{
    public class Button : UIElement
    {
  
        public string label, option;
        public bool leftBound = false;
        public delegate void Event();
        public delegate void optionEvent(string s);
        public Event ClickEvent;
        public optionEvent ClickOptionEvent;
        public Vector2 strSize;
        public Color color, normColor, hoverColor, clickColor, txtColor;
       public Button(Event CLICKEVENT, Window WINDOW, Vector2 POS, Vector2 DIM, string LABEL) :base(WINDOW, POS, DIM)
       {
           ClickEvent = CLICKEVENT;
           label=LABEL;
           window = WINDOW;
           strSize = Globals.font.MeasureString(label) * Globals.fontSize.X;
           normColor = new Color(100, 100, 100);
           hoverColor = new Color(120, 120, 120);
           clickColor = new Color(150, 150, 150);
           txtColor = new Color(245, 255, 250);
       }

        public Button(optionEvent CLICKEVENT, string OPTION, Window WINDOW, Vector2 POS, Vector2 DIM, string LABEL) : base(WINDOW, POS, DIM)
        {
            ClickOptionEvent = CLICKEVENT;
            option = OPTION;
            label = LABEL;
            window = WINDOW;
            strSize = Globals.font.MeasureString(label) * Globals.fontSize.X;
            normColor = new Color(100, 100, 100);
            hoverColor = new Color(120, 120, 120);
            clickColor = new Color(150, 150, 150);
        }

        public override void Update(Vector2 OFFSET)
       {
            if (MouseHover())
            {
                if(!Globals.mouse.LeftClickHold()) color = hoverColor;
                if (Clicked())
                {
                     color = clickColor;
                    if (ClickOptionEvent == null)
                        ClickEvent();   //let instantiator decide what function gets called
                    else
                        ClickOptionEvent(option);
                }
            }
            else color = normColor;
  
            base.Update(OFFSET);
       }

       public override void Draw(Vector2 OFFSET)
       {
            //field
            Globals.primitives.DrawRect(pos,dim, color);
            //label
            if (leftBound)
                Globals.primitives.DrawTxt(label, new Vector2(pos.X + 5, pos.Y + dim.Y / 2 - strSize.Y / 2), Globals.fontSize, txtColor);
            else
                Globals.primitives.DrawTxt(label, new Vector2(pos.X + dim.X / 2 - strSize.X / 2, pos.Y + dim.Y / 2 - strSize.Y / 2), Globals.fontSize, txtColor);
            base.DrawCentered(OFFSET);
       } 

    }


}