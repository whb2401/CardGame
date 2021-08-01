// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33494,y:32744,varname:node_9361,prsc:2|custl-8098-OUT,alpha-1049-OUT;n:type:ShaderForge.SFN_Tex2d,id:5864,x:32763,y:32808,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_5864,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:5867,x:32763,y:32643,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_5867,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:4474,x:33156,y:32568,varname:node_4474,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:8098,x:33156,y:32631,varname:node_8098,prsc:2|A-4474-OUT,B-5867-RGB,C-5864-RGB;n:type:ShaderForge.SFN_Tex2d,id:3041,x:32228,y:33081,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_3041,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-3251-OUT;n:type:ShaderForge.SFN_Desaturate,id:9272,x:32607,y:33033,varname:node_9272,prsc:2|COL-3041-RGB;n:type:ShaderForge.SFN_Multiply,id:1049,x:33030,y:33059,varname:node_1049,prsc:2|A-5864-A,B-9272-OUT,C-3041-A;n:type:ShaderForge.SFN_TexCoord,id:5083,x:31381,y:32999,varname:node_5083,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:4878,x:31768,y:32953,varname:node_4878,prsc:2,frmn:0,frmx:1,tomn:-6,tomx:7|IN-4571-OUT;n:type:ShaderForge.SFN_Vector2,id:3913,x:31583,y:33228,varname:node_3913,prsc:2,v1:0.5,v2:0.5;n:type:ShaderForge.SFN_Lerp,id:3251,x:32000,y:33190,varname:node_3251,prsc:2|A-4878-OUT,B-3913-OUT,T-2490-OUT;n:type:ShaderForge.SFN_Slider,id:9579,x:30989,y:33416,ptovrint:False,ptlb:ScaleUV,ptin:_ScaleUV,varname:node_9579,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Set,id:5682,x:33214,y:32849,varname:Noise,prsc:2|IN-1676-OUT;n:type:ShaderForge.SFN_Get,id:6800,x:31163,y:33136,varname:node_6800,prsc:2|IN-5682-OUT;n:type:ShaderForge.SFN_Append,id:1676,x:33030,y:32849,varname:node_1676,prsc:2|A-5864-R,B-5864-G;n:type:ShaderForge.SFN_Lerp,id:4571,x:31569,y:32971,varname:node_4571,prsc:2|A-5083-UVOUT,B-6800-OUT,T-6476-OUT;n:type:ShaderForge.SFN_Slider,id:6476,x:31224,y:33211,ptovrint:False,ptlb:MaskDistort,ptin:_MaskDistort,varname:node_6476,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:0.1;n:type:ShaderForge.SFN_Time,id:5252,x:30989,y:33540,varname:node_5252,prsc:2;n:type:ShaderForge.SFN_Multiply,id:903,x:31204,y:33562,varname:node_903,prsc:2|A-5252-TSL,B-1855-OUT;n:type:ShaderForge.SFN_Slider,id:1855,x:30848,y:33714,ptovrint:False,ptlb:AutoSpeed,ptin:_AutoSpeed,varname:node_1855,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3.410256,max:6;n:type:ShaderForge.SFN_Frac,id:1106,x:31378,y:33562,varname:node_1106,prsc:2|IN-903-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:3783,x:31570,y:33550,ptovrint:False,ptlb:AutoScale,ptin:_AutoScale,varname:node_3783,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-9579-OUT,B-1106-OUT;n:type:ShaderForge.SFN_OneMinus,id:1280,x:31771,y:33537,varname:node_1280,prsc:2|IN-3783-OUT;n:type:ShaderForge.SFN_Power,id:2490,x:32192,y:33441,varname:node_2490,prsc:2|VAL-3783-OUT,EXP-7283-OUT;n:type:ShaderForge.SFN_Multiply,id:7283,x:31957,y:33499,varname:node_7283,prsc:2|A-1280-OUT,B-7207-OUT;n:type:ShaderForge.SFN_Vector1,id:7207,x:31771,y:33695,varname:node_7207,prsc:2,v1:0.2;proporder:5867-5864-3041-6476-9579-3783-1855;pass:END;sub:END;*/

Shader "Base/ScaleUV" {
    Properties {
        [HDR]_Color ("Color", Color) = (0.5,0.5,0.5,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _MaskDistort ("MaskDistort", Range(0, 0.1)) = 0
        _ScaleUV ("ScaleUV", Range(0, 1)) = 0
        [MaterialToggle] _AutoScale ("AutoScale", Float ) = 0
        _AutoSpeed ("AutoSpeed", Range(0, 6)) = 3.410256
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
                "LightMode"="Forward"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _Color;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float _ScaleUV;
            uniform float _MaskDistort;
            uniform float _AutoSpeed;
            uniform fixed _AutoScale;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 finalColor = (2.0*_Color.rgb*_MainTex_var.rgb);
                float2 Noise = float2(_MainTex_var.r,_MainTex_var.g);
                float4 node_5252 = _Time;
                float _AutoScale_var = lerp( _ScaleUV, frac((node_5252.r*_AutoSpeed)), _AutoScale );
                float2 node_3251 = lerp((lerp(i.uv0,Noise,_MaskDistort)*13.0+-6.0),float2(0.5,0.5),pow(_AutoScale_var,((1.0 - _AutoScale_var)*0.2)));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(node_3251, _Mask));
                fixed4 finalRGBA = fixed4(finalColor,(_MainTex_var.a*dot(_Mask_var.rgb,float3(0.3,0.59,0.11))*_Mask_var.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
