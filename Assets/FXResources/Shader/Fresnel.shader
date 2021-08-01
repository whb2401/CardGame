// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33853,y:32556,varname:node_9361,prsc:2|custl-3759-OUT,alpha-8238-OUT;n:type:ShaderForge.SFN_Color,id:274,x:32785,y:32535,ptovrint:False,ptlb:node_274,ptin:_node_274,varname:node_274,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:5174,x:33211,y:33036,ptovrint:False,ptlb:node_5174,ptin:_node_5174,varname:node_5174,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:5778,x:32727,y:32730,ptovrint:False,ptlb:node_5778,ptin:_node_5778,varname:node_5778,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Add,id:3485,x:33211,y:32625,varname:node_3485,prsc:2|A-274-RGB,B-4926-OUT;n:type:ShaderForge.SFN_Fresnel,id:2547,x:32674,y:33019,varname:node_2547,prsc:2|EXP-5893-OUT;n:type:ShaderForge.SFN_Multiply,id:4926,x:32949,y:33021,varname:node_4926,prsc:2|A-5778-RGB,B-2547-OUT,C-7444-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5893,x:32462,y:33108,ptovrint:False,ptlb:node_5893,ptin:_node_5893,varname:node_5893,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:7444,x:32759,y:33196,ptovrint:False,ptlb:node_7444,ptin:_node_7444,varname:node_7444,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Tex2d,id:6525,x:33211,y:32796,ptovrint:False,ptlb:Maintexture,ptin:_Maintexture,varname:node_6525,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:3759,x:33555,y:32565,varname:node_3759,prsc:2|A-6525-RGB,B-3485-OUT;n:type:ShaderForge.SFN_Multiply,id:8238,x:33612,y:32937,varname:node_8238,prsc:2|A-6525-A,B-5174-A;proporder:5174-274-5778-5893-7444-6525;pass:END;sub:END;*/

Shader "Test/Fresnel" {
    Properties {
        [HDR]_node_5174 ("node_5174", Color) = (0.5,0.5,0.5,1)
        [HDR]_node_274 ("node_274", Color) = (0.5,0.5,0.5,1)
        [HDR]_node_5778 ("node_5778", Color) = (0.5,0.5,0.5,1)
        _node_5893 ("node_5893", Float ) = 0
        _node_7444 ("node_7444", Float ) = 0
        _Maintexture ("Maintexture", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
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
            uniform float4 _node_274;
            uniform float4 _node_5174;
            uniform float4 _node_5778;
            uniform float _node_5893;
            uniform float _node_7444;
            uniform sampler2D _Maintexture; uniform float4 _Maintexture_ST;
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
                float4 _Maintexture_var = tex2D(_Maintexture,TRANSFORM_TEX(i.uv0, _Maintexture));
                float3 finalColor = (_Maintexture_var.rgb*(_node_274.rgb+(_node_5778.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_5893)*_node_7444)));
                return fixed4(finalColor,(_Maintexture_var.a*_node_5174.a));
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
