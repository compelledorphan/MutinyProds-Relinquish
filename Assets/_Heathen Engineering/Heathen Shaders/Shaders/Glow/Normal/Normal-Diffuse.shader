Shader "Heathen/Glow/Normal/Diffuse" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
	//===================Step 1 add selective glow properties===========================
	//The color to tent the glow with
	_GlowColor ("Glow Color", Color) = (1,1,1,1)
	//The strength of the glow with in the glow map area
	_Inner ("Inner Intensity", Range(0.0,10)) = 2.0
	//The strength of the glow post blur e.g. out side the glow map area
	_Outter ("Outter Intensity", Range(0.0,10)) = 2.0
	//Alpha > 0 will glow, the color of the pixel is the base color of the glow before tenting
	_GlowMap ("Glow (A)", 2D) = "white" {}
	//==================================================================================
}
SubShader {
	//=================Step 2 add the tag===============================================
    //Set the RenderType so the Render Selective Glow replacement shader knows to make this glow
	Tags { "RenderType"="SelectiveGlow" }
	//==================================================================================
	LOD 200

CGPROGRAM
#pragma surface surf Lambert
#pragma target 3.0

sampler2D _MainTex;
fixed4 _Color;
//=======================Step 3.a optional: support emission============================
//Initalize the glow parameters, this is not required for Selective Glow but allows us to apply the data to the material Emission value to support GI and reflection probes
sampler2D _GlowMap;
half4 _GlowColor;
float _Inner, _Outter;
//=======================================================================================

struct Input {
	float2 uv_MainTex;
	float2 uv_GlowMap;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	fixed4 glowAlpha = tex2D (_GlowMap, IN.uv_GlowMap) * _GlowColor;
	o.Albedo = c.rgb;
	o.Alpha = c.a;
	//=====================Step 3.b optional: support emission============================
	//Set the emission to glow values passed in
	o.Emission = (((_Outter * _GlowColor) * (_Inner * glowAlpha)) * (glowAlpha.a * _GlowColor.a)).rgb * _Inner;
	//====================================================================================
}
ENDCG
}

Fallback "VertexLit"
}
