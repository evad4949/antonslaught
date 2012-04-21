using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AntOnslaught
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Renderer rend;
        Map map;
        List<MovableObject> movableObjs;
        Menu menu;
        KeyboardState keyState;
        MouseState mouseState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            movableObjs = new List<MovableObject>();
            movableObjs.Add(new WorkerAnt(new Vector2(0, 0), new SpriteAnimation(Content.Load<Texture2D>("worker_sprite_sheet"), 32, 32, 100)));
            base.Initialize();
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            map = new Map();
            map.setTexture(Content.Load<Texture2D>("trunk"));
            rend = new Renderer(spriteBatch, GraphicsDevice.Viewport, new Vector2(0, 0));
            menu = new Menu(spriteBatch, Content);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            // TODO: Add your update logic here
            base.Update(gameTime);
            updateMenuState(gameTime);
            if (menu.shouldQuit())
                this.Exit();
            if (!menu.isPaused())
                updateGameState(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (menu.isPaused())
            {
                drawMenuState();
            }
            else
            {
                drawGameState();
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void updateGameState(GameTime gameTime)
        {
            foreach (MovableObject obj in movableObjs)
            {
                if (!obj.updateMovement(gameTime))
                {
                    obj.setPath(
                        map.getPath(map.getCell((int)obj.getPosition().X / 32, (int)obj.getPosition().X / 32),
                        map.getCell((int)obj.getGoal().X / 32, (int)obj.getGoal().X / 32)));
                }

            }
            
            if ( mouseState.RightButton == ButtonState.Pressed )
            {
                foreach(Ant ant in movableObjs)
                {
                    ant.setGoal(new Vector2(mouseState.X, mouseState.Y));
                    ant.setPath(
                        map.getPath(map.getCell((int)ant.getPosition().X / 32, (int)ant.getPosition().X / 32),
                        map.getCell((int)ant.getGoal().X / 32, (int)ant.getGoal().X / 32)));
                }
            }
            // Allows the game to exit
            //if (keyState.IsKeyDown(Keys.Escape))
            //{
            //    this.Exit();
            //}
            //Move the map around
            if (keyState.IsKeyDown(Keys.Left))
            {
                Vector2 vec = rend.getViewCenter();
                rend.setViewCenter(new Vector2(vec.X - 1, vec.Y));
            }
            if (keyState.IsKeyDown(Keys.Right))
            {
                Vector2 vec = rend.getViewCenter();
                rend.setViewCenter(new Vector2(vec.X + 1, vec.Y));
            }
            if (keyState.IsKeyDown(Keys.Up))
            {
                Vector2 vec = rend.getViewCenter();
                rend.setViewCenter(new Vector2(vec.X, vec.Y - 1));
            }
            if (keyState.IsKeyDown(Keys.Down))
            {
                Vector2 vec = rend.getViewCenter();
                rend.setViewCenter(new Vector2(vec.X, vec.Y + 1));
            }
        }

        public void drawGameState()
        {
            rend.Draw(map);
            foreach (MovableObject obj in movableObjs)
            {
                rend.Draw(obj);
            }
        }

        public void updateMenuState(GameTime gameTime)
        {
            menu.update(gameTime, keyState, mouseState);
        }

        public void drawMenuState()
        {
            menu.draw();
        }
    }
}
