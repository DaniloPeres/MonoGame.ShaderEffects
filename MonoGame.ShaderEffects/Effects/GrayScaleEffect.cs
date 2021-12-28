using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame
{
    public static partial class ShaderEffects
    {
        public static Effect GetGrayScaleEffect(GraphicsDevice graphics)
        {
            return GetEffect("GrayScale", graphics);
        }

        public static Texture2D ApplyGrayScaleEffect(Texture2D src, GraphicsDevice graphics)
        {
            return ApplyEffect(src, GetGrayScaleEffect(graphics), Color.White, graphics);
        }
    }
}
