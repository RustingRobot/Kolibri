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
        private ObjManager om;
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
            //set all Globals.*
            Globals.SystemFont = "galleryFont";
            Globals.fontSize = new Vector2(0.5f,0.5f);
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.content = this.Content;
            Globals.keyboard = new EKeyboard();
            Globals.mouse = new EMouseControl();
            Globals.primitives = new EPrimitives();
            Globals.graphicsDevice = GraphicsDevice;
            om = new ObjManager();
        }

        protected override void Update(GameTime gameTime)   //main update loop
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Globals.gameTime = gameTime;
            Globals.keyboard.Update();
            Globals.mouse.Update();
            om.Update();
            Globals.keyboard.LateUpdate();
            Globals.mouse.LateUpdate();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(73,78,80));
            Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            om.Draw();
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
