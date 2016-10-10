Shader "Heathen/Glow/Cutout/Diffuse" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	_GlowColor ("Glow Color", Color) = (1,1,1,1)
	_Inner ("Inner Intensity", Range(0.0,10)) = 2.0
	_Outter ("Outter Intensity", Range(0.0,10)) = 2.0
	_GlowMap ("Glow (A)", 2D) = "white" {}
}

SubShader {
	Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentGlow"}
	LOD 200
	Cull Off
CGPROGRAM
#pragma surface surf Lambert alphatest:_Cutoff
#pragma target 3.0

sampler2D _MainTex;
fixed4 _Color;
sampler2D _GlowMap;
half4 _GlowColor;
float _Inner, _Outter;

struct Input {
	float2 uv_MainTex;
	float2 uv_GlowMap;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	fixed4 glowAlpha = tex2D (_GlowMap, IN.uv_GlowMap) * _GlowColor;
	o.Albedo = c.rgb;
	
	if(c.a > 0.25)
	{
		o.Alpha = c.a;
		o.Emission = (((_Outter * _GlowColor) * (_Inner * glowAlpha)) * (glowAlpha.a * _GlowColor.a)).rgb * _Inner;
	}
	else
	{
		o.Alpha = 0;
		o.Emission = fixed3(0,0,0);
	}
}
ENDCG
}

Fallback "Transparent/Cutout/VertexLit"
}
