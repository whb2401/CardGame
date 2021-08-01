// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2768,x:34760,y:33898,varname:node_2768,prsc:2|emission-7315-OUT,alpha-2015-OUT;n:type:ShaderForge.SFN_Tex2d,id:5547,x:33754,y:34051,ptovrint:False,ptlb:ATex,ptin:_ATex,varname:node_5547,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9316,x:33739,y:34311,ptovrint:False,ptlb:dissolve,ptin:_dissolve,varname:node_9316,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:1767,x:33739,y:34474,varname:node_1767,prsc:2,uv:1,uaff:False;n:type:ShaderForge.SFN_Step,id:836,x:33959,y:34372,varname:node_836,prsc:2|A-9316-R,B-1767-U;n:type:ShaderForge.SFN_Multiply,id:8253,x:34119,y:34228,varname:node_8253,prsc:2|A-5547-R,B-836-OUT;n:type:ShaderForge.SFN_FaceSign,id:1875,x:33866,y:33914,varname:node_1875,prsc:2,fstp:0;n:type:ShaderForge.SFN_Color,id:1139,x:33829,y:33722,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1139,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:6591,x:34230,y:33821,varname:node_6591,prsc:2|A-2051-OUT,B-1139-RGB;n:type:ShaderForge.SFN_ValueProperty,id:2051,x:34050,y:33702,ptovrint:False,ptlb:Value,ptin:_Value,varname:node_2051,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.6;n:type:ShaderForge.SFN_Lerp,id:4324,x:34530,y:33904,varname:node_4324,prsc:2|A-6591-OUT,B-1139-RGB,T-1875-VFACE;n:type:ShaderForge.SFN_VertexColor,id:1104,x:34072,y:34065,varname:node_1104,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7315,x:34518,y:34005,varname:node_7315,prsc:2|A-4324-OUT,B-1104-RGB;n:type:ShaderForge.SFN_Multiply,id:2015,x:34456,y:34240,varname:node_2015,prsc:2|A-1104-A,B-8253-OUT;proporder:5547-9316-1139-2051;pass:END;sub:END;*/

Shader "Common/particle/Water_Double" {
    Properties {
        _ATex ("ATex", 2D) = "white" {}
        _dissolve ("dissolve", 2D) = "white" {}
        [HDR]_Color ("Color", Color) = (0.5,0.5,0.5,1)
        _Value ("Value", Float ) = 0.6
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _ATex; uniform float4 _ATex_ST;
            uniform sampler2D _dissolve; uniform float4 _dissolve_ST;
            uniform float4 _Color;
            uniform float _Value;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float3 emissive = (lerp((_Value*_Color.rgb),_Color.rgb,isFrontFace)*i.vertexColor.rgb);
                float3 finalColor = emissive;
                float4 _ATex_var = tex2D(_ATex,TRANSFORM_TEX(i.uv0, _ATex));
                float4 _dissolve_var = tex2D(_dissolve,TRANSFORM_TEX(i.uv0, _dissolve));
                return fixed4(finalColor,(i.vertexColor.a*(_ATex_var.r*step(_dissolve_var.r,i.uv1.r))));
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
