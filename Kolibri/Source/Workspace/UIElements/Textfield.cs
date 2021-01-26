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
        private string content, lastChar;
        private Vector2 dim, relativePos;
        private Window window;
        private float width, txtHeight;
        private bool selected;
        public Color color;
        public Textfield(Window WINDOW, Vector2 POS, Vector2 DIM , string CONTENT) :base("Square", POS, DIM)    
        {
            window = WINDOW;
            relativePos = POS;       
            content = CONTENT;
            dim = DIM;
            txtHeight = Globals.font.MeasureString("A").Y;
            color = new Color(100, 100, 100);
        }

        public override void Update(Vector2 OFFSET)
        {
            pos = relativePos + window.pos;
            width = Globals.font.MeasureString(content).X * 0.6f;

            if (Globals.mouse.LeftClick())
            {
                if(Globals.GetBoxOverlap(pos,dim,Globals.mouse.newMousePos, Vector2.Zero) && !selected)
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
            
            if(Globals.keyboard.pressedKeys.Count > 0)
            {
                if(Globals.keyboard.pressedKeys[0].print != lastChar && width < dim.X - 0.1f && selected)
                {
                    lastChar = Globals.keyboard.pressedKeys[0].print;
                    content += lastChar;
                }
            }
            else
            {
                lastChar = null;
            }

            if (!Globals.keyboard.oldKeyboard.IsKeyDown(Keys.Back) && Globals.keyboard.newKeyboard.IsKeyDown(Keys.Back) && content.Length >= 1 && selected)
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
            Globals.primitives.DrawTxt(content.ToLower(), new Vector2(pos.X + 5, pos.Y + dim.Y / 2 - txtHeight / 4), new Vector2(0.6f, 0.6f), new Color(245, 255, 250));
        } 


    }
    
}