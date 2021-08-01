Shader "Custom/ActorMask"
{
	Properties 
	{
		_MaskColor ("Mask Color", Color) = (0.0, 0.0, 0.0, 0.0)
	}
	
	SubShader 
	{
		//Tags{ "Queue" = "Geometry+1" "IgnoreProjector" = "True" "RenderType" = "Opaque" }
		Tags{ "Queue" = "AlphaTest+1" "IgnoreProjector" = "True" "RenderType" = "Opaque" }

		Blend SrcAlpha OneMinusSrcAlpha
		Blend SrcAlpha One
		Cull Back
		Lighting Off
		ZWrite Off
		Fog{ Mode Off }
		//ZTest Always

		Pass 
		{		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"

			fixed4 _MaskColor;
			
			struct appdata_t 
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f_t 
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};			

			v2f_t vert (appdata_t v)
			{
				v2f_t o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
				return o;
			}
	
			fixed4 frag (v2f_t i) : COLOR
			{			
				fixed4 col =  _MaskColor;
				return col;
			}
			ENDCG 
		}
	} 		

	Fallback "Mobile/Unlit (Supports Lightmap)"
}
