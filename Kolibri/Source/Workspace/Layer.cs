using Kolibri.Engine;
using Kolibri.Engine.Input;
using Kolibri.Source;
using Kolibri.Source.Workspace;
using Kolibri.Source.Workspace.Windows;
using Kolibri.Source.Workspace.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Kolibri.Source.Workspace
{
    public class Layer: UIElement
    {
        public UInt32[] pixels;
        public Timeline timeline;

        string label;
        Vector2 pos;
        //Color color;
        int border = 2, index;

        public Layer(int LAYERINDEX,Window WINDOW, string LABEL, Vector2 POS): base(WINDOW, POS, new Vector2(10,25))
        {
           label = LABEL;
           pos = POS;
           timeline = new Timeline(LAYERINDEX, WINDOW);
        }

        public override void Update(Vector2 OFFSET)
        {

            base.Update(OFFSET);
            timeline.Update();
        }
        public override void Draw(Vector2 OFFSET)
        {
            
            timeline.Draw();
            Globals.primitives.DrawTxt(label, pos + window.pos,Globals.fontSize, Color.Gray);
            
            
        }

        
    }
}