using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MonoGame;

public static partial class ShaderEffects
{
    private static Dictionary<string, Effect> effectsCache = [];
    private static Texture2D pixel;

    public static Texture2D ApplyEffect(Texture2D src, Effect effect, Color color, GraphicsDevice graphics)
    {
        lock (graphics)
        {
            var renderTarget = new RenderTarget2D(graphics, src.Width, src.Height);

            // Draw the img with the effect
            graphics.SetRenderTarget(renderTarget);
            graphics.Clear(Color.Transparent);

            using (SpriteBatch spriteBatch = new SpriteBatch(graphics))
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, effect: effect);
                spriteBatch.Draw(src, new Vector2(0, 0), color);
                spriteBatch.End();
            }
            graphics.SetRenderTarget(null);

            return renderTarget;
        }
    }

    public static Texture2D GetPixel(GraphicsDevice graphics)
    {
        if (pixel == null)
        {
            pixel = new Texture2D(graphics, 1, 1);
            Color[] _Color = [Color.White];
            pixel.SetData(_Color);
        }

        return pixel;
    }

    private static Effect GetEffect(string effectName, GraphicsDevice graphics)
    {
        if (!effectsCache.TryGetValue(effectName, out Effect effect))
        {
            effect = LoadEffect(effectName, graphics);
            effectsCache.Add(effectName, effect);
        }

        return effect;
    }

    private static Effect LoadEffect(string effectName, GraphicsDevice graphics)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"MonoGame.ShaderEffects.Content.bin.DesktopGL.{effectName}.xnb";

        using Stream stream = assembly.GetManifestResourceStream(resourceName);
        using MemoryStream ms = new();
        stream.CopyTo(ms);
        var content = new XnbContentManager(ms, graphics);
        return content.Load<Effect>();
    }
}
