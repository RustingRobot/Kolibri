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
        static DockSpace()
        {
            top = new DockIcon("top");
            bottom = new DockIcon("bottom");
            left = new DockIcon("left");
            right = new DockIcon("right");

            XConstraintValues.Add(0); //left Border
            YConstraintValues.Add(24); //top Border
            XConstraintValues.Add(Globals.screenWidth); // right Border
            YConstraintValues.Add(Globals.screenHeight - 24); //bottom Border
        }

        public static void Update()
        {
            XConstraintValues[1] = Globals.screenWidth;
            YConstraintValues[1] = Globals.screenHeight - 24;
            top.pos = new Vector2(Globals.screenWidth / 2 + top.dim.X / 2, border + 24 + top.dim.Y);
            bottom.pos = new Vector2(Globals.screenWidth / 2 - bottom.dim.X / 2, Globals.screenHeight - bottom.dim.Y - border);
            left.pos = new Vector2(border + left.dim.X / 2, Globals.screenHeight / 2 - left.dim.Y);
            right.pos = new Vector2(Globals.screenWidth - border - right.dim.X / 2, Globals.screenHeight / 2 + right.dim.Y + border / 2);
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
