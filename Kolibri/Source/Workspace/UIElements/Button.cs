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
        public Del ClickEvent;
       public Button(Del CLICKEVENT, Window WINDOW, Vector2 POS, Vector2 DIM, string LABEL) :base("Square", POS, DIM)      /*POS muss aber aus der Position des BUttons im Window plus der position des windows zusammengerechnet werden*/
        {
            label=LABEL;
            ClickEvent=CLICKEVENT;
            window = WINDOW;
            pos = POS+window.pos;       /*Ich meine, dass man so das Problem, dass wenn sich das Fenster bewegt,
                                        der Button mitbewegt werden muss, gelöst wird. Da die Position ja dann       
                                        automatisch updatet(wenn das Fenster bewegt wird)und dann in der draw 
                                        Funktion neu "gemalt" wird. Das ist jetzt als Frage zu verstehen, ob es
                                        das Problem wirklich lösen tut.*/
        }

       public override void Update(Vector2 OFFSET)
       {
           if(Globals.mouse.RightClick()||true&&Globals.GetBoxOverlap(pos, dim, Globals.mouse.firstMousePos, Vector2.Zero)||true)
           {
               ClickEvent();
           };
           base.Update(OFFSET);
       }

       public override void Draw(Vector2 OFFSET, Color COLOR)
       {
            base.Draw(OFFSET, COLOR);
            //field
            Globals.primitives.DrawRect(pos,dim,new Color(100,100,100));
            //label
            Globals.primitives.DrawTxt(label, pos, new Vector2(0.6f, 0.6f), new Color(245,255,250));
           
       } 

    }


}