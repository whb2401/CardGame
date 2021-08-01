// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "class/Standard_PBR"
{
	Properties
	{
		_Maintexture("Maintexture", 2D) = "white" {}
		[HDR]_Color("Color", Color) = (0,0,0,0)
		_Normaltexture("Normaltexture", 2D) = "bump" {}
		_texturemetallicsmoothness("texturemetallicsmoothness", 2D) = "white" {}
		_MetallicStr("MetallicStr", Float) = 0
		_SmoothnessStr("SmoothnessStr", Float) = 0
		_NormalStr("NormalStr", Float) = 0
		[HDR]_Emission("Emission", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _NormalStr;
		uniform sampler2D _Normaltexture;
		uniform float4 _Normaltexture_ST;
		uniform float4 _Color;
		uniform sampler2D _Maintexture;
		uniform float4 _Maintexture_ST;
		uniform float4 _Emission;
		uniform sampler2D _texturemetallicsmoothness;
		uniform float4 _texturemetallicsmoothness_ST;
		uniform float _MetallicStr;
		uniform float _SmoothnessStr;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normaltexture = i.uv_texcoord * _Normaltexture_ST.xy + _Normaltexture_ST.zw;
			o.Normal = UnpackScaleNormal( tex2D( _Normaltexture, uv_Normaltexture ), _NormalStr );
			float2 uv_Maintexture = i.uv_texcoord * _Maintexture_ST.xy + _Maintexture_ST.zw;
			o.Albedo = ( _Color * tex2D( _Maintexture, uv_Maintexture ) ).rgb;
			float2 uv_texturemetallicsmoothness = i.uv_texcoord * _texturemetallicsmoothness_ST.xy + _texturemetallicsmoothness_ST.zw;
			float4 tex2DNode9 = tex2D( _texturemetallicsmoothness, uv_texturemetallicsmoothness );
			o.Emission = ( _Emission * tex2DNode9.r ).rgb;
			o.Metallic = ( tex2DNode9.g * _MetallicStr );
			o.Smoothness = ( tex2DNode9.b * _SmoothnessStr );
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
3088;101;1923;899;498.5548;978.0087;2.125549;True;False
Node;AmplifyShaderEditor.SamplerNode;2;552.3478,-583.4452;Inherit;True;Property;_Maintexture;Maintexture;0;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;4;618.2941,-771.8969;Float;False;Property;_Color;Color;1;1;[HDR];Create;True;0;0;False;0;0,0,0,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;16;515.2037,-343.1426;Float;False;Property;_NormalStr;NormalStr;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;17;564.4935,-197.5325;Float;False;Property;_Emission;Emission;7;1;[HDR];Create;True;0;0;False;0;0,0,0,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;12;827.421,10.38399;Float;False;Property;_MetallicStr;MetallicStr;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;855.3692,180.0059;Float;False;Property;_SmoothnessStr;SmoothnessStr;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;9;318.3932,250.8058;Inherit;True;Property;_texturemetallicsmoothness;texturemetallicsmoothness;3;0;Create;True;0;0;False;0;-1;None;d4082d57cdf4ed4408fff8560d1eba5a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;955.3048,-541.2065;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;7;742.3633,-401.2933;Inherit;True;Property;_Normaltexture;Normaltexture;2;0;Create;True;0;0;False;0;-1;None;36086d96c42321c41beb50f05955f40a;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;860.9539,-208.9841;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;1048.169,-77.83141;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;1035.373,64.02557;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1482.63,-241.1361;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;class/Standard_PBR;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;4;0
WireConnection;5;1;2;0
WireConnection;7;5;16;0
WireConnection;10;0;17;0
WireConnection;10;1;9;1
WireConnection;13;0;9;2
WireConnection;13;1;12;0
WireConnection;14;0;9;3
WireConnection;14;1;15;0
WireConnection;0;0;5;0
WireConnection;0;1;7;0
WireConnection;0;2;10;0
WireConnection;0;3;13;0
WireConnection;0;4;14;0
ASEEND*/
//CHKSM=DC9B3AE18D81B6A6C1DD053A6B35628EC2A442C2