﻿using Kolibri.Engine;
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
        GraphicsDeviceManager graphics;
        ObjManager om;
        Texture2D colorMap;
        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;
        }

        public void OnResize(Object sender, EventArgs e)
        {
            graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            graphics.ApplyChanges();
            Globals.screenWidth = graphics.PreferredBackBufferWidth;
            Globals.screenHeight = graphics.PreferredBackBufferHeight;
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
            colorMap = Content.Load<Texture2D>("ColorMap");
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

        protected override void Draw(GameTime gameTime) //main draw loop
        {
            GraphicsDevice.Clear(new Color(73,78,80));  //BG color
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
