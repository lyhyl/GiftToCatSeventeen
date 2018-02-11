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
  * Normal Depth
  */

NDVOutput VS_NormalDepth(VOutput input)
{
    NDVOutput output;

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

STVOutput VS_Falloff(VOutput input)
{
    STVOutput output;

	output = LightingVertex(input);

    return output;
}

float4 PS_Falloff(STPInput input) : COLOR0
{
    float4 color = TextureEnabled ? tex2D(Sampler, input.TextureCoordinate) : Color;

	color.rgb *= mul(CameraDirection, normalize(input.Normal));
    
    return color;
}

technique Falloff
{
	pass P0
	{
        VertexShader = compile vs_2_0 VS_Falloff();
        PixelShader = compile ps_2_0 PS_Falloff();
	}
}