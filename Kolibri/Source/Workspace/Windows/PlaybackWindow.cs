using Kolibri.Engine;
using Kolibri.Source.Workspace.UIElements;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Kolibri.Source.Workspace.Windows
{
    class PlaybackWindow : Window
    {
        Button playBtn, backBtn;
        Textfield fpsTxt, startFrameTxt, endFrameTxt;
        public int startFrame, endFrame;
        bool playing;
        ETimer timer;
        TimelineWindow timelineWindow;

        public PlaybackWindow(Vector2 POS, Vector2 DIM) : base(POS, DIM, "Playback")
        {
            playBtn = new Button(play, this, new Vector2(10, 25), new Vector2(40, 25), "play");
            backBtn = new Button(back, this, new Vector2(55, 25), new Vector2(40, 25), "back");
            fpsTxt = new Textfield(this, new Vector2(38, 55), new Vector2(57, 25), "1") { defaultContent = "0" };
            startFrameTxt = new Textfield(this, new Vector2(200, 25), new Vector2(57, 25), "") { defaultContent = "0" };
            endFrameTxt = new Textfield(this, new Vector2(200, 55), new Vector2(57, 25), "") { defaultContent = "0" };
            startFrameTxt.numberField = true;
            endFrameTxt.numberField = true;
            fpsTxt.numberField = true;
            timer = new ETimer(1200);
        }

        public override void Update(Vector2 OFFSET)
        {
            if (timelineWindow == null)
            {
                timelineWindow = (TimelineWindow)ObjManager.Windows.Find(x => x.GetType().Name == "TimelineWindow");
                timelineWindow.pbWindow = this;
            }
            else
            {
                if (timer.Test() && playing && fpsTxt.content != "0")
                {
                    if (timelineWindow.timeline.currentFrame < endFrame)
                        timelineWindow.timeline.nextFrame();
                    else
                        back();
                    timer.ResetToZero();
                    if (fpsTxt.content == "") timer.Ms = 1000;
                    else timer.Ms = 1000 / int.Parse( fpsTxt.content);
                }
                timer.UpdateTimer();
            }
            base.Update(OFFSET);
            playBtn.Update(OFFSET);
            backBtn.Update(OFFSET);
            fpsTxt.Update(OFFSET);
            startFrameTxt.Update(OFFSET);
            endFrameTxt.Update(OFFSET);
            if (startFrameTxt.content != "") startFrame = int.Parse(startFrameTxt.content);
            else startFrame = 0;
            if (endFrameTxt.content != "") endFrame = int.Parse(endFrameTxt.content);
            if (endFrame > 20) { endFrame = 20; endFrameTxt.content = "20"; }
        }
        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            beginWindowContent();
            playBtn.Draw(OFFSET);
            backBtn.Draw(OFFSET);
            Globals.primitives.DrawTxt("fps:", new Vector2(10, 58) + pos, Globals.fontSize, Color.Gray);
            Globals.primitives.DrawTxt("start frame:", new Vector2(116, 28) + pos, Globals.fontSize, Color.Gray);
            Globals.primitives.DrawTxt("end frame:", new Vector2(116, 58) + pos, Globals.fontSize, Color.Gray);
            fpsTxt.Draw(OFFSET);
            startFrameTxt.Draw(OFFSET);
            endFrameTxt.Draw(OFFSET);
            endWindowContent();
        }

        public void play()
        {
            playing = !playing;
            playBtn.label = (playing) ? "stop" : "play";
        }

        public void back()
        {
            timelineWindow.timeline.gotoFrame(startFrame);
        }
    }
}
