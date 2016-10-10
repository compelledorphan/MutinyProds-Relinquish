Shader "Heathen/Glow/Normal/Tri-Planar" {
  Properties {
		_XTexture ("X", 2D) = "white" {}
		_XGlowColor ("X Glow Color", Color) = (1,1,1,1)
		_XGlowMap ("X Glow (A)", 2D) = "white" {}
		_YTexture ("Y", 2D) = "white" {}
		_YGlowColor ("Y Glow Color", Color) = (1,1,1,1)
		_YGlowMap ("Y Glow (A)", 2D) = "white" {}
		_ZTexture ("Z", 2D) = "white" {}
		_ZGlowColor ("Z Glow Color", Color) = (1,1,1,1)
		_ZGlowMap ("Z Glow (A)", 2D) = "white" {}
		_Inner ("Inner Intensity", Range(0.0,10)) = 2.0
		_Outter ("Outter Intensity", Range(0.0,10)) = 2.0
	}
	
	SubShader {
		Tags { "RenderType"="TriplanarGlow" }
		LOD 300
		
		CGPROGRAM
		#pragma target 4.0
		#pragma surface surf Lambert vertex:vert
		#pragma exclude_renderers flash
		
		half4 _XTexture_ST;
		half4 _YTexture_ST;
		half4 _ZTexture_ST;
		sampler2D _XTexture, _YTexture, _ZTexture;

		struct Input {
			half4 xyv;
			half4 zvb;
			half4 xyb;
			half3 norm;
		};
		
		void vert (inout appdata_full v, out Input o) {
		//Known issue with 2 part rendering causes glow map to offset 
		//in a manner diffrent than the defuse pass 
			UNITY_INITIALIZE_OUTPUT(Input,o);
			o.xyv.xy = TRANSFORM_TEX(v.vertex.zy, _XTexture);
			o.xyv.zw = TRANSFORM_TEX(v.vertex.zx, _YTexture);
			o.zvb.xy = TRANSFORM_TEX(v.vertex.xy, _ZTexture);
			
			o.norm = (abs(v.normal));
			o.norm = (o.norm - 0.2) * 7;  
			o.norm = max(o.norm, 0);
			o.norm /= (o.norm.x + o.norm.y + o.norm.z ).xxx;   
		}
		
		void surf (Input IN, inout SurfaceOutput o) {

			half3 norm = IN.norm;
			 
			half4 output = 
			tex2D(_XTexture, IN.xyv.xy)*norm.xxxx+
			tex2D(_YTexture, IN.xyv.zw)*norm.yyyy+
			tex2D(_ZTexture, IN.zvb.xy)*norm.zzzz;
			
			o.Albedo = output.rgb;
			o.Alpha = 0;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}