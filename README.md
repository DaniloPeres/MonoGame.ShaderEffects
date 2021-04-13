<p align="center">
  <img src="http://daniloperes.com/MonoGame.ShaderEffects_Logo_256.png" alt="MonoGame.ShaderEffects" width="120" height="120">
</p>

# MonoGame.GlowEffect
<b>MonoGame.ShaderEffects</b> is a library to generate glow for Texture2D in MonoGame. We also support Sprite Font.
We use a shader effect to generate the Glow effect.

## Nuget package
There is a nuget package avaliable here https://www.nuget.org/packages/MonoGame.ShaderEffects/.

# Examples

<img src="http://daniloperes.com/MonoGame.ShaderEffects.Samples.gif?1" alt="MonoGame.ShaderEffects" width="640" height="496">

# Shader Effects

## Stroke Effect

https://github.com/DaniloPeres/MonoGame.StrokeEffect

## Glow Effect

https://github.com/DaniloPeres/MonoGame.GlowEffect

## Gray Scale Effect

Example:
```csharp
var textureGrayScale = ShaderEffects.ApplyGrayScaleEffect(myTexture, GraphicsDevice);
```

## Circle Effect

| Parameter | Type | Description |
| --- | --- | --- |
| diameter | int | The diameter of the circle in pixels |

Example:
```csharp
int circleDiameter = 150;
var myCircle = ShaderEffects.CreateCircle(circleDiameter, GraphicsDevice);
```

## Cut off by angle

| Parameter | Type | Description |
| --- | --- | --- |
| src | Texture2D | The original Texture2D |
| angleCutOff | float | The angle to be removed from the image in 360 degrees |
| angleStart | float | The angle to start the image in 360 degrees |

Example 1:
```csharp
float angleCutOff = 90f;
float angleStart = 0f;
var myCircularImageCut = ShaderEffects.ApplyCutOffByAngleEffect(myTexture, angleCutOff, angleStart, GraphicsDevice);
```

Result:
<img src="http://daniloperes.com/MonoGame.ShaderEffects.CutAngle.Eg1.png" alt="MonoGame.ShaderEffects" width="401" height="257">

Example 2:
```csharp
float angleCutOff = 235f;
float angleStart = 235f;
var myCircularImageCut = ShaderEffects.ApplyCutOffByAngleEffect(myTexture, angleCutOff, angleStart, GraphicsDevice);
```

Result:
<img src="http://daniloperes.com/MonoGame.ShaderEffects.CutAngle.Eg2.png" alt="MonoGame.ShaderEffects" width="424" height="233">

## License

MIT
