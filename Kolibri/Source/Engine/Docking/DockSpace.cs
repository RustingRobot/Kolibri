using Kolibri.Engine;
using Kolibri.Source.Workspace.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kolibri.Source.Workspace
{
    public static class DockSpace
    {
        static DockIcon top, bottom, left, right;
        static int iconBorder = 10, windowBorder = 3;
        public static Window[] WindowFraming = new Window[4]; //0 = Top; 1 = Bottom; 2 = Right; 3 = Left
        public static Window topWin, bottomWin, rightWin, leftWin;
        static DockSpace()
        {
            top = new DockIcon("top");
            bottom = new DockIcon("bottom");
            left = new DockIcon("left");
            right = new DockIcon("right");

            topWin = new Window(new Vector2(windowBorder, 24), new Vector2(Globals.screenWidth, 0), "");
            bottomWin = new Window(new Vector2(windowBorder, Globals.screenHeight - windowBorder), new Vector2(Globals.screenWidth, 0), "");
            rightWin = new Window(new Vector2(Globals.screenWidth - windowBorder, 0), new Vector2(0, Globals.screenHeight), "");
            leftWin = new Window(new Vector2(windowBorder, 0), new Vector2(0, Globals.screenHeight), "");

            WindowFraming[0] = topWin;
            WindowFraming[1] = bottomWin;
            WindowFraming[2] = rightWin;
            WindowFraming[3] = leftWin;
        }

        public static void Update()
        {
            topWin.dim.X = Globals.screenWidth;
            bottomWin.pos.Y = Globals.screenHeight - windowBorder;
            bottomWin.dim.X = Globals.screenWidth;
            rightWin.pos.X = Globals.screenWidth - windowBorder;
            rightWin.dim.Y = Globals.screenHeight;
            leftWin.dim.Y = Globals.screenHeight;

            top.pos = new Vector2((Globals.screenWidth - (Globals.screenWidth - WindowFraming[2].pos.X) - WindowFraming[3].dim.X + WindowFraming[3].pos.X) / 2 + top.dim.X / 2 + WindowFraming[3].dim.X, iconBorder + top.dim.Y + WindowFraming[0].dim.Y + WindowFraming[0].pos.Y);
            bottom.pos = new Vector2((Globals.screenWidth - (Globals.screenWidth - WindowFraming[2].pos.X) - WindowFraming[3].dim.X + WindowFraming[3].pos.X) / 2 - bottom.dim.X / 2 + WindowFraming[3].dim.X, Globals.screenHeight - (Globals.screenHeight - WindowFraming[1].pos.Y) - bottom.dim.Y - iconBorder);
            left.pos = new Vector2(iconBorder + left.dim.X / 2 + WindowFraming[3].dim.X + WindowFraming[3].pos.X, (Globals.screenHeight - (Globals.screenHeight - WindowFraming[1].pos.Y) - WindowFraming[0].dim.Y - WindowFraming[0].pos.Y) / 2 - left.dim.Y + WindowFraming[0].dim.Y + WindowFraming[0].pos.Y);
            right.pos = new Vector2(Globals.screenWidth - iconBorder - right.dim.X / 2 - (Globals.screenWidth - WindowFraming[2].pos.X), (Globals.screenHeight - (Globals.screenHeight - WindowFraming[1].pos.Y) - WindowFraming[0].dim.Y - WindowFraming[0].pos.Y) / 2 + right.dim.Y + iconBorder / 2 + WindowFraming[0].dim.Y + WindowFraming[0].pos.Y);
            bottom.Update(Vector2.Zero);
            left.Update(Vector2.Zero);
            right.Update(Vector2.Zero);
            top.Update(Vector2.Zero);
        }

        public static void Draw()
        {
            bottom.Draw(Vector2.Zero);
            left.Draw(Vector2.Zero);
            right.Draw(Vector2.Zero);
            top.Draw(Vector2.Zero);
        }

        public static void applyDocking()
        {
            bottom.applyDocking();
            left.applyDocking();
            right.applyDocking();
            top.applyDocking();
        }
    }
}
