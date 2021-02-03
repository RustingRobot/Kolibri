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
    public class Menu : Window
    {
        private Vector2 posb;
        private Vector2 dimbutton; 
        public List<Button> menuitems = new List<Button>();
        
        public void openlistdatei(){
            //Globals.primitives.DrawTxt("funktioniert",pos,dim,new Color(100,100,100));
        }
    
        public void openlistbearbeiten(){

        }

        public void openlisthelp(){

        }
      
        public virtual void AddMenuitem(object INFO)
        {
            menuitems.Add((Button)INFO);
        }

       public Menu(Vector2 POS, Vector2 DIM) :base(POS, DIM,"")    
        {
            posb = pos+ new Vector2(30,4);
            AddMenuitem(new Button(openlistdatei, this, posb, new Vector2( Globals.font.MeasureString("Datei").X * 0.6f,20), "Datei"));
            AddMenuitem(new Button(openlistbearbeiten, this, posb+ new Vector2(Globals.font.MeasureString("Datei").X * 0.6f + 20f,0), new Vector2( Globals.font.MeasureString("Bearbeiten").X * 0.6f,20), "Bearbeiten"));
            AddMenuitem(new Button (openlisthelp, this, posb+ new Vector2(Globals.font.MeasureString("Datei").X * 0.6f + 20f + Globals.font.MeasureString("Bearbeiten").X * 0.6f + 20f,0), new Vector2( Globals.font.MeasureString("Help").X * 0.6f,20), "Help"));
        }
    
       public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
            for (int i = 0; i < menuitems.Count; i++) 
            {
                menuitems[i].Update(OFFSET);
            }
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            for (int i = 0; i < menuitems.Count; i++) 
            {
                menuitems[i].Draw(OFFSET, COLOR);
            }
    
        }


    }
    
}