using System;
using Kolibri.Engine;
using Kolibri.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Kolibri.Source.Workspace;


namespace Kolibri.Source.Workspace.UIElements
{
    public class Textfield : UIElement
    {
        public string content, lastChar;
        public bool numberField;
        private float width, txtHeight;
        private bool selected;
        public Color color;
        public Textfield(Window WINDOW, Vector2 POS, Vector2 DIM , string CONTENT) :base(WINDOW, POS, DIM)    
        {      
            content = CONTENT;
            dim = DIM;
            txtHeight = Globals.font.MeasureString("A").Y;
            color = new Color(100, 100, 100);
        }

        public override void Update(Vector2 OFFSET)
        {
            width = Globals.font.MeasureString(content).X * Globals.fontSize.X;

            if (Globals.mouse.LeftClick())
            {
                if(Clicked() && !selected)   //set selected mode
                {
                    color = new Color(140, 140, 140);
                    selected = true;
                }
                else
                {
                    color = new Color(100, 100, 100);
                    selected = false;
                }
            }
            
            if(Globals.keyboard.pressedKeys.Count > 0)  //add character
            {
                if(Globals.keyboard.pressedKeys[0].print != lastChar && width < dim.X - 0.1f && selected)
                {
                    int number;
                    if(!(numberField && !Int32.TryParse(Globals.keyboard.pressedKeys[0].print, out number)))
                    {
                        lastChar = Globals.keyboard.pressedKeys[0].print;
                        content += lastChar;
                    }
                }
            }
            else
            {
                lastChar = null;
            }

            if (!Globals.keyboard.oldKeyboard.IsKeyDown(Keys.Back) && Globals.keyboard.newKeyboard.IsKeyDown(Keys.Back) && content.Length >= 1 && selected) //remove character
            {
                 content = content.Remove(content.Length-1);
            }

            Console.WriteLine(content);
            base.Update(OFFSET);
        }
        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
            //field
            Globals.primitives.DrawRect(pos,dim, color);
            //label        
            Globals.primitives.DrawTxt(content.ToLower(), new Vector2(pos.X + 5, pos.Y + dim.Y / 2 - txtHeight / 4), Globals.fontSize, new Color(245, 255, 250));
        } 


    }
    
}