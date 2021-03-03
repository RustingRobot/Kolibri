using Kolibri.Engine;
using Kolibri.Source.Workspace;
using Kolibri.Source.Workspace.UIElements;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kolibri.Source.Workspace.Windows
{
    class TimelineWindow : Window
    {
        Frame frame1, frame2, frame3, frame4, frame5;
        Button b1, b2, b3, b4, b5;

        Button left, right;
        public Timeline timeline;
        public TimelineWindow( Vector2 POS, Vector2 DIM) : base(POS, DIM, "Timeline")
        {   
            timeline = new Timeline();

            frame3 = timeline.actualFrame;
            frame2 = timeline.frames[timeline.frames.IndexOf(timeline.actualFrame)-1];
            frame1 = timeline.frames[timeline.frames.IndexOf(frame2)-1];
            frame4 = timeline.frames[timeline.frames.IndexOf(timeline.actualFrame)+1];
            frame5 = timeline.frames[timeline.frames.IndexOf(frame4)+1];

            b1 = new Button(click1,this, new Vector2(30,25), new Vector2(10,10), "");
            b2 = new Button(click2,this, new Vector2(80,25), new Vector2(10,10), "");
            b3 = new Button(click3,this, new Vector2(130,25), new Vector2(10,10), "x");
            b4 = new Button(click4,this, new Vector2(180,25), new Vector2(10,10), "");
            b5 = new Button(click5,this, new Vector2(230,25), new Vector2(10,10), "");

            left = new Button(goLeft, this, new Vector2(10, 25), new Vector2(10,10), "Left");
            right = new Button(goRight, this, new Vector2(240, 25), new Vector2(10,10), "Right");

        }
        
        public void goLeft()
        {
            if(timeline.frames[timeline.frames.IndexOf(timeline.actualFrame)-1] != null)
            {
                frame3 = timeline.frames[timeline.frames.IndexOf(timeline.actualFrame)-1];
            }
            
        }

        public void goRight()
        {
            if(timeline.frames[timeline.frames.Count] != null)
            {
                frame3 = timeline.frames[timeline.frames.IndexOf(timeline.actualFrame)+1];
            }
            
        }
        public void click1()
        {
            canvas.pixels = frame1.pixels;
            timeline.actualFrame = frame1;
        }
        public void click2()
        {
            canvas.pixels = frame2.pixels;
            timeline.actualFrame = frame2;
        }

        public void click3()
        {
            canvas.pixels = frame3.pixels;
            timeline.actualFrame = frame3;
        }

        public void click4()
        {
            canvas.pixels = frame4.pixels;
            timeline.actualFrame = frame4;
        }
        public void click5()
        {
            canvas.pixels = frame5.pixels;
            timeline.actualFrame = frame5;
        }
        public override void Update(Vector2 OFFSET)
        {
            frame2 = timeline.frames[timeline.frames.IndexOf(timeline.actualFrame)-1];
            frame1 = timeline.frames[timeline.frames.IndexOf(frame2)-1];
            frame4 = timeline.frames[timeline.frames.IndexOf(timeline.actualFrame)+1];
            frame5 = timeline.frames[timeline.frames.IndexOf(frame4)+1];
            base.Update(OFFSET);
            left.Update(OFFSET);
            right.Update(OFFSET);
            b1.Update(OFFSET);
            b2.Update(OFFSET);
            b3.Update(OFFSET);
            b4.Update(OFFSET);
            b5.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            beginWindowContent();
            Globals.primitives.DrawRect(new Vector2(25, 25),new Vector2(210, 4), new Color(72,209,204));
            left.Draw(OFFSET);
            right.Draw(OFFSET);
            b1.Draw(OFFSET);
            b2.Draw(OFFSET);
            b3.Draw(OFFSET);
            b4.Draw(OFFSET);
            b5.Draw(OFFSET);
            endWindowContent();
        }
    }
}