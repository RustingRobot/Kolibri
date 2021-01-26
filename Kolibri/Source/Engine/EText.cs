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
using Kolibri.Engine;
#endregion

namespace Kolibri.Engine
{
    public class EText //perhaps depricated
    {
        public Vector2 pos, dim, origin;
        public SpriteFont font;
        public string txt;
        public float rot;
        public Color color;

        public EText(string PATH, string TXT, Vector2 POS, Vector2 DIM, Color COLOR , float ROT = 0)
        {
            pos = POS;
            dim = DIM;
            txt = TXT;
            rot = ROT;
            color = COLOR;
            font = Globals.content.Load<SpriteFont>(PATH);
        }

        public virtual void Update(Vector2 OFFSET)
        {

        }

        public virtual void Draw(Vector2 OFFSET)
        {
            if (font != null)
                Globals.spriteBatch.DrawString(font, txt, pos, color, rot, origin, dim, new SpriteEffects(), 0);
        }

    }
}
