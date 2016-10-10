Shader "Heathen/Glow/Reflective/Bumped Diffuse" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
	_MainTex ("Base (RGB) RefStrength (A)", 2D) = "white" {}
	_Cube ("Reflection Cubemap", Cube) = "_Skybox" { TexGen CubeReflect }
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_GlowColor ("Glow Color", Color) = (1,1,1,1)
	_Inner ("Inner Intensity", Range(0.0,10)) = 2.0
	_Outter ("Outter Intensity", Range(0.0,10)) = 2.0
	_GlowMap ("Glow (A)", 2D) = "white" {}
}

SubShader {
	Tags { "RenderType"="SelectiveGlow" }
	LOD 300
	
CGPROGRAM
#pragma surface surf Lambert
#pragma exclude_renderers d3d11_9x
#pragma target 3.0

sampler2D _MainTex;
sampler2D _BumpMap;
samplerCUBE _Cube;
sampler2D _GlowMap;
half4 _GlowColor;
float _Inner, _Outter;

fixed4 _Color;
fixed4 _ReflectColor;

struct Input {
	float2 uv_MainTex;
	float2 uv_BumpMap;
	float2 uv_GlowMap;
	float3 worldRefl;
	INTERNAL_DATA
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	fixed4 glowAlpha = tex2D (_GlowMap, IN.uv_GlowMap) * _GlowColor;
	fixed4 c = tex * _Color;
	o.Albedo = c.rgb;
	
	o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
	
	float3 worldRefl = WorldReflectionVector (IN, o.Normal);
	fixed4 reflcol = texCUBE (_Cube, worldRefl);
	reflcol *= tex.a;
	if(glowAlpha.a > 0.1)
	{
		o.Emission = (((_Outter * _GlowColor) * (_Inner * glowAlpha)) * (glowAlpha.a * _GlowColor.a)).rgb * _Inner;
	}
	else
	{
		o.Emission = reflcol.rgb * _ReflectColor.rgb;
	}
	o.Alpha = reflcol.a * _ReflectColor.a;
}
ENDCG
}

FallBack "Reflective/VertexLit"
}
