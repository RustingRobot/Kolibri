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
    public class EKeyboard //is linked to Globals!
    {
        public KeyboardState newKeyboard, oldKeyboard;
        public List<EKey> pressedKeys = new List<EKey>(), previousPressedKeys = new List<EKey>();

        public EKeyboard()
        {

        }

        public virtual void Update()
        {
            newKeyboard = Keyboard.GetState();
            GetPressedKeys();
        }

        public void LateUpdate()
        {
            oldKeyboard = newKeyboard;

            previousPressedKeys = new List<EKey>();
            for (int i = 0; i < pressedKeys.Count; i++)
            {
                previousPressedKeys.Add(pressedKeys[i]);
            }
        }

        public bool GetPress(string KEY)
        {
            for (int i = 0; i < pressedKeys.Count; i++)
            {
                if(pressedKeys[i].key == KEY)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void GetPressedKeys()
        {
            bool found = false;

            pressedKeys.Clear();
            for (int i = 0; i < newKeyboard.GetPressedKeys().Length; i++)
            {
                pressedKeys.Add(new EKey(newKeyboard.GetPressedKeys()[i].ToString(), 1));
            }
        }
    }
}
