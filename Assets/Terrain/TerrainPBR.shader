// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:2865,x:32808,y:32507,varname:node_2865,prsc:2|diff-6446-OUT,spec-4553-OUT,gloss-3383-OUT,normal-5169-OUT;n:type:ShaderForge.SFN_Tex2d,id:8883,x:31776,y:33355,ptovrint:False,ptlb:Normal_Cliff,ptin:_Normal_Cliff,varname:_node_8674_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:329cc29562008b54a84f1ebe659e92d0,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:4124,x:31484,y:32562,ptovrint:False,ptlb:Diffuse_Grass,ptin:_Diffuse_Grass,varname:_node_8674_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c6ded2ee54f977f46abdb0f743513460,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8585,x:31525,y:33249,ptovrint:False,ptlb:Normal_Grass,ptin:_Normal_Grass,varname:_node_8674_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d712413138fd1e648a72225317cd6d27,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Lerp,id:2529,x:31869,y:33027,varname:node_2529,prsc:2|A-8585-RGB,B-8883-RGB,T-7149-RGB;n:type:ShaderForge.SFN_Normalize,id:5169,x:32074,y:33012,varname:node_5169,prsc:2|IN-2529-OUT;n:type:ShaderForge.SFN_Lerp,id:6446,x:31871,y:32660,varname:node_6446,prsc:2|A-4124-RGB,B-2536-RGB,T-7149-RGB;n:type:ShaderForge.SFN_Tex2d,id:2536,x:31658,y:32787,ptovrint:False,ptlb:Diffuse_Cliff,ptin:_Diffuse_Cliff,varname:node_8674,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e13ad7a9d4dc84697aaa540e7548d65b,ntxv:0,isnm:False;n:type:ShaderForge.SFN_VertexColor,id:7149,x:31263,y:32823,varname:node_7149,prsc:2;n:type:ShaderForge.SFN_VertexColor,id:7057,x:32527,y:33246,varname:node_7057,prsc:2;n:type:ShaderForge.SFN_Lerp,id:4553,x:32543,y:32348,varname:node_4553,prsc:2|A-6303-RGB,B-5389-RGB,T-7057-RGB;n:type:ShaderForge.SFN_Lerp,id:3383,x:32707,y:33029,varname:node_3383,prsc:2|A-829-RGB,B-3896-RGB,T-7057-RGB;n:type:ShaderForge.SFN_Tex2d,id:6303,x:32237,y:32106,ptovrint:False,ptlb:Metallic_Grass,ptin:_Metallic_Grass,varname:node_6303,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2b852b7e566fac74eaf18365cbc17d53,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5389,x:32221,y:32360,ptovrint:False,ptlb:Metallic_Cliff,ptin:_Metallic_Cliff,varname:_Metallic_Grass_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:76e48eced0e77ae42839ca6b7951db92,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:829,x:32271,y:33114,ptovrint:False,ptlb:Gloss_Grass,ptin:_Gloss_Grass,varname:node_829,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ecaeec286f25cde49afac3f2420fa8cd,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3896,x:32241,y:33300,ptovrint:False,ptlb:Gloss_Cliff,ptin:_Gloss_Cliff,varname:node_3896,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5aa3f99d96ab84948a8d1b2c41ea0cfe,ntxv:0,isnm:False;proporder:4124-2536-8585-8883-6303-5389-829-3896;pass:END;sub:END;*/

Shader "Shader Forge/TerrainPBR" {
    Properties {
        _Diffuse_Grass ("Diffuse_Grass", 2D) = "white" {}
        _Diffuse_Cliff ("Diffuse_Cliff", 2D) = "white" {}
        _Normal_Grass ("Normal_Grass", 2D) = "bump" {}
        _Normal_Cliff ("Normal_Cliff", 2D) = "bump" {}
        _Metallic_Grass ("Metallic_Grass", 2D) = "white" {}
        _Metallic_Cliff ("Metallic_Cliff", 2D) = "white" {}
        _Gloss_Grass ("Gloss_Grass", 2D) = "white" {}
        _Gloss_Cliff ("Gloss_Cliff", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Normal_Cliff; uniform float4 _Normal_Cliff_ST;
            uniform sampler2D _Diffuse_Grass; uniform float4 _Diffuse_Grass_ST;
            uniform sampler2D _Normal_Grass; uniform float4 _Normal_Grass_ST;
            uniform sampler2D _Diffuse_Cliff; uniform float4 _Diffuse_Cliff_ST;
            uniform sampler2D _Metallic_Grass; uniform float4 _Metallic_Grass_ST;
            uniform sampler2D _Metallic_Cliff; uniform float4 _Metallic_Cliff_ST;
            uniform sampler2D _Gloss_Grass; uniform float4 _Gloss_Grass_ST;
            uniform sampler2D _Gloss_Cliff; uniform float4 _Gloss_Cliff_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_Grass_var = UnpackNormal(tex2D(_Normal_Grass,TRANSFORM_TEX(i.uv0, _Normal_Grass)));
                float3 _Normal_Cliff_var = UnpackNormal(tex2D(_Normal_Cliff,TRANSFORM_TEX(i.uv0, _Normal_Cliff)));
                float3 normalLocal = normalize(lerp(_Normal_Grass_var.rgb,_Normal_Cliff_var.rgb,i.vertexColor.rgb));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _Gloss_Grass_var = tex2D(_Gloss_Grass,TRANSFORM_TEX(i.uv0, _Gloss_Grass));
                float4 _Gloss_Cliff_var = tex2D(_Gloss_Cliff,TRANSFORM_TEX(i.uv0, _Gloss_Cliff));
                float gloss = lerp(_Gloss_Grass_var.rgb,_Gloss_Cliff_var.rgb,i.vertexColor.rgb);
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                d.boxMax[0] = unity_SpecCube0_BoxMax;
                d.boxMin[0] = unity_SpecCube0_BoxMin;
                d.probePosition[0] = unity_SpecCube0_ProbePosition;
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.boxMax[1] = unity_SpecCube1_BoxMax;
                d.boxMin[1] = unity_SpecCube1_BoxMin;
                d.probePosition[1] = unity_SpecCube1_ProbePosition;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                UnityGI gi = UnityGlobalIllumination (d, 1, gloss, normalDirection);
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 _Diffuse_Grass_var = tex2D(_Diffuse_Grass,TRANSFORM_TEX(i.uv0, _Diffuse_Grass));
                float4 _Diffuse_Cliff_var = tex2D(_Diffuse_Cliff,TRANSFORM_TEX(i.uv0, _Diffuse_Cliff));
                float3 diffuseColor = lerp(_Diffuse_Grass_var.rgb,_Diffuse_Cliff_var.rgb,i.vertexColor.rgb); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                float4 _Metallic_Grass_var = tex2D(_Metallic_Grass,TRANSFORM_TEX(i.uv0, _Metallic_Grass));
                float4 _Metallic_Cliff_var = tex2D(_Metallic_Cliff,TRANSFORM_TEX(i.uv0, _Metallic_Cliff));
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, lerp(_Metallic_Grass_var.rgb,_Metallic_Cliff_var.rgb,i.vertexColor.rgb).r, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * unity_LightGammaCorrectionConsts_PIDiv4 );
                float3 directSpecular = 1 * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Normal_Cliff; uniform float4 _Normal_Cliff_ST;
            uniform sampler2D _Diffuse_Grass; uniform float4 _Diffuse_Grass_ST;
            uniform sampler2D _Normal_Grass; uniform float4 _Normal_Grass_ST;
            uniform sampler2D _Diffuse_Cliff; uniform float4 _Diffuse_Cliff_ST;
            uniform sampler2D _Metallic_Grass; uniform float4 _Metallic_Grass_ST;
            uniform sampler2D _Metallic_Cliff; uniform float4 _Metallic_Cliff_ST;
            uniform sampler2D _Gloss_Grass; uniform float4 _Gloss_Grass_ST;
            uniform sampler2D _Gloss_Cliff; uniform float4 _Gloss_Cliff_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_Grass_var = UnpackNormal(tex2D(_Normal_Grass,TRANSFORM_TEX(i.uv0, _Normal_Grass)));
                float3 _Normal_Cliff_var = UnpackNormal(tex2D(_Normal_Cliff,TRANSFORM_TEX(i.uv0, _Normal_Cliff)));
                float3 normalLocal = normalize(lerp(_Normal_Grass_var.rgb,_Normal_Cliff_var.rgb,i.vertexColor.rgb));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _Gloss_Grass_var = tex2D(_Gloss_Grass,TRANSFORM_TEX(i.uv0, _Gloss_Grass));
                float4 _Gloss_Cliff_var = tex2D(_Gloss_Cliff,TRANSFORM_TEX(i.uv0, _Gloss_Cliff));
                float gloss = lerp(_Gloss_Grass_var.rgb,_Gloss_Cliff_var.rgb,i.vertexColor.rgb);
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 _Diffuse_Grass_var = tex2D(_Diffuse_Grass,TRANSFORM_TEX(i.uv0, _Diffuse_Grass));
                float4 _Diffuse_Cliff_var = tex2D(_Diffuse_Cliff,TRANSFORM_TEX(i.uv0, _Diffuse_Cliff));
                float3 diffuseColor = lerp(_Diffuse_Grass_var.rgb,_Diffuse_Cliff_var.rgb,i.vertexColor.rgb); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                float4 _Metallic_Grass_var = tex2D(_Metallic_Grass,TRANSFORM_TEX(i.uv0, _Metallic_Grass));
                float4 _Metallic_Cliff_var = tex2D(_Metallic_Cliff,TRANSFORM_TEX(i.uv0, _Metallic_Cliff));
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, lerp(_Metallic_Grass_var.rgb,_Metallic_Cliff_var.rgb,i.vertexColor.rgb).r, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * unity_LightGammaCorrectionConsts_PIDiv4 );
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Diffuse_Grass; uniform float4 _Diffuse_Grass_ST;
            uniform sampler2D _Diffuse_Cliff; uniform float4 _Diffuse_Cliff_ST;
            uniform sampler2D _Metallic_Grass; uniform float4 _Metallic_Grass_ST;
            uniform sampler2D _Metallic_Cliff; uniform float4 _Metallic_Cliff_ST;
            uniform sampler2D _Gloss_Grass; uniform float4 _Gloss_Grass_ST;
            uniform sampler2D _Gloss_Cliff; uniform float4 _Gloss_Cliff_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float4 _Diffuse_Grass_var = tex2D(_Diffuse_Grass,TRANSFORM_TEX(i.uv0, _Diffuse_Grass));
                float4 _Diffuse_Cliff_var = tex2D(_Diffuse_Cliff,TRANSFORM_TEX(i.uv0, _Diffuse_Cliff));
                float3 diffColor = lerp(_Diffuse_Grass_var.rgb,_Diffuse_Cliff_var.rgb,i.vertexColor.rgb);
                float specularMonochrome;
                float3 specColor;
                float4 _Metallic_Grass_var = tex2D(_Metallic_Grass,TRANSFORM_TEX(i.uv0, _Metallic_Grass));
                float4 _Metallic_Cliff_var = tex2D(_Metallic_Cliff,TRANSFORM_TEX(i.uv0, _Metallic_Cliff));
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, lerp(_Metallic_Grass_var.rgb,_Metallic_Cliff_var.rgb,i.vertexColor.rgb).r, specColor, specularMonochrome );
                float4 _Gloss_Grass_var = tex2D(_Gloss_Grass,TRANSFORM_TEX(i.uv0, _Gloss_Grass));
                float4 _Gloss_Cliff_var = tex2D(_Gloss_Cliff,TRANSFORM_TEX(i.uv0, _Gloss_Cliff));
                float roughness = 1.0 - lerp(_Gloss_Grass_var.rgb,_Gloss_Cliff_var.rgb,i.vertexColor.rgb);
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
