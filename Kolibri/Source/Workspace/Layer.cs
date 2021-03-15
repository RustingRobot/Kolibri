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
       
        public Boolean hidden;
        public Boolean currentLayer;

        Vector2 pos;
        public Button labelBtn;
        public string label;
        public int layerIndex;

        public Layer(int LAYERINDEX, Window WINDOW, string LABEL, Vector2 POS): base(WINDOW, Vector2.Zero, new Vector2(15, 25))
        {
            layerIndex=LAYERINDEX;
            window = WINDOW;
            label = "Layer "+layerIndex;
            labelBtn = new Button(hideLayer, WINDOW, new Vector2(6,58+30*layerIndex),new Vector2(55,18),LABEL);
            labelBtn.color = new Color(33, 33, 33);
            pos = POS; 
            timeline = new Timeline(this,layerIndex, WINDOW);
            currentLayer = true;
            hidden = false;
        }

        public void hideLayer()
        {
            if(hidden==false)
            {
                hidden = true; 
            }
            else
            {
                hidden = false;
            }
             
        }

        
        public override void Update(Vector2 OFFSET)
        {
            label = "Layer "+layerIndex;
            labelBtn = new Button(hideLayer, window, new Vector2(6,58+30*layerIndex),new Vector2(55,18),label);
         
            if(hidden==true)
            {
                labelBtn.normColor = new Color(176, 176, 176);
            }
            if(hidden == false)
            {
                labelBtn.normColor = new Color(33, 33, 33);
            }
            pos = OFFSET + window.pos;
            base.Update(OFFSET);
            labelBtn.Update(OFFSET);
            timeline.Update();
        }
        public override void Draw(Vector2 OFFSET)
        {

            labelBtn.Draw(OFFSET);
            timeline.Draw();
            //Globals.primitives.DrawTxt(labelString, pos + window.pos,Globals.fontSize, Color.Gray);
           base.Draw(OFFSET);
            
        }

        
    }
}