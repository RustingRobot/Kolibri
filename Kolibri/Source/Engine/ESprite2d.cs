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
    public class ESprite2d
    {
        public float rot;
        public Vector2 pos, dim;
        public Texture2D model;

        public ESprite2d(string PATH, Vector2 POS, Vector2 DIM)
        {
            pos = POS;
            dim = DIM;

            model = Globals.content.Load<Texture2D>(PATH);
        }

        public virtual void Update(Vector2 OFFSET)
        {

        }

        public virtual void Draw(Vector2 OFFSET)
        {   
            if(model != null)
                Globals.spriteBatch.Draw(model, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dim.X, (int)dim.Y), null, Color.White, rot, Vector2.Zero, new SpriteEffects(), 0);
        }

        public virtual void Draw(Vector2 OFFSET, Color COLOR)
        {
            if (model != null)
                Globals.spriteBatch.Draw(model, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dim.X, (int)dim.Y), null, COLOR, rot, Vector2.Zero, new SpriteEffects(), 0);
        }
    }
}
