using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame
{
    public static partial class ShaderEffects
    {
        public static Effect GetCutOffByAngleEffect(GraphicsDevice graphics)
        {
            return GetEffect("CutOffByAngle", graphics);
        }

        public static Texture2D ApplyCutOffByAngleEffect(Texture2D src, float angleCutOff, float angleStart, GraphicsDevice graphics)
        {
            var effect = GetCutOffByAngleEffect(graphics);

            effect.Parameters["angleCutOff"].SetValue(angleCutOff);
            effect.Parameters["angleStart"].SetValue(angleStart);

            return ApplyEffect(src, effect, Color.White, graphics);
        }
    }
}
