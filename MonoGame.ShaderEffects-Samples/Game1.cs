using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGame.ShaderEffects_Samples
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Texture2D
            imgDragon,
            imgDragonStrokeOutline,
            imgDragonStrokeOutlineNoTexture,
            imgDragonGrayScale,
            imgCircle,
            imgDragonCutOffByAngle,
            imgDragonCutOffByAngleStart,
            imgDragonGlow; 

        SpriteFont arialSpritFont;

        float angleCutOff = 0;

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

            imgDragon = Content.Load<Texture2D>("Dragon");
            imgDragonStrokeOutline = StrokeEffect.CreateStroke(imgDragon, 4, Color.LightGreen, GraphicsDevice, StrokeType.OutlineAndTexture);
            imgDragonStrokeOutlineNoTexture = StrokeEffect.CreateStroke(imgDragon, 4, Color.LightGreen, GraphicsDevice, StrokeType.OutlineWithoutTexture);
            imgDragonGrayScale = ShaderEffects.ApplyGrayScaleEffect(imgDragon, GraphicsDevice);
            imgDragonGlow = GlowEffect.CreateGlow(imgDragon, Color.Magenta, 35, 35, 0, 0, 1, 6, 0, 0, 15, 15, GraphicsDevice);
            imgCircle = ShaderEffects.CreateCircle(150, GraphicsDevice);

            arialSpritFont = Content.Load<SpriteFont>("Arial");

            // crate a render target with margins
            using (var renderTargetResize = new RenderTarget2D(GraphicsDevice, imgDragon.Width + 20, imgDragon.Height + 20))
            {
                GraphicsDevice.SetRenderTarget(renderTargetResize);
                GraphicsDevice.Clear(Color.Green);

                using (SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice))
                {
                    // Create a new texture with the new size
                    spriteBatch.Begin();

                    // draw the texture with the margin
                    spriteBatch.Draw(imgDragon, new Vector2(10), Color.White);

                    spriteBatch.End();
                }

                var renderTarget = new RenderTarget2D(GraphicsDevice, renderTargetResize.Width, renderTargetResize.Height);

                // Draw the img with the effect
                GraphicsDevice.SetRenderTarget(renderTarget);
                GraphicsDevice.Clear(Color.Transparent);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            angleCutOff+=2;
            if (angleCutOff > 360)
                angleCutOff = angleCutOff % 360;

            imgDragonCutOffByAngle?.Dispose();
            imgDragonCutOffByAngle = ShaderEffects.ApplyCutOffByAngleEffect(imgDragon, angleCutOff, 0, GraphicsDevice);

            imgDragonCutOffByAngleStart?.Dispose();
            imgDragonCutOffByAngleStart = ShaderEffects.ApplyCutOffByAngleEffect(imgDragon, 280f, angleCutOff, GraphicsDevice);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            Vector2 pos = new Vector2(10, 10);
            spriteBatch.DrawString(arialSpritFont, "Original", pos, Color.Black);
            spriteBatch.Draw(imgDragon, pos + new Vector2(0, 75), Color.White);

            pos += new Vector2(200, 0);
            spriteBatch.DrawString(arialSpritFont, "Stroke outline", pos, Color.Black);
            spriteBatch.Draw(imgDragonStrokeOutline, pos + new Vector2(0, 75), Color.White);

            pos += new Vector2(200, 0);
            spriteBatch.DrawString(arialSpritFont, "Stroke outline\nwithout texture", pos, Color.Black);
            spriteBatch.Draw(imgDragonStrokeOutlineNoTexture, pos + new Vector2(0, 75), Color.White);

            pos += new Vector2(200, 0);
            spriteBatch.DrawString(arialSpritFont, "Gray scale", pos, Color.Black);
            spriteBatch.Draw(imgDragonGrayScale, pos + new Vector2(0, 75), Color.White);

            pos = new Vector2(10, pos.Y + 300);
            spriteBatch.DrawString(arialSpritFont, "Circle", pos, Color.Black);
            spriteBatch.Draw(imgCircle, pos + new Vector2(0, 75), Color.White);

            pos += new Vector2(200, 0);
            spriteBatch.DrawString(arialSpritFont, "Cut off by angle", pos, Color.Black);
            spriteBatch.Draw(imgDragonStrokeOutlineNoTexture, pos + new Vector2(0, 75), Color.White);
            spriteBatch.Draw(imgDragonCutOffByAngle, pos + new Vector2(0, 75) + new Vector2(4), Color.White);

            pos += new Vector2(200, 0);
            spriteBatch.DrawString(arialSpritFont, "Cut off by angle\nstart angle", pos, Color.Black);
            spriteBatch.Draw(imgDragonStrokeOutlineNoTexture, pos + new Vector2(0, 75), Color.White);
            spriteBatch.Draw(imgDragonCutOffByAngleStart, pos + new Vector2(0, 75) + new Vector2(4), Color.White);

            pos += new Vector2(220, 0);
            spriteBatch.DrawString(arialSpritFont, "Glow", pos, Color.Black);
            spriteBatch.Draw(imgDragonGlow, pos + new Vector2(-55, 20), Color.White);

            spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
