using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace For_real_this_time
{

    class Planet
    {
        public Vector2 pos;
        public int stopX;
        public int stopY;
        public int speed;
        public Texture2D tex;
        public Planet(Texture2D tex, Vector2 pos, int stopX, int stopY, int speed)
        {
            this.tex = tex;
            this.pos = pos;
            this.stopX = stopX;
            this.stopY = stopY;
            this.speed = speed;
        }

        public void Update()
        {
            if (pos.X < stopX && pos.Y < stopY)
            {
                pos.X = pos.X + speed;
                pos.Y = pos.Y + speed;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, pos, Color.White);
        }
    }
    //------------------------------------------------------------------------------------------------------------------------
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        List<Planet> planetList;
        Random rnd;
       

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;  
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D PixelEarth = Content.Load<Texture2D>(@"PixelEarth");
            Texture2D PixelMars = Content.Load<Texture2D>(@"PixelMars");

            Vector2 pos1 = Vector2.Zero;

            Vector2 pos2;
            pos2.X = Window.ClientBounds.Width / 2 - PixelMars.Width / 2;
            pos2.Y = Window.ClientBounds.Height / 2 - PixelMars.Height / 2;

            int stopX = Window.ClientBounds.Width - PixelEarth.Width;
            int stopY = Window.ClientBounds.Height - PixelEarth.Height;

            rnd = new Random();

            planetList = new List<Planet>();

            for (int i = 0; i < 10; i++)
            {
                int randX = rnd.Next(0, stopX);
                int randY = rnd.Next(0, stopY);
                int speed = rnd.Next(1, 5);
                Vector2 pos = new Vector2(randX, randY);
                Planet tempMars = new Planet(PixelMars, pos, stopX, stopY, speed);
                planetList.Add(tempMars);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            
            foreach(Planet planet in planetList)
            {
                
                planet.Update();
            }
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach(Planet planet in planetList)
            {
                planet.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}