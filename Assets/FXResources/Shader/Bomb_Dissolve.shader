// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2768,x:36079,y:34211,varname:node_2768,prsc:2|emission-1706-OUT,alpha-7209-OUT,clip-2286-OUT;n:type:ShaderForge.SFN_Tex2d,id:2517,x:34326,y:34071,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_2517,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:8949,x:34789,y:34214,ptovrint:False,ptlb:outsideColor,ptin:_outsideColor,varname:node_8949,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:4364,x:34900,y:34035,ptovrint:False,ptlb:InnerColor,ptin:_InnerColor,varname:node_4364,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Lerp,id:7598,x:35393,y:33818,varname:node_7598,prsc:2|A-9072-OUT,B-4364-RGB,T-428-OUT;n:type:ShaderForge.SFN_TexCoord,id:4513,x:34215,y:34369,varname:node_4513,prsc:2,uv:1,uaff:True;n:type:ShaderForge.SFN_Step,id:428,x:34648,y:34019,varname:node_428,prsc:2|A-4513-Z,B-2517-R;n:type:ShaderForge.SFN_Multiply,id:3537,x:34730,y:34585,varname:node_3537,prsc:2|A-2517-R,B-2383-OUT;n:type:ShaderForge.SFN_Vector1,id:2383,x:34487,y:34547,varname:node_2383,prsc:2,v1:1.1;n:type:ShaderForge.SFN_Step,id:8103,x:34892,y:34505,varname:node_8103,prsc:2|A-4513-Z,B-3537-OUT;n:type:ShaderForge.SFN_Lerp,id:9072,x:35224,y:34202,varname:node_9072,prsc:2|A-8949-RGB,B-1757-RGB,T-8103-OUT;n:type:ShaderForge.SFN_Color,id:1757,x:34662,y:34355,ptovrint:False,ptlb:SecondColor,ptin:_SecondColor,varname:node_1757,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7700,x:35045,y:34153,ptovrint:False,ptlb:Opa,ptin:_Opa,varname:node_7700,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Step,id:2286,x:35227,y:34645,varname:node_2286,prsc:2|A-7700-R,B-4513-U;n:type:ShaderForge.SFN_VertexColor,id:7989,x:35389,y:34265,varname:node_7989,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1706,x:35624,y:34153,varname:node_1706,prsc:2|A-7598-OUT,B-7989-RGB;n:type:ShaderForge.SFN_Multiply,id:7209,x:35686,y:34452,varname:node_7209,prsc:2|A-7989-A,B-378-R;n:type:ShaderForge.SFN_Tex2d,id:378,x:35501,y:34524,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_378,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;proporder:2517-8949-4364-1757-7700-378;pass:END;sub:END;*/

Shader "Common/particle/Bomb_Dissolve" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        [HDR]_outsideColor ("outsideColor", Color) = (0.5,0.5,0.5,1)
        [HDR]_InnerColor ("InnerColor", Color) = (0.5,0.5,0.5,1)
        [HDR]_SecondColor ("SecondColor", Color) = (0.5,0.5,0.5,1)
        _Opa ("Opa", 2D) = "white" {}
        _Opacity ("Opacity", 2D) = "white" {}
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
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _outsideColor;
            uniform float4 _InnerColor;
            uniform float4 _SecondColor;
            uniform sampler2D _Opa; uniform float4 _Opa_ST;
            uniform sampler2D _Opacity; uniform float4 _Opacity_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 texcoord1 : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 uv1 : TEXCOORD1;
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
            float4 frag(VertexOutput i) : COLOR {
                float4 _Opa_var = tex2D(_Opa,TRANSFORM_TEX(i.uv0, _Opa));
                float node_2286 = step(_Opa_var.r,i.uv1.r);
                clip(node_2286 - 0.5);
////// Lighting:
////// Emissive:
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 emissive = (lerp(lerp(_outsideColor.rgb,_SecondColor.rgb,step(i.uv1.b,(_Diffuse_var.r*1.1))),_InnerColor.rgb,step(i.uv1.b,_Diffuse_var.r))*i.vertexColor.rgb);
                float3 finalColor = emissive;
                float4 _Opacity_var = tex2D(_Opacity,TRANSFORM_TEX(i.uv0, _Opacity));
                return fixed4(finalColor,(i.vertexColor.a*_Opacity_var.r));
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
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
            uniform sampler2D _Opa; uniform float4 _Opa_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 uv1 : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _Opa_var = tex2D(_Opa,TRANSFORM_TEX(i.uv0, _Opa));
                float node_2286 = step(_Opa_var.r,i.uv1.r);
                clip(node_2286 - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
