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
        public Textfield nameField;
        public string label;
        public int layerIndex;
        public List<Frame> Frames = new List<Frame>();
        public Timeline timeline;

        public Layer(int LAYERINDEX, Timeline TIMELINE, Window WINDOW): base(WINDOW, Vector2.Zero, new Vector2(15, 25))
        {
            layerIndex=LAYERINDEX;
            timeline = TIMELINE;
            label = "Layer "+ layerIndex;
            nameField = new Textfield(WINDOW, new Vector2(6, 55 + 27 * layerIndex), new Vector2(70, 25), label);
        }

        
        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
            while (Frames.Count * 17 + 70 < window.dim.X)
            {
                Frames.Add(new Frame(this, window, timeline));
            }

            for (int i = 0; i < Frames.Count; i++)
            {
                Frames[i].Update(new Vector2(OFFSET.X + i * 17 + 80, OFFSET.Y + layerIndex * 27 + 55));
            }
            nameField.Update(OFFSET);
        }
        public override void Draw(Vector2 OFFSET)
        {
            for (int i = 0; i < Frames.Count; i++)
            {
                Frames[i].Draw(new Vector2(OFFSET.X + i * 17 + 80, OFFSET.Y + layerIndex * 27 + 55));
            }
            nameField.Draw(OFFSET);
            base.Draw(OFFSET);
            
        }

        public void moveUp() //don't let the layerIndex get messed up if a layer over this one gets deleted
        {
            layerIndex--;
            nameField.relativePos.Y -= 27;
        }
    }
}