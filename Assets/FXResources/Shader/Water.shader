// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32422,y:32296,varname:node_3138,prsc:2|emission-9464-OUT,voffset-1396-OUT;n:type:ShaderForge.SFN_Time,id:1407,x:29949,y:32307,varname:node_1407,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7472,x:30159,y:32327,varname:node_7472,prsc:2|A-1407-T,B-5584-OUT;n:type:ShaderForge.SFN_Vector1,id:5584,x:29936,y:32430,varname:node_5584,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Tex2d,id:3292,x:31193,y:32134,varname:node_3292,prsc:2,ntxv:0,isnm:False|UVIN-657-OUT,TEX-8050-TEX;n:type:ShaderForge.SFN_TexCoord,id:7934,x:30701,y:32117,varname:node_7934,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:657,x:30944,y:32173,varname:node_657,prsc:2|A-7934-UVOUT,B-70-OUT;n:type:ShaderForge.SFN_Append,id:70,x:30727,y:32348,varname:node_70,prsc:2|A-5898-OUT,B-7472-OUT;n:type:ShaderForge.SFN_Vector1,id:5898,x:30391,y:32244,varname:node_5898,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:5851,x:30342,y:32590,varname:node_5851,prsc:2|A-1407-T,B-6280-OUT;n:type:ShaderForge.SFN_Vector1,id:6280,x:30140,y:32609,varname:node_6280,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Append,id:4282,x:30727,y:32647,varname:node_4282,prsc:2|A-5851-OUT,B-5851-OUT;n:type:ShaderForge.SFN_TexCoord,id:8476,x:30727,y:32481,varname:node_8476,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:5906,x:30989,y:32574,varname:node_5906,prsc:2|A-8476-UVOUT,B-4282-OUT;n:type:ShaderForge.SFN_Blend,id:8220,x:31450,y:32299,varname:node_8220,prsc:2,blmd:10,clmp:True|SRC-3292-R,DST-2142-R;n:type:ShaderForge.SFN_Lerp,id:4578,x:31722,y:32280,varname:node_4578,prsc:2|A-4545-OUT,B-8220-OUT,T-4013-OUT;n:type:ShaderForge.SFN_Vector1,id:4545,x:31556,y:32173,varname:node_4545,prsc:2,v1:0;n:type:ShaderForge.SFN_Slider,id:4013,x:31416,y:32548,ptovrint:False,ptlb:Bump,ptin:_Bump,varname:node_4013,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:1396,x:31931,y:32151,varname:node_1396,prsc:2|A-5303-OUT,B-4578-OUT;n:type:ShaderForge.SFN_NormalVector,id:5303,x:31683,y:32005,prsc:2,pt:False;n:type:ShaderForge.SFN_Tex2dAsset,id:8050,x:31008,y:32386,ptovrint:False,ptlb:BumpTex,ptin:_BumpTex,varname:node_8050,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2142,x:31227,y:32470,varname:node_2142,prsc:2,ntxv:0,isnm:False|UVIN-5906-OUT,TEX-8050-TEX;n:type:ShaderForge.SFN_Tex2d,id:5863,x:31232,y:32962,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_5863,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-4530-OUT;n:type:ShaderForge.SFN_Multiply,id:1837,x:30554,y:33036,varname:node_1837,prsc:2|A-3826-T,B-9305-OUT;n:type:ShaderForge.SFN_Time,id:3826,x:30336,y:32901,varname:node_3826,prsc:2;n:type:ShaderForge.SFN_Slider,id:9305,x:30221,y:33096,ptovrint:False,ptlb:VSpeed,ptin:_VSpeed,varname:node_9305,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:1,max:10;n:type:ShaderForge.SFN_Append,id:5871,x:30737,y:32997,varname:node_5871,prsc:2|A-3107-OUT,B-1837-OUT;n:type:ShaderForge.SFN_Vector1,id:3107,x:30567,y:32935,varname:node_3107,prsc:2,v1:0;n:type:ShaderForge.SFN_TexCoord,id:7192,x:30737,y:32799,varname:node_7192,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:4530,x:30842,y:32978,varname:node_4530,prsc:2|A-7192-UVOUT,B-5871-OUT;n:type:ShaderForge.SFN_Fresnel,id:8194,x:31274,y:33178,varname:node_8194,prsc:2|EXP-8694-OUT;n:type:ShaderForge.SFN_Vector1,id:8694,x:31083,y:33196,varname:node_8694,prsc:2,v1:2.5;n:type:ShaderForge.SFN_Color,id:393,x:31244,y:32767,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_393,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:4370,x:31525,y:32815,varname:node_4370,prsc:2|A-393-RGB,B-5863-RGB;n:type:ShaderForge.SFN_Multiply,id:7094,x:31636,y:33206,varname:node_7094,prsc:2|A-8194-OUT,B-393-RGB,C-3745-OUT;n:type:ShaderForge.SFN_Tex2d,id:6981,x:31314,y:33341,varname:node_6981,prsc:2,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-3645-OUT,TEX-2779-TEX;n:type:ShaderForge.SFN_Multiply,id:3645,x:31033,y:33307,varname:node_3645,prsc:2|A-4530-OUT,B-5654-OUT;n:type:ShaderForge.SFN_Vector2,id:5654,x:30837,y:33325,varname:node_5654,prsc:2,v1:1,v2:0.5;n:type:ShaderForge.SFN_Tex2dAsset,id:2779,x:31129,y:33480,ptovrint:False,ptlb:WaterWenli,ptin:_WaterWenli,varname:node_2779,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8651,x:31314,y:33561,varname:node_8651,prsc:2,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-601-OUT,TEX-2779-TEX;n:type:ShaderForge.SFN_Multiply,id:601,x:31109,y:33736,varname:node_601,prsc:2|A-4530-OUT,B-7442-OUT,C-7554-OUT;n:type:ShaderForge.SFN_Vector2,id:7442,x:30881,y:33724,varname:node_7442,prsc:2,v1:1,v2:0.5;n:type:ShaderForge.SFN_Vector1,id:7554,x:30914,y:33843,varname:node_7554,prsc:2,v1:1.3;n:type:ShaderForge.SFN_Multiply,id:3745,x:31487,y:33465,varname:node_3745,prsc:2|A-6981-R,B-8651-R;n:type:ShaderForge.SFN_Add,id:9464,x:31791,y:32902,varname:node_9464,prsc:2|A-4370-OUT,B-7094-OUT;proporder:4013-8050-5863-9305-393-2779;pass:END;sub:END;*/

Shader "BaseSG/Water_SF" {
    Properties {
        _Bump ("Bump", Range(-1, 1)) = 0
        _BumpTex ("BumpTex", 2D) = "white" {}
        _MainTex ("MainTex", 2D) = "white" {}
        _VSpeed ("VSpeed", Range(-10, 10)) = 1
        [HDR]_Color ("Color", Color) = (0.5,0.5,0.5,1)
        _WaterWenli ("WaterWenli", 2D) = "white" {}
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
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform float _Bump;
            uniform sampler2D _BumpTex; uniform float4 _BumpTex_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _VSpeed;
            uniform float4 _Color;
            uniform sampler2D _WaterWenli; uniform float4 _WaterWenli_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_1407 = _Time;
                float2 node_657 = (o.uv0+float2(0.0,(node_1407.g*0.5)));
                float4 node_3292 = tex2Dlod(_BumpTex,float4(TRANSFORM_TEX(node_657, _BumpTex),0.0,0));
                float node_5851 = (node_1407.g*0.2);
                float2 node_5906 = (o.uv0+float2(node_5851,node_5851));
                float4 node_2142 = tex2Dlod(_BumpTex,float4(TRANSFORM_TEX(node_5906, _BumpTex),0.0,0));
                v.vertex.xyz += (v.normal*lerp(0.0,saturate(( node_2142.r > 0.5 ? (1.0-(1.0-2.0*(node_2142.r-0.5))*(1.0-node_3292.r)) : (2.0*node_2142.r*node_3292.r) )),_Bump));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_3826 = _Time;
                float2 node_4530 = (i.uv0+float2(0.0,(node_3826.g*_VSpeed)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_4530, _MainTex));
                float2 node_3645 = (node_4530*float2(1,0.5));
                float4 node_6981 = tex2D(_WaterWenli,TRANSFORM_TEX(node_3645, _WaterWenli));
                float2 node_601 = (node_4530*float2(1,0.5)*1.3);
                float4 node_8651 = tex2D(_WaterWenli,TRANSFORM_TEX(node_601, _WaterWenli));
                float3 emissive = ((_Color.rgb*_MainTex_var.rgb)+(pow(1.0-max(0,dot(normalDirection, viewDirection)),2.5)*_Color.rgb*(node_6981.r*node_8651.r)));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform float _Bump;
            uniform sampler2D _BumpTex; uniform float4 _BumpTex_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_1407 = _Time;
                float2 node_657 = (o.uv0+float2(0.0,(node_1407.g*0.5)));
                float4 node_3292 = tex2Dlod(_BumpTex,float4(TRANSFORM_TEX(node_657, _BumpTex),0.0,0));
                float node_5851 = (node_1407.g*0.2);
                float2 node_5906 = (o.uv0+float2(node_5851,node_5851));
                float4 node_2142 = tex2Dlod(_BumpTex,float4(TRANSFORM_TEX(node_5906, _BumpTex),0.0,0));
                v.vertex.xyz += (v.normal*lerp(0.0,saturate(( node_2142.r > 0.5 ? (1.0-(1.0-2.0*(node_2142.r-0.5))*(1.0-node_3292.r)) : (2.0*node_2142.r*node_3292.r) )),_Bump));
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
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
