Shader "Heathen/Glow/Reflective/Diffuse" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
	_MainTex ("Base (RGB) RefStrength (A)", 2D) = "white" {} 
	_Cube ("Reflection Cubemap", Cube) = "_Skybox" { TexGen CubeReflect }
	_GlowColor ("Glow Color", Color) = (1,1,1,1)
	_Inner ("Inner Intensity", Range(0.0,10)) = 2.0
	_Outter ("Outter Intensity", Range(0.0,10)) = 2.0
	_GlowMap ("Glow (A)", 2D) = "white" {}
}
SubShader {
	LOD 200
	Tags { "RenderType"="SelectiveGlow" }
	
CGPROGRAM
#pragma surface surf Lambert

sampler2D _MainTex;
samplerCUBE _Cube;
sampler2D _GlowMap;
half4 _GlowColor;
float _Inner, _Outter;

fixed4 _Color;
fixed4 _ReflectColor;

struct Input {
	float2 uv_MainTex;
	float3 worldRefl;
	float2 uv_GlowMap;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	fixed4 glowAlpha = tex2D (_GlowMap, IN.uv_GlowMap) * _GlowColor;
	fixed4 c = tex * _Color;
	o.Albedo = c.rgb;
	
	fixed4 reflcol = texCUBE (_Cube, IN.worldRefl);
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
