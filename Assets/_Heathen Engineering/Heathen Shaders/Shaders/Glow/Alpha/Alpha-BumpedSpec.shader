Shader "Heathen/Glow/Transparent/Bumped Specular" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_MainTex ("Base (RGB) TransGloss (A)", 2D) = "white" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_GlowColor ("Glow Color", Color) = (1,1,1,1)
	_Inner ("Inner Intensity", Range(0.0,10)) = 2.0
	_Outter ("Outter Intensity", Range(0.0,10)) = 2.0
	_GlowMap ("Glow (A)", 2D) = "white" {}
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="TransparentGlow"}
	LOD 400
	
CGPROGRAM
#pragma surface surf BlinnPhong alpha

sampler2D _MainTex;
sampler2D _BumpMap;
fixed4 _Color;
half _Shininess;
sampler2D _GlowMap;
half4 _GlowColor;
float _Inner, _Outter;

struct Input {
	float2 uv_MainTex;
	float2 uv_BumpMap;
	float2 uv_GlowMap;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	fixed4 glowAlpha = tex2D (_GlowMap, IN.uv_GlowMap) * _GlowColor;
	o.Albedo = tex.rgb * _Color.rgb;
	o.Gloss = tex.a;
	o.Alpha = tex.a * _Color.a;
	o.Specular = _Shininess;
	o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
	o.Emission = (((_Outter * _GlowColor) * (_Inner * glowAlpha)) * (glowAlpha.a * _GlowColor.a)).rgb * _Inner;
}
ENDCG
}

FallBack "Transparent/VertexLit"
}