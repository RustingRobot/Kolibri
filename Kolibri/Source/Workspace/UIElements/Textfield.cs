using System;
using Kolibri.Engine;
using Kolibri.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Kolibri.Source.Workspace;


namespace Kolibri.Source.Workspace.UIElements
{
    public class Textfield : ESprite2d
    {
        public delegate void Del();
        private string content;
        private Double maxchar;
        private string title;
        private Vector2 dim;
        private Window window;

        
       public Textfield(Window WINDOW, Vector2 POS, Vector2 DIM , string TITLE) :base("Square", POS, DIM)    
        {
            window = WINDOW;
            title=TITLE;
            pos = POS+window.pos;       
            content = "";
            dim = DIM;
            Double value = Convert.ToDouble(Convert.ToInt32(dim.X) /8);
            maxchar = Math.Round(value);

        }
    

    private string lastChar;

        private float width;

        public override void Update(Vector2 OFFSET)
       {
           width = Globals.font.MeasureString(content).X*0.3f;
          
           if(dim.X-0.8f >= width)
            {
                if(Globals.keyboard.pressedKeys.Count>0)
                {
                    // lastChar = Globals.keyboard.pressedKeys[Globals.keyboard.pressedKeys.Count-1].print;

                    if(Globals.keyboard.pressedKeys[0].print != lastChar)
                    {
                        lastChar= Globals.keyboard.pressedKeys[0].print;
                        content += lastChar;
                    }    
                    else
                    {
                        lastChar=null;
                    }
                }
            }
           

           if (Globals.keyboard.newKeyboard.IsKeyDown(Keys.Back)&&content.Length>=1)
            {
                content = content.Remove(content.Length-1);
            }

            

        
           
           
           Console.WriteLine(content);
           base.Update(OFFSET);
       }
       public override void Draw(Vector2 OFFSET, Color COLOR)
       {
            base.Draw(OFFSET, COLOR);
            //field
            Globals.primitives.DrawRect(pos,dim,new Color(100,100,100));
            //label
           // Globals.primitives.DrawTxt(content, new Vector2(pos.X + 8, pos.Y + 2), new Vector2(0.6f, 0.6f), new Color(39, 40, 48));
               
            Globals.primitives.DrawTxt(content, pos, new Vector2(0.3f, 0.3f), new Color(39,44,48));   
            
       } 


    }
    
}