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
    float dx = 0.5 - coords.x, dy = 0.5 - coords.y;
	float dist = dx * dx + dy * dy;
	float distFromCenter = 0.25 - dist;  // positive when inside the circle

	return (distFromCenter > 0)
		? float4(1, 1, 1, 1) * inColor
		: float4(0, 0, 0, 0);
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile PS_PROFILE PixelShaderFunction();
    }
}