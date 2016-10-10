Shader "Heathen/Glow/Self-Illumin/VertexLit" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Spec Color", Color) = (1,1,1,1)
	_Shininess ("Shininess", Range (0.1, 1)) = 0.7
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_Illum ("Illumin (A)", 2D) = "white" {}
	_EmissionLM ("Emission (Lightmapper)", Float) = 0
	_GlowColor ("Glow Color", Color) = (1,1,1,1)
	_Inner ("Inner Intensity", Range(0.0,10)) = 2.0
	_Outter ("Outter Intensity", Range(0.0,10)) = 2.0
	_GlowMap ("Glow (A)", 2D) = "white" {}
}

SubShader {
	LOD 100
	Tags { "RenderType"="SelectiveGlow" }
	
	Pass {
		Name "BASE"
		Tags {"LightMode" = "Vertex"}
		Material {
			Diffuse [_Color]
			Shininess [_Shininess]
			Specular [_SpecColor]
		}
		SeparateSpecular On
		Lighting On
		SetTexture [_Illum] {
			constantColor [_Color]
			combine constant lerp (texture) previous
		}
		SetTexture [_MainTex] {
			Combine texture * previous, texture*primary
		}
	}
}

Fallback "VertexLit"
}
