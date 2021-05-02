using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame
{
    public static partial class ShaderEffects
    {
        public static Effect GetCircleEffect(GraphicsDevice graphics)
        {
            return GetEffect("Circle", graphics);
        }

        public static Texture2D CreateCircle(int diameter, GraphicsDevice graphics)
        {
            return CreateCircle(new Point(diameter), graphics);
        }

        public static Texture2D CreateCircle(Point diameter, GraphicsDevice graphics)
        {
            lock (graphics)
            {
                var effect = GetCircleEffect(graphics);

                var renderTarget = new RenderTarget2D(graphics, diameter.X, diameter.Y);

                // Draw the img with the effect
                graphics.SetRenderTarget(renderTarget);
                graphics.Clear(Color.Transparent);

                using (SpriteBatch spriteBatch = new SpriteBatch(graphics))
                {
                    spriteBatch.Begin(SpriteSortMode.Immediate, effect: effect);
                    spriteBatch.Draw(GetPixel(graphics), new Rectangle(Point.Zero, diameter), Color.White);
                    spriteBatch.End();
                }
                graphics.SetRenderTarget(null);

                return renderTarget;
            }
        }
    }
}
