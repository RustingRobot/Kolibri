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

        Button clearFrameBtn;
        Texture2D clearFrame;

        public TimelineWindow( Vector2 POS, Vector2 DIM) : base(POS, DIM, "Timeline")
        {   
            timeline = new Timeline(this);
            clearFrameBtn = new Button(timeline.clearFrames, this, new Vector2(7, 24), new Vector2(18, 18), "");

            clearFrame = Globals.content.Load<Texture2D>("clearFrame");

            clearFrameBtn.normColor = Color.Transparent;
            clearFrameBtn.imgSize = new Vector2(0.6f, 0.6f);
            //clearFrameBtn.model = clearFrame;
        }

        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
            clearFrameBtn.Update(OFFSET);
            timeline.Update();
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            beginWindowContent();
            clearFrameBtn.Draw(OFFSET);
            Globals.primitives.DrawLine(new Vector2(pos.X, pos.Y + 47), new Vector2(pos.X + dim.X, pos.Y + 47), 2, new Color(39, 44, 48));
            timeline.Draw();
            endWindowContent();
        }
    }
}