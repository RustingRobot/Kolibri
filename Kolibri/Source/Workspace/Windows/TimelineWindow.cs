using Kolibri.Engine;
using Kolibri.Source.Workspace;
using Kolibri.Source.Workspace.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Kolibri.Source.Workspace.Windows
{
    class TimelineWindow : Window
    {
        public Timeline timeline;
        public PlaybackWindow pbWindow;

        public List<Layer> layers = new List<Layer>();
        Button addLayerBtn;
        Button clearFrameBtn;
        Texture2D clearFrame;
        Layer layer1;
        int i; 
        public TimelineWindow( Vector2 POS, Vector2 DIM) : base(POS, DIM, "Timeline")
        {   
            //timeline = new Timeline(this);
            i=1;
            layer1 = new Layer((i-1),this, "Label 1", new Vector2(10, 58));
            layers.Add(layer1);
            i = 2;

            addLayerBtn = new Button(addLayer, this, new Vector2 (30, 24), new Vector2(18,18), "aL");

            clearFrameBtn = new Button(layer1.timeline.clearFrames, this, new Vector2(7, 24), new Vector2(18, 18), "");

            clearFrame = Globals.content.Load<Texture2D>("clearFrame");

            clearFrameBtn.normColor = Color.Transparent;
            clearFrameBtn.imgSize = new Vector2(0.6f, 0.6f);
            //clearFrameBtn.model = clearFrame;
        }

        public void addLayer()
        {
            layers.Add(new Layer((i-1),this, "Label " + i, new Vector2(10, 58+ 30*(i-1))));
            i = i+1;
        }
        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
            clearFrameBtn.Update(OFFSET);
            addLayerBtn.Update(OFFSET);
           // timeline.Update();
           for (int i = 0; i < layers.Count; i++)
            {
                layers[i].Update(OFFSET);
            }
           //layer1.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            beginWindowContent();
            clearFrameBtn.Draw(OFFSET);
            addLayerBtn.Draw(OFFSET);
            Globals.primitives.DrawLine(new Vector2(pos.X, pos.Y + 47), new Vector2(pos.X + dim.X, pos.Y + 47), 2, new Color(39, 44, 48));
           // timeline.Draw();
           for (int i = 0; i < layers.Count; i++)
            {
                layers[i].Draw(OFFSET);
            }
            //layer1.Draw(OFFSET);
            endWindowContent();
        }
    }
}