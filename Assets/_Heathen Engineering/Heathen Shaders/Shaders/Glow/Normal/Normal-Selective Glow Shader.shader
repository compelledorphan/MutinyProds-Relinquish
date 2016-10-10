Shader "Heathen/Glow/Normal/Defuse Tint" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)	
	_MainTex ("Main (RGB)", 2D) = "white" {}
	_GlowColor ("Glow Color", Color) = (1,1,1,1)
	_Inner ("Inner Intensity", Range(0.0,10)) = 2.0
	_Outter ("Outter Intensity", Range(0.0,10)) = 2.0
	_GlowMap ("Glow (A)", 2D) = "white" {}
}

SubShader {
	Tags { "RenderType"="SelectiveGlow" "LightMode"="Vertex"}
	
	Pass {
		Colormask RGB
		Material {
			Diffuse [_Color]
			Ambient [_GlowColor]
		} 
		Lighting On
		SetTexture [_MainTex] {
			Combine texture * primary DOUBLE, texture * primary
		} 
	}
	}
}
