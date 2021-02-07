using System;
using Kolibri.Engine;
using Microsoft.Xna.Framework;
using Kolibri.Source.Workspace;

namespace Kolibri.Source.Workspace.UIElements
{
    public class Slider : ESprite2d
    {
  
        private string label;
        private Window window;
        public Vector2  relativePosMarker, posMarker, relativePos;
        public Color color;
        
        public int start, end, steplength;
        public Slider(int START, int END, int STEPLENGTH, Window WINDOW, Vector2 POS, Vector2 DIM, string LABEL) :base("Square", POS, DIM)
        {
            start = START;
            end = END;
            steplength = STEPLENGTH;
            label=LABEL;
            window = WINDOW;
            relativePos = POS;
            color = new Color(100, 100, 100);
            posMarker = relativePos + window.pos;
           
        }

       public override void Update(Vector2 OFFSET)
       {
           pos = relativePos + window.pos;
           posMarker.Y = pos.Y;
            

           if(posMarker.X > pos.X||posMarker.X < pos.X+dim.X)
           {
              if (Globals.mouse.LeftClickHold()&&Globals.GetBoxOverlap(posMarker, new Vector2(5,7), Globals.mouse.newMousePos, Vector2.Zero))
              {
                posMarker.X= Globals.mouse.newMousePos.X;
              };
           }
           base.Update(OFFSET);
       }

       public override void Draw(Vector2 OFFSET)
       {
            base.Draw(OFFSET);
            Globals.primitives.DrawTxt(Convert.ToString(start), new Vector2(pos.X, pos.Y+15),new Vector2(0.4f, 0.4f), new Color(245,255,250));
            Globals.primitives.DrawTxt(Convert.ToString(end), new Vector2(pos.X + dim.X, pos.Y+15),new Vector2(0.4f, 0.4f), new Color(245,255,250));
            
            for (int i = 0; i < end/steplength+1; i++) 
            {
                Globals.primitives.DrawRect(new Vector2(pos.X+(dim.X/(end/steplength))*i,pos.Y),new Vector2 (3, 8),color);

            }

            Globals.primitives.DrawTxt(label, new Vector2 (pos.X+dim.X +20, pos.Y-5),new Vector2(0.4f, 0.4f), new Color(245,255,250));
            
            Globals.primitives.DrawRect(pos, new Vector2(dim.X,3), color);

            //Marker
            Globals.primitives.DrawRect(posMarker, new Vector2(5,7), new Color(160,210,20));
            
       } 

    }


}