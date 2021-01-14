using Kolibri.Engine;
using Kolibri.Engine.Input;
using Kolibri.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Kolibri
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private Workspace ws;
        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Globals.screenWidth = 1000;
            Globals.screenHeight = 720;
            graphics.PreferredBackBufferWidth = (int)Globals.screenWidth;
            graphics.PreferredBackBufferHeight = (int)Globals.screenHeight;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.SystemFont = "galleryFont";
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.content = this.Content;
            Globals.keyboard = new EKeyboard();
            Globals.mouse = new EMouseControl();
            Globals.primitives = new EPrimitives();

            ws = new Workspace();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Globals.gameTime = gameTime;
            Globals.keyboard.Update();
            Globals.mouse.Update();
            ws.Update();
            Globals.keyboard.LateUpdate();
            Globals.mouse.LateUpdate();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(56,58,60));
            Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            ws.Draw();
            Globals.spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    public static class Program
    {
        static void Main()
        {
            using (var game = new Main())
                game.Run();
        }
    }
}
