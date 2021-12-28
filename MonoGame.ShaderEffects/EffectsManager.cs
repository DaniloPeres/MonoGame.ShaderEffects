using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace MonoGame
{
    public enum GraphicsApi
    {
        OPEN_GL,
        DIRECT_X
    }

    public static partial class ShaderEffects
    {
        private static Dictionary<string, Effect> effectsCache = new Dictionary<string, Effect>();
        private static string CurrentShaderExtension => GetShaderExtension() == GraphicsApi.OPEN_GL ? "OpenGL" : "DirectX";
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
                Color[] _Color = new Color[1];
                _Color[0] = Color.White;
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
            var resourceName = $"MonoGame.ShaderEffects.Content.{CurrentShaderExtension}.{effectName}.xnb";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    var content = new XnbContentManager(ms, graphics);
                    return content.Load<Effect>("MyMemoryStreamAsset");
                }
            }
        }

        private static GraphicsApi GetShaderExtension()
        {
            // Use reflection to figure out if Shader.Profile is OpenGL (0) or DirectX (1).
            // May need to be changed / fixed for future shader profiles.
            const string SHADER_TYPE = "Microsoft.Xna.Framework.Graphics.Shader";
            const string PROFILE = "Profile";

            var assembly = typeof(Game).GetTypeInfo().Assembly;
            if (assembly == null)
                throw new Exception(
                    "Error determining shader profile. Couldn't find assembly. Falling back to OpenGL.");

            var shaderType = assembly.GetType(SHADER_TYPE);
            if (shaderType == null)
                throw new Exception(
                    $"Error determining shader profile. Couldn't find shader type of '{SHADER_TYPE}'. Falling back to OpenGL.");

            var shaderTypeInfo = shaderType.GetTypeInfo();
            if (shaderTypeInfo == null)
                throw new Exception(
                    "Error determining shader profile. Couldn't get TypeInfo of shadertype. Falling back to OpenGL.");

            // https://github.com/MonoGame/MonoGame/blob/develop/MonoGame.Framework/Graphics/Shader/Shader.cs#L47
            var profileProperty = shaderTypeInfo.GetDeclaredProperty(PROFILE);
            var value = (int)profileProperty.GetValue(null);

            switch (value)
            {
                case 0:
                    return GraphicsApi.OPEN_GL;
                case 1:
                    return GraphicsApi.DIRECT_X;
                default:
                    throw new Exception("Unknown shader profile.");
            }
        }
    }
}
