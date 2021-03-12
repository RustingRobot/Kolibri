using System;
using Kolibri.Engine;
using Microsoft.Xna.Framework;
using Kolibri.Source.Workspace;

namespace Kolibri.Source.Workspace.UIElements
{
    public class Label : UIElement
    {
        public string label;
        
        public Vector2 strSize;
        public Color color;
       public Label(Window WINDOW, Vector2 POS, Vector2 DIM, string LABEL) :base(WINDOW, POS, DIM)
        {
            label=LABEL;
            window = WINDOW;
            strSize = Globals.font.MeasureString(label) * Globals.fontSize.X;
            color = Color.Gray;
        }

       public override void Update(Vector2 OFFSET)
       {
           base.Update(OFFSET);
       }

       public override void Draw(Vector2 OFFSET)
       {        
            Globals.primitives.DrawTxt(label, new Vector2(pos.X + dim.X / 2 - strSize.X / 2, pos.Y + dim.Y / 2 - strSize.Y / 2), Globals.fontSize, color); 
       } 

    }


}