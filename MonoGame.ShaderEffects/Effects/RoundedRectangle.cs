using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame
{
    public static partial class ShaderEffects
    {
        public static Effect GetRoundedRectangleEffect(GraphicsDevice graphics)
        {
            return GetEffect("RoundedRectangle", graphics);
        }

        public static Texture2D CreateRoudedRectangle(float radius, Point rectangleSize, Color color, GraphicsDevice graphics)
        {
            lock (graphics)
            {
                var effect = GetRoundedRectangleEffect(graphics);

                effect.Parameters["radius"].SetValue(radius);
                effect.Parameters["rectangleSize"].SetValue(rectangleSize.ToVector2());

                var renderTarget = new RenderTarget2D(graphics, rectangleSize.X, rectangleSize.Y);

                // Draw the img with the effect
                graphics.SetRenderTarget(renderTarget);
                graphics.Clear(Color.Transparent);

                using (SpriteBatch spriteBatch = new SpriteBatch(graphics))
                {
                    spriteBatch.Begin(SpriteSortMode.Immediate, effect: effect);
                    spriteBatch.Draw(GetPixel(graphics), new Rectangle(Point.Zero, rectangleSize), Color.White);
                    spriteBatch.End();
                }
                graphics.SetRenderTarget(null);

                return ApplyEffect(renderTarget, effect, color, graphics);
            }
        }
    }
}
