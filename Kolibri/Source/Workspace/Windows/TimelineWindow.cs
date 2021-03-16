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

        Button addLayerBtn, deleteLayerBtn;
        Button clearFrameBtn; 
        public TimelineWindow( Vector2 POS, Vector2 DIM) : base(POS, DIM, "Timeline")
        {   
            timeline = new Timeline(this);

            addLayerBtn = new Button(timeline.AddLayer, this, new Vector2 (72, 24), new Vector2(60,18), "add L");

            //clearFrameBtn = new Button(layer1.timeline.clearFrames, this, new Vector2(7, 24), new Vector2(60, 18), "Clear");

            deleteLayerBtn = new Button(timeline.DeleteLayer, this, new Vector2 (137, 24), new Vector2(60,18), "delete L");


            //clearFrameBtn.normColor = Color.Transparent;
            //clearFrameBtn.imgSize = new Vector2(0.6f, 0.6f);
            //clearFrameBtn.model = clearFrame;
        }

        public override void Update(Vector2 OFFSET)
        {
             
            base.Update(OFFSET);
            //clearFrameBtn.Update(OFFSET);
            addLayerBtn.Update(OFFSET);
            deleteLayerBtn.Update(OFFSET);
            timeline.Update(OFFSET);
            if (Globals.keyboard.OnPress("Up") && timeline.currentLayer > 0)
            {
                timeline.currentLayer--;
            }

            if (Globals.keyboard.OnPress("Down") && timeline.currentLayer < timeline.Layers.Count -1 )
            {
                timeline.currentLayer++;
            }

            if (Globals.keyboard.OnPress("Left") && timeline.currentFrame > 0)
            {
                timeline.previousFrame();
            }

            if (Globals.keyboard.OnPress("Right") && timeline.currentFrame < timeline.Layers[timeline.currentLayer].Frames.Count)
            {
                timeline.nextFrame();
            }
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            beginWindowContent();
            //clearFrameBtn.Draw(OFFSET);
            addLayerBtn.Draw(OFFSET);
            deleteLayerBtn.Draw(OFFSET);
            Globals.primitives.DrawLine(new Vector2(pos.X, pos.Y + 47), new Vector2(pos.X + dim.X, pos.Y + 47), 3, new Color(39, 44, 48));
            timeline.Draw(Vector2.Zero);
            endWindowContent();
        }
    }
}