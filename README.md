<p align="center">
  <img src="http://daniloperes.com/MonoGame.ShaderEffects_Logo_256.png" alt="MonoGame.ShaderEffects" width="120" height="120">
</p>

# MonoGame.GlowEffect
<b>MonoGame.ShaderEffects</b> <b>MonoGame.ShaderEffects</b> is a library to generate effects with Shaders.

## Nuget package
There is a nuget package avaliable here https://www.nuget.org/packages/MonoGame.ShaderEffects/.

# Examples

<img src="https://raw.githubusercontent.com/DaniloPeres/MonoGame.ShaderEffects/main/MonoGame.ShaderEffects-Samples/Sample.gif" width="600" height="639">

- [Stroke Effect](#stroke-effect)
- [Glow Effect](#glow-effect)
- [Gray Scale Effect](#gray-scale-effect)
- [Circle Effect](#circle-effect)
- [Rounded Rectangle Effect](#rounded-rectangle-effect)
- [Cut off by angle](#cut-off-by-angle)

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

## Rounded Rectangle Effect

| Parameter | Type | Description |
| --- | --- | --- |
| radius | float | The round the corners of an element's outer border edge |
| rectangleSize | Point | The size of the rectangle |
| color | Color | The color of the object |

Example:
```csharp
float radius = 30;
Point rectangleSize = new Point(150, 200);
Color color = Color.White;
var myRoundedRectangle = ShaderEffects.CreateRoudedRectangle(radius, rectangleSize, color, GraphicsDevice);
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
