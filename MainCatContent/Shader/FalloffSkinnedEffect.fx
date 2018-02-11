float4x4 World;
float4x4 View;
float4x4 Projection;

float3 LightDirection = normalize(float3(1,1,1));
float3 CameraDirection;

bool TextureEnabled;
texture Texture;
float4 Color = {1, 1, 1, 1};

sampler Sampler = sampler_state
{
    Texture = <Texture>;
    
    MinFilter = Linear;
    MagFilter = Linear;
    MipFilter = Linear;
    
    AddressU = Wrap;
    AddressV = Wrap;
};

/*
  * Description
  */

struct VOutput
{
    float4 Position : POSITION0;
    float3 Normal : NORMAL0;
	float3 BlendWeights : BLENDWEIGHT0;
    uint4 BlendIndices : BLENDINDICES0;
    float2 TextureCoordinate : TEXCOORD0;
};

struct STVOutput
{
    float4 Position : POSITION0;
    float2 TextureCoordinate : TEXCOORD0;
	float3 Normal : TEXCOORD1;
};

struct NDVOutput
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
};

struct STPInput
{
    float2 TextureCoordinate : TEXCOORD0;
	float3 Normal : TEXCOORD1;
};

/*
  * Skinned
  */

#define MATRIX_PALETTE_SIZE 59
float4x4 amPalette[MATRIX_PALETTE_SIZE];

VOutput Skinned(const VOutput input, int infBoneNum)
{
    VOutput output = (VOutput)(0);

	float lastWeight = 1.0f;
	float weight;
	float weightList[3] = (float[3])input.BlendWeights;
	uint indexList[4] = (uint[4])input.BlendIndices;

	[unroll]
	for(int i = 0; (i < 3) && (i < infBoneNum - 1); ++i)
	{
		weight = weightList[i];
		lastWeight -= weight;
		output.Position += mul(input.Position, amPalette[indexList[i]]) * weight;
		output.Normal += mul(input.Normal, amPalette[indexList[i]]) * weight;
	}
	output.Position += mul(input.Position, amPalette[indexList[infBoneNum - 1]]) * lastWeight;
	output.Normal += mul(input.Normal, amPalette[indexList[infBoneNum - 1]]) * lastWeight;

	output.TextureCoordinate = input.TextureCoordinate;

    return output;
}

/*
  * Normal Depth
  */

NDVOutput VS_NormalDepth(VOutput input)
{
    NDVOutput output;

	input = Skinned(input, 2);

    output.Position = mul(mul(mul(input.Position, World), View), Projection);
    
    float3 worldNormal = mul(input.Normal, World);

    // The output color holds the normal, scaled to fit into a 0 to 1 range.
    output.Color.rgb = (worldNormal + 1) / 2;

    // The output alpha holds the depth, scaled to fit into a 0 to 1 range.
    output.Color.a = output.Position.z / output.Position.w;
    
    return output;
}

float4 PS_NormalDepth(float4 color : COLOR0) : COLOR0
{
    return color;
}

technique NormalDepth
{
    pass P0
    {
        VertexShader = compile vs_2_0 VS_NormalDepth();
        PixelShader = compile ps_2_0 PS_NormalDepth();
    }
}

/*
  * Toon
  */

STVOutput LightingVertex(VOutput input)
{
    STVOutput output;

    // Apply camera matrices to the input position.
    output.Position = mul(mul(mul(input.Position, World), View), Projection);

    output.TextureCoordinate = input.TextureCoordinate;

	output.Normal = normalize(mul(input.Normal, World));
    
    return output;
}

STVOutput VS_SkinnedFalloff(VOutput input)
{
    STVOutput output;
	
	input = Skinned(input, 2);

	output = LightingVertex(input);

    return output;
}

float4 PS_SkinnedFalloff(STPInput input) : COLOR0
{
    float4 color = TextureEnabled ? tex2D(Sampler, input.TextureCoordinate) : Color;
	
	color.a = 1;
	color.rgb *= mul(CameraDirection, normalize(input.Normal));

	/*
	  * swap R and B
	  * XNA px format : ARGB
	  * System.Drawing.Bitmap px format : ABGR
	  */
	float tmp = color.r;
	color.r = color.b;
	color.b = tmp;
    
    return color;
}

technique SkinnedToon
{
	pass P0
	{
        VertexShader = compile vs_2_0 VS_SkinnedFalloff();
        PixelShader = compile ps_2_0 PS_SkinnedFalloff();
	}
}