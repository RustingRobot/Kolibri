#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace Kolibri.Engine.Input
{
    public class EMouseControl
    {
        public bool dragging, rightDrag;
        public Vector2 newMousePos, oldMousePos, firstMousePos, newMouseAdjustedPos, systemCursorPos, screenLoc;
        public MouseState newMouse, oldMouse, firstMouse;

        public EMouseControl()
        {
            dragging = false;

            newMouse = Mouse.GetState();
            oldMouse = newMouse;
            firstMouse = newMouse;

            newMousePos = new Vector2(newMouse.Position.X, newMouse.Position.Y);
            oldMousePos = newMousePos;
            firstMousePos = newMousePos;

            GetMouseAndAdjust();
        }

        public void Update()
        {
            GetMouseAndAdjust();

            if(newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                firstMouse = newMouse;
                firstMousePos = newMousePos = GetScreenPos(firstMouse);
            }
        }

        public void LateUpdate()
        {
            oldMouse = newMouse;
            oldMousePos = GetScreenPos(oldMouse);
        }

        public virtual float GetDistanceFromClick()
        {
            return Globals.GetDistance(newMousePos, firstMousePos);
        }

        public virtual void GetMouseAndAdjust()
        {
            newMouse = Mouse.GetState();
            newMousePos = GetScreenPos(newMouse);
        }

        public int GetMouseweelChange()
        {
            return newMouse.ScrollWheelValue - oldMouse.ScrollWheelValue;
        }

        public Vector2 GetScreenPos(MouseState MOUSE)
        {
            return new Vector2(MOUSE.Position.X, MOUSE.Position.Y);
        }

        public virtual bool LeftClick()
        {
            if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
            {
                return true;
            }
            return false;
        }

        public virtual bool LeftClickHold()
        {
            bool holding = false;
            if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
            {
                holding = true;

                if (Math.Abs(newMouse.Position.X - firstMouse.Position.X) > 8 || Math.Abs(newMouse.Position.Y - firstMouse.Position.Y) > 8)
                {
                    dragging = true;
                }
            }
            return holding;
        }

        public virtual bool LeftClickRelease()
        {
            if (newMouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed)
            {
                dragging = false;
                return true;
            }
            return false;
        }

        public virtual bool RightClick()
        {
            if (newMouse.RightButton == ButtonState.Pressed && oldMouse.RightButton != ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
            {
                return true;
            }
            return false;
        }

        public virtual bool RightClickHold()
        {
            bool holding = false;
            if (newMouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
            {
                holding = true;

                if (Math.Abs(newMouse.Position.X - firstMouse.Position.X) > 8 || Math.Abs(newMouse.Position.Y - firstMouse.Position.Y) > 8)
                {
                    dragging = true;
                }
            }
            return holding;
        }

        public virtual bool RightClickRelease()
        {
            if (newMouse.RightButton == ButtonState.Released && oldMouse.RightButton == ButtonState.Pressed)
            {
                dragging = false;
                return true;
            }
            return false;
        }

        public void SetFirst()
        {

        }
    }
}
