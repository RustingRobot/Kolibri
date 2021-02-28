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
        static int border = 10;
        public static List<float> XConstraintValues = new List<float>(), YConstraintValues = new List<float>();
        public static Window[] WindowFraming = new Window[4]; //0 = Top; 1 = Bottom; 2 = Right; 3 = Left
        public static Window topWin, bottomWin, rightWin, leftWin;
        static DockSpace()
        {
            top = new DockIcon("top");
            bottom = new DockIcon("bottom");
            left = new DockIcon("left");
            right = new DockIcon("right");

            topWin = new Window(new Vector2(0, 24), new Vector2(Globals.screenWidth, 0), "");
            bottomWin = new Window(new Vector2(Globals.screenHeight, 0), new Vector2(Globals.screenWidth, 0), "");
            rightWin = new Window(new Vector2(Globals.screenWidth, 0), new Vector2(0, Globals.screenHeight), "");
            leftWin = new Window(new Vector2(0, 0), new Vector2(0, Globals.screenHeight), "");
            //Array.Fill(WindowFraming, new Window(Vector2.Zero, Vector2.Zero, "null"));


            WindowFraming[0] = topWin;
            WindowFraming[1] = bottomWin;
            WindowFraming[2] = rightWin;
            WindowFraming[3] = leftWin;

            XConstraintValues.Add(0); //left Border
            YConstraintValues.Add(24); //top Border
            XConstraintValues.Add(Globals.screenWidth); // right Border
            YConstraintValues.Add(Globals.screenHeight - 24); //bottom Border
        }

        public static void Update()
        {
            XConstraintValues[1] = Globals.screenWidth;
            YConstraintValues[1] = Globals.screenHeight - 24;
            top.pos = new Vector2((Globals.screenWidth - WindowFraming[2].dim.X - WindowFraming[3].dim.X) / 2 + top.dim.X / 2 + WindowFraming[3].dim.X, border + top.dim.Y + WindowFraming[0].dim.Y + WindowFraming[0].pos.Y);
            bottom.pos = new Vector2((Globals.screenWidth - WindowFraming[2].dim.X - WindowFraming[3].dim.X) / 2 - bottom.dim.X / 2 + WindowFraming[3].dim.X, Globals.screenHeight - bottom.dim.Y - border - WindowFraming[1].dim.Y);
            left.pos = new Vector2(border + left.dim.X / 2 + WindowFraming[3].dim.X, (Globals.screenHeight - WindowFraming[1].dim.Y - WindowFraming[0].dim.Y - WindowFraming[0].pos.Y) / 2 - left.dim.Y + WindowFraming[0].dim.Y + WindowFraming[0].pos.Y);
            right.pos = new Vector2(Globals.screenWidth - border - right.dim.X / 2 - WindowFraming[2].dim.X, (Globals.screenHeight - WindowFraming[1].dim.Y - WindowFraming[0].dim.Y - WindowFraming[0].pos.Y) / 2 + right.dim.Y + border / 2 + WindowFraming[0].dim.Y + WindowFraming[0].pos.Y);
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
