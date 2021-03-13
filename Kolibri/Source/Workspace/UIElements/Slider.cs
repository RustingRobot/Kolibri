using System;
using Kolibri.Engine;
using Microsoft.Xna.Framework;
using Kolibri.Source.Workspace;
using System.Diagnostics;

namespace Kolibri.Source.Workspace.UIElements
{
    public class Slider : ESprite2d
    {
  
        private string label;
        private Window window;
        public Vector2   posMarker, relativePos;
        public Color color;
        
        public Button editBtn;
        Boolean toEdit;
        public int value;

        public int start, end, steplength;
        public Slider(int START, int END, int STEPLENGTH, Window WINDOW, Vector2 POS, Vector2 DIM, string LABEL) :base("Square", POS, DIM)
        {
            editBtn = new Button(changeColor, WINDOW, new Vector2 (pos.X+dim.X+15, pos.Y), new Vector2(30,18), LABEL);
            editBtn.color = new Color(178,0,0);
            toEdit = true;
            start = START;
            end = END;
            steplength = STEPLENGTH;
            //label=LABEL;
            window = WINDOW;
            relativePos = POS;
            color = new Color(100, 100, 100);
            posMarker = relativePos + window.pos;
           
        }
        public void changeColor()
        {
            editBtn.normColor = (toEdit)? new Color(0, 128, 0) : new Color(178, 0, 0);
            toEdit = !toEdit;
        }

        public int getValue()
        {
            if(posMarker.X == pos.X)
            {
                value=-1;
            }
            else
            {
                int placeholder = Convert.ToInt32(posMarker.X);
                for(int i = 0; placeholder> pos.X;i++)
                {
                    placeholder = placeholder -  Convert.ToInt32(dim.X/((end-start))*steplength);
                    value=i;
                }
            }
            
            value = (value+1)*steplength;
            return value;
        }

       public override void Update(Vector2 OFFSET)
       {
           pos = relativePos + window.pos;
          // posMarker.Y = pos.Y;
           //posMarker.X = posMarker.X + window.pos.X;
           
          /* if(posMarker.X > pos.X||posMarker.X < pos.X+dim.X)
           {
              if (Globals.mouse.LeftClickHold()&&Globals.GetBoxOverlap(posMarker, new Vector2(5,7), Globals.mouse.newMousePos, Vector2.Zero))
              {
                posMarker.X= Globals.mouse.newMousePos.X;
              };
           }
            */

            if(toEdit == true)
            {
                editBtn.normColor = new Color(0,178,0);
            }
            if(toEdit == false)
            {
                editBtn.normColor = new Color(178,0,0);
            }

           if(Globals.keyboard.OnPress("Right")&&toEdit==true&&posMarker.X<pos.X+dim.X)
           {
               posMarker = new Vector2((dim.X/(end-start))*steplength, 0) + posMarker;
               Console.WriteLine("value: " + this.getValue());
           }
           if(Globals.keyboard.OnPress("Left")&&toEdit==true&&posMarker.X>pos.X)
           {
               posMarker = posMarker - new Vector2((dim.X/((end-start))*steplength), 0) ;
           }
           editBtn.Update(OFFSET);
           base.Update(OFFSET);

       }

       public override void Draw(Vector2 OFFSET)
       {
            base.Draw(OFFSET);
            editBtn.Draw(OFFSET);
            Globals.primitives.DrawTxt(Convert.ToString(start), new Vector2(pos.X, pos.Y+15), new Vector2(Globals.fontSize.X - 0.1f, Globals.fontSize.X - 0.1f), new Color(245,255,250));
            Globals.primitives.DrawTxt(Convert.ToString(end), new Vector2(pos.X + dim.X, pos.Y+15), new Vector2(Globals.fontSize.X - 0.1f, Globals.fontSize.X - 0.1f), new Color(245,255,250));
            
            for (int i = 0; i < end/steplength+1; i++) 
            {
                Globals.primitives.DrawRect(new Vector2(pos.X+(dim.X/(end-start/steplength))*i,pos.Y),new Vector2 (3, 8),color);

            }

            //Globals.primitives.DrawTxt(label, new Vector2 (pos.X+dim.X +20, pos.Y-5),new Vector2(Globals.fontSize.X - 0.1f, Globals.fontSize.X - 0.1f), new Color(245,255,250));
            
            Globals.primitives.DrawRect(pos, new Vector2(dim.X,3), color);

            //Marker
            Globals.primitives.DrawRect(posMarker, new Vector2(5,7), new Color(160,210,20));
            
       } 

    }


}