using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame
{
    public static partial class ShaderEffects
    {
        public static Effect GetCircleEffect(GraphicsDevice graphics)
        {
            return GetEffect("Circle", graphics);
        }

        public static Texture2D CreateCircle(int diameter, Color color, GraphicsDevice graphics)
        {
            return CreateCircle(new Point(diameter), color, graphics);
        }

        public static Texture2D CreateCircle(Point diameter, Color color, GraphicsDevice graphics)
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
                    spriteBatch.Draw(GetPixel(graphics), new Rectangle(Point.Zero, diameter), color);
                    spriteBatch.End();
                }
                graphics.SetRenderTarget(null);

                return renderTarget;
            }
        }
    }
}
