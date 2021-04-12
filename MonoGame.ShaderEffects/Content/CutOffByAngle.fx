#if SM4
	#define PS_PROFILE ps_4_0
	#define VS_PROFILE vs_4_0
#else
	#define PS_PROFILE ps_3_0
	#define VS_PROFILE vs_3_0
#endif

float angleCutOff;
float angleStart;
static const float pi = 3.14159265;
static const float center = 0.5f;
sampler s0;

float4 PixelShaderFunction( float4 inPosition : SV_Position,
			    float4 inColor : COLOR0,
			    float2 coords : TEXCOORD0 ) : COLOR0
{
	float angle = atan2(coords.x - center, coords.y - center) * 180 / pi + 180 + angleStart;
	if (angle < 0) {
		angle = angle + 2 * pi;
	}

	float angleCutOffCalc = angleCutOff;

	bool isInAngle = angle % 360 <= angleCutOffCalc;

    return (isInAngle)
		? float4(0, 0, 0, 0)
		: tex2D(s0, coords) * inColor;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile PS_PROFILE PixelShaderFunction();
    }
}