using System;
using Kolibri.Engine;
using Kolibri.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Kolibri.Source.Workspace;
using Kolibri.Source.Workspace.UIElements;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text;


namespace Kolibri.Source.Workspace.UIElements
{
    public class Menu : ESprite2d
    {
        private Vector2 posb;
        public List<Button> menuitems = new List<Button>();

        public List<Button> menusubitems = new List<Button>();
    
        private bool visible = false;
        public void openlistdatei(){
            visible = true;
            AddMenusubitem(new Button(openspeichern,null, posb + new Vector2(0,30),new Vector2(150,30),"Speichern"));
        }
    
        public void openlistbearbeiten(){
    
        }

        public void openspeichern(){

        }

        public void openlisthelp(){

        }
      
        public virtual void AddMenuitem(object INFO)
        {
            menuitems.Add((Button)INFO);
        }
        public virtual void AddMenusubitem(object INFO)
        {
            menusubitems.Add((Button)INFO);
        }

        public void cleanScreen(){
            for (int i = 0; i < menusubitems.Count; i++) 
            {
                menusubitems.Remove(menusubitems[i]);
            }
        }

       public Menu(Vector2 POS, Vector2 DIM) :base("square", POS,DIM)    
        {
            posb = pos+ new Vector2(30,4);
            AddMenuitem(new Button(openlistdatei, null, posb, new Vector2( Globals.font.MeasureString("Datei").X * 0.6f,20), "Datei"));
            Console.WriteLine("hello");
            AddMenuitem(new Button(openlistbearbeiten, null, posb+ new Vector2(Globals.font.MeasureString("Datei").X * 0.6f + 20f,0), new Vector2( Globals.font.MeasureString("Bearbeiten").X * 0.6f,20), "Bearbeiten"));
            AddMenuitem(new Button (openlisthelp, null, posb+ new Vector2(Globals.font.MeasureString("Datei").X * 0.6f + 20f + Globals.font.MeasureString("Bearbeiten").X * 0.6f + 20f,0), new Vector2( Globals.font.MeasureString("Help").X * 0.6f,20), "Help"));
        }
           
       public override void Update(Vector2 OFFSET)
        {
           /* if(visible||true && Globals.mouse.RightClick()||true&&Globals.GetBoxOverlap(posb + new Vector2(0,30), new Vector2(150,1*30), Globals.mouse.newMousePos, Vector2.Zero)||false)
            {
                cleanScreen();
            }*/


            base.Update(OFFSET);
            for (int i = 0; i < menuitems.Count; i++) 
            {
                menuitems[i].Update(OFFSET);
            }
            for (int i = 0; i < menusubitems.Count; i++) 
            {
                menusubitems[i].Update(OFFSET);
            }
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            for (int i = 0; i < menuitems.Count; i++) 
            {
                menuitems[i].Draw(OFFSET);
            }
            for (int i = 0; i < menusubitems.Count; i++) 
            {
                menusubitems[i].Draw(OFFSET);
            }
    
        }


    }
    
}