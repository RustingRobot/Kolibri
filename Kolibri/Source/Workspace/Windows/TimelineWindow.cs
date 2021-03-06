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
    public class TimelineWindow : Window
    {
        public Timeline timeline;
        public PlaybackWindow pbWindow;
        public bool osActive = false;
        public Label currentFrameTxt, currentLayerTxt;
        public Button addLayerBtn, deleteLayerBtn, clearFrameBtn, onionSkinBtn;
        public TimelineWindow( Vector2 POS, Vector2 DIM) : base(POS, DIM, "Timeline")
        {   
            timeline = new Timeline(this);

            addLayerBtn = new Button(timeline.AddLayer, this, new Vector2 (80, 24), new Vector2(18,18), "+");
            deleteLayerBtn = new Button(timeline.DeleteLayer, this, new Vector2(100, 24), new Vector2(18, 18), "-");
            clearFrameBtn = new Button(timeline.clearFrames, this, new Vector2(7, 24), new Vector2(70, 18), "Clear");
            onionSkinBtn = new Button(onionSkin, this, new Vector2(320, 24), new Vector2(24, 18), "os");
            currentFrameTxt = new Label(this, new Vector2(160, 32), Globals.fontSize, "Frame: ");
            currentLayerTxt = new Label(this, new Vector2(240, 32), Globals.fontSize, "Layer: ");

            clearFrameBtn.normColor = Color.Transparent;
            clearFrameBtn.imgSize = new Vector2(0.6f, 0.6f);
        }

        public void onionSkin()
        {
            osActive = !osActive;
            onionSkinBtn.normColor = (osActive) ? new Color(60, 104, 148) : new Color(100, 100, 100);
        }

        public override void Update(Vector2 OFFSET)
        {
             
            base.Update(OFFSET);
            clearFrameBtn.Update(OFFSET);
            addLayerBtn.Update(OFFSET);
            deleteLayerBtn.Update(OFFSET);
            onionSkinBtn.Update(OFFSET);
            timeline.Update(OFFSET);
            currentFrameTxt.label = $"Frame: {timeline.currentFrame}";
            currentLayerTxt.label = $"Layer: {timeline.currentLayer}";
            currentFrameTxt.Update(OFFSET);
            currentLayerTxt.Update(OFFSET);
            //timeline navigation without mouse
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
            clearFrameBtn.Draw(OFFSET);
            addLayerBtn.Draw(OFFSET);
            deleteLayerBtn.Draw(OFFSET);
            onionSkinBtn.Draw(OFFSET);
            currentFrameTxt.Draw(OFFSET);
            currentLayerTxt.Draw(OFFSET);
            Globals.primitives.DrawLine(new Vector2(pos.X, pos.Y + 47), new Vector2(pos.X + dim.X, pos.Y + 47), 3, new Color(39, 44, 48));
            timeline.Draw(Vector2.Zero);
            endWindowContent();
        }
    }
}