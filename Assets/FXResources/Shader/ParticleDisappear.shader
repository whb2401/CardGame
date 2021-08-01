// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:False,igpj:False,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33404,y:32634,varname:node_4795,prsc:2|emission-2015-OUT,custl-4344-OUT,clip-4257-OUT,voffset-8455-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:31853,y:32799,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:31853,y:32436,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Tex2d,id:9142,x:32185,y:33280,varname:node_9142,prsc:2,ntxv:0,isnm:False|UVIN-3930-OUT,TEX-6866-TEX;n:type:ShaderForge.SFN_Multiply,id:5670,x:32550,y:33280,varname:node_5670,prsc:2|A-4663-OUT,B-9142-A;n:type:ShaderForge.SFN_Desaturate,id:4663,x:32381,y:33280,varname:node_4663,prsc:2|COL-9142-RGB;n:type:ShaderForge.SFN_TexCoord,id:1033,x:31225,y:33254,varname:node_1033,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Step,id:4257,x:32845,y:33001,varname:node_4257,prsc:2|A-2985-OUT,B-2095-R;n:type:ShaderForge.SFN_Multiply,id:4344,x:32512,y:32570,varname:node_4344,prsc:2|A-4801-OUT,B-6468-OUT,C-2053-RGB;n:type:ShaderForge.SFN_Vector1,id:4801,x:32512,y:32505,varname:node_4801,prsc:2,v1:2;n:type:ShaderForge.SFN_OneMinus,id:2369,x:32370,y:32963,varname:node_2369,prsc:2|IN-163-OUT;n:type:ShaderForge.SFN_Subtract,id:163,x:32185,y:32963,varname:node_163,prsc:2|A-8653-OUT,B-6299-OUT;n:type:ShaderForge.SFN_Vector1,id:6299,x:31984,y:32983,varname:node_6299,prsc:2,v1:0.02;n:type:ShaderForge.SFN_Color,id:2717,x:32072,y:32050,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:node_2717,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Set,id:6798,x:32831,y:33501,varname:Mask,prsc:2|IN-5670-OUT;n:type:ShaderForge.SFN_Get,id:2137,x:31648,y:32203,varname:node_2137,prsc:2|IN-6798-OUT;n:type:ShaderForge.SFN_Vector1,id:1195,x:31625,y:32318,varname:node_1195,prsc:2,v1:0.25;n:type:ShaderForge.SFN_Subtract,id:3417,x:31895,y:32203,varname:node_3417,prsc:2|A-2137-OUT,B-1195-OUT;n:type:ShaderForge.SFN_Clamp01,id:5139,x:32072,y:32203,varname:node_5139,prsc:2|IN-3417-OUT;n:type:ShaderForge.SFN_Power,id:2985,x:32555,y:32973,varname:node_2985,prsc:2|VAL-2369-OUT,EXP-2334-OUT;n:type:ShaderForge.SFN_Vector1,id:5061,x:31984,y:33121,varname:node_5061,prsc:2,v1:6;n:type:ShaderForge.SFN_Multiply,id:2334,x:32185,y:33087,varname:node_2334,prsc:2|A-5061-OUT,B-8653-OUT;n:type:ShaderForge.SFN_Multiply,id:2015,x:32512,y:32377,varname:node_2015,prsc:2|A-2717-RGB,B-5139-OUT,C-2053-RGB;n:type:ShaderForge.SFN_Tex2d,id:5005,x:31853,y:32603,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_5005,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-3930-OUT;n:type:ShaderForge.SFN_Multiply,id:6468,x:32243,y:32466,varname:node_6468,prsc:2|A-797-RGB,B-5005-RGB;n:type:ShaderForge.SFN_NormalVector,id:5132,x:32831,y:33719,prsc:2,pt:False;n:type:ShaderForge.SFN_Slider,id:7953,x:32674,y:33884,ptovrint:False,ptlb:Bump,ptin:_Bump,varname:node_7953,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.5,cur:0,max:0.5;n:type:ShaderForge.SFN_Multiply,id:8455,x:33041,y:33582,varname:node_8455,prsc:2|A-7267-OUT,B-5132-OUT,C-7953-OUT;n:type:ShaderForge.SFN_OneMinus,id:7267,x:32831,y:33582,varname:node_7267,prsc:2|IN-5670-OUT;n:type:ShaderForge.SFN_Add,id:2775,x:31563,y:33322,varname:node_2775,prsc:2|A-1033-V,B-9110-OUT;n:type:ShaderForge.SFN_Append,id:3930,x:31757,y:33282,varname:node_3930,prsc:2|A-1033-U,B-2775-OUT;n:type:ShaderForge.SFN_Slider,id:8775,x:31065,y:33430,ptovrint:False,ptlb:UVSpeed,ptin:_UVSpeed,varname:node_8775,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:0,max:2;n:type:ShaderForge.SFN_Time,id:5992,x:31222,y:33525,varname:node_5992,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9110,x:31410,y:33445,varname:node_9110,prsc:2|A-8775-OUT,B-5992-TSL,C-5089-OUT;n:type:ShaderForge.SFN_Vector1,id:5089,x:31222,y:33646,varname:node_5089,prsc:2,v1:9;n:type:ShaderForge.SFN_Multiply,id:8653,x:32185,y:32800,varname:node_8653,prsc:2|A-5005-A,B-2053-A;n:type:ShaderForge.SFN_Tex2d,id:2095,x:32185,y:33506,varname:node_2095,prsc:2,ntxv:0,isnm:False|UVIN-8528-UVOUT,TEX-6866-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:6866,x:31981,y:33435,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_6866,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:3304,x:31776,y:33625,varname:node_3304,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:8528,x:31981,y:33625,varname:node_8528,prsc:2,spu:1,spv:1|UVIN-3304-UVOUT,DIST-6259-OUT;n:type:ShaderForge.SFN_Vector1,id:6259,x:31776,y:33795,varname:node_6259,prsc:2,v1:0.5;proporder:797-5005-2717-6866-8775-7953;pass:END;sub:END;*/

Shader "Base/ParticleDisappear" {
    Properties {
        [HDR]_TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _MainTex ("MainTex", 2D) = "white" {}
        [HDR]_Emission ("Emission", Color) = (1,1,1,1)
        _Mask ("Mask", 2D) = "white" {}
        _UVSpeed ("UVSpeed", Range(-2, 2)) = 0
        _Bump ("Bump", Range(-0.5, 0.5)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="UniversalForward"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform float4 _TintColor;
            uniform float4 _Emission;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Bump;
            uniform float _UVSpeed;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_5992 = _Time;
                float2 node_3930 = float2(o.uv0.r,(o.uv0.g+(_UVSpeed*node_5992.r*9.0)));
                float4 node_9142 = tex2Dlod(_Mask,float4(TRANSFORM_TEX(node_3930, _Mask),0.0,0));
                float node_5670 = (dot(node_9142.rgb,float3(0.3,0.59,0.11))*node_9142.a);
                v.vertex.xyz += ((1.0 - node_5670)*v.normal*_Bump);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 node_5992 = _Time;
                float2 node_3930 = float2(i.uv0.r,(i.uv0.g+(_UVSpeed*node_5992.r*9.0)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_3930, _MainTex));
                float node_8653 = (_MainTex_var.a*i.vertexColor.a);
                float2 node_8528 = (i.uv0+0.5*float2(1,1));
                float4 node_2095 = tex2D(_Mask,TRANSFORM_TEX(node_8528, _Mask));
                clip(step(pow((1.0 - (node_8653-0.02)),(6.0*node_8653)),node_2095.r) - 0.5);
////// Lighting:
////// Emissive:
                float4 node_9142 = tex2D(_Mask,TRANSFORM_TEX(node_3930, _Mask));
                float node_5670 = (dot(node_9142.rgb,float3(0.3,0.59,0.11))*node_9142.a);
                float Mask = node_5670;
                float3 emissive = (_Emission.rgb*saturate((Mask-0.25))*i.vertexColor.rgb);
                float3 finalColor = emissive + (2.0*(_TintColor.rgb*_MainTex_var.rgb)*i.vertexColor.rgb);
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Bump;
            uniform float _UVSpeed;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_5992 = _Time;
                float2 node_3930 = float2(o.uv0.r,(o.uv0.g+(_UVSpeed*node_5992.r*9.0)));
                float4 node_9142 = tex2Dlod(_Mask,float4(TRANSFORM_TEX(node_3930, _Mask),0.0,0));
                float node_5670 = (dot(node_9142.rgb,float3(0.3,0.59,0.11))*node_9142.a);
                v.vertex.xyz += ((1.0 - node_5670)*v.normal*_Bump);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 node_5992 = _Time;
                float2 node_3930 = float2(i.uv0.r,(i.uv0.g+(_UVSpeed*node_5992.r*9.0)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_3930, _MainTex));
                float node_8653 = (_MainTex_var.a*i.vertexColor.a);
                float2 node_8528 = (i.uv0+0.5*float2(1,1));
                float4 node_2095 = tex2D(_Mask,TRANSFORM_TEX(node_8528, _Mask));
                clip(step(pow((1.0 - (node_8653-0.02)),(6.0*node_8653)),node_2095.r) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
