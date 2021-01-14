using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Kolibri.Engine;

namespace Kolibri.Engine
{
    public class ETimer
    {
        public bool ready;
        protected int ms;
        protected TimeSpan timer = new TimeSpan();

        public ETimer(int m)
        {
            ready = false;
            ms = m;
        }

        public ETimer(int m, bool STARTLOADED)
        {
            ready = STARTLOADED;
            ms = m;
        }

        public int Ms
        {
            get { return ms; }
            set { ms = value; }
        }

        public int Timer
        {
            get { return (int)timer.TotalMilliseconds; }
        }

        public void UpdateTimer()
        {
            timer += Globals.gameTime.ElapsedGameTime;
        }

        public void UpdateTimer(float SPEED)
        {
            timer += TimeSpan.FromTicks((long)(Globals.gameTime.ElapsedGameTime.Ticks * SPEED));
        }

        public virtual void AddToTimer(int MS)
        {
            timer += TimeSpan.FromMilliseconds((long)(MS));
        }

        public bool Test()
        {
            if(timer.TotalMilliseconds >= ms || ready)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            timer = timer.Subtract(new TimeSpan(0, 0, ms / 60000, ms / 1000, ms % 1000));
            if(timer.TotalMilliseconds < 0)
            {
                timer = TimeSpan.Zero;
            }
            ready = false;
        }

        public void Restet(int NEWTIMER)
        {
            timer = TimeSpan.Zero;
            Ms = NEWTIMER;
            ready = false;
        }

        public void ResetToZero()
        {
            timer = TimeSpan.Zero;
            ready = false;
        }

        public void SetTimer(TimeSpan TIME)
        {
            timer = TIME;
        }

        public virtual void SetTimer(int MS)
        {
            timer = TimeSpan.FromMilliseconds((long)(MS));
        }
    }
}
