#if SM4
    #define PS_PROFILE ps_4_0_level_9_1
    #define VS_PROFILE vs_4_0_level_9_1
#else
    #define PS_PROFILE ps_2_0
    #define VS_PROFILE vs_2_0
#endif

sampler s0;

float4 PixelShaderFunction( float4 inPosition : SV_Position,
			    float4 inColor : COLOR0,
			    float2 coords : TEXCOORD0 ) : COLOR0
{
    float4 color = tex2D(s0, coords);

	color.rgb = color.r * 0.2126 + color.g * 0.7152 + color.b * 0.0722;

	return color;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile PS_PROFILE PixelShaderFunction();
    }
}