Shader "Heathen/Glow/Normal/Specular" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_GlowColor ("Glow Color", Color) = (1,1,1,1)
	_Inner ("Inner Intensity", Range(0.0,10)) = 2.0
	_Outter ("Outter Intensity", Range(0.0,10)) = 2.0
	_GlowMap ("Glow (A)", 2D) = "white" {}
}

SubShader {
	Tags { "RenderType"="SelectiveGlow" }
	LOD 300
	
CGPROGRAM
#pragma surface surf BlinnPhong
#pragma target 3.0

sampler2D _MainTex;
fixed4 _Color;
half _Shininess;
sampler2D _GlowMap;
half4 _GlowColor;
float _Inner, _Outter;

struct Input {
	float2 uv_MainTex;
	float2 uv_GlowMap;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	fixed4 glowAlpha = tex2D (_GlowMap, IN.uv_GlowMap) * _GlowColor;
	o.Albedo = tex.rgb * _Color.rgb;
	o.Gloss = tex.a;
	o.Alpha = tex.a * _Color.a;
	o.Specular = _Shininess;
	o.Emission = (((_Outter * _GlowColor) * (_Inner * glowAlpha)) * (glowAlpha.a * _GlowColor.a)).rgb * _Inner;
}
ENDCG
}

Fallback "VertexLit"
}
