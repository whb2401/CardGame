// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Common/Dissolve_Normal" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_DissolveSrc ("DissolveSrc", 2D) = "white" {}
		_Amount ("Amount", Range (0, 1)) = 0.5
		_EdgeFactor ("EdgeFactor", Range (0.1, 0.5)) = 0.2
		
	}
	
	CGINCLUDE
	
		#include "UnityCG.cginc"

		sampler2D _MainTex;
		sampler2D _DissolveSrc;
		
		float  _Amount;
		float  _EdgeFactor;
		float4 _Color;
		
		uniform float4 _MainTex_ST;
				
		struct v2f {
			half4 pos : SV_POSITION;
			half2 uv : TEXCOORD0;		
		};

		v2f vert(appdata_full v)
		{
			v2f o;
			
			//v.vertex.yzx += v.texcoord1.xyy;
			o.uv.xy = TRANSFORM_TEX(v.texcoord.xy, _MainTex);
			o.pos = UnityObjectToClipPos (v.vertex);	
						
			return o; 
		}
		
		fixed4 frag( v2f i ) : COLOR
		{	
			float4 srcColor = tex2D(_MainTex, i.uv) * _Color;
			float factor = tex2D(_DissolveSrc, i.uv).r - _Amount;
			if (_Amount < 1.0)
			{
				srcColor.a = clamp(srcColor.a, 0, 1 - -factor / _EdgeFactor) * srcColor.a;
			}
			else
			{
				srcColor.a = 0;
			}
			return srcColor;
		}
	
	ENDCG
	
	SubShader {
		Tags { "Queue"="Transparent"  "Queue" = "Transparent" }
		Cull Off
		ZWrite Off
		Lighting off
		Blend SrcAlpha One
		
	Pass {
	
		CGPROGRAM
		
		#pragma vertex vert
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest 
		
		ENDCG
		 
		}
				
	} 
	FallBack Off
}
