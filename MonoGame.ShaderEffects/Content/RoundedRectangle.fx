#if SM4
    #define PS_PROFILE ps_4_0_level_9_1
    #define VS_PROFILE vs_4_0_level_9_1
#else
    #define PS_PROFILE ps_2_0
    #define VS_PROFILE vs_2_0
#endif

float radius;
float2 rectangleSize : VPOS;
sampler s0;

// Rounded rect distance function
float udRoundRect(float2 p, float2 b, float r)
{
	return length(max(abs(p) - b, 0.0)) - r;
}

float4 PixelShaderFunction( float4 inPosition : SV_Position,
			    float4 inColor : COLOR0,
			    float2 coords : TEXCOORD0 ) : COLOR0
{
    float2 center = rectangleSize / 2.0;
    float2 pos = coords * rectangleSize;

    float a = clamp(udRoundRect(pos - center, center - radius, radius), 0.0, 1.0);

	return (a > 0)
        ? float4(0, 0, 0, 0)
        : float4(1, 1, 1, 1) * inColor;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile PS_PROFILE PixelShaderFunction();
    }
}