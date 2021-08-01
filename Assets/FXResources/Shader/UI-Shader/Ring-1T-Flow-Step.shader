// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "E3D/UI/Ring-1T-Flow-Step"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		[HDR]_MainColor("MainColor", Color) = (1,1,1,0)
		_ShapeStep("ShapeStep", Range( 0 , 1)) = 0.3647059
		_TailMax("Tail-Max", Range( 0 , 0.99)) = 0.3409177
		_Speed("Speed", Range( -1 , 1)) = 0
		_RoundMin("RoundMin", Range( -0.4 , 0.4)) = 0.03323776
		_Radius("Radius", Range( 0 , 1)) = 2.297945
		[Toggle(_REVERSE_ON)] _Reverse("Reverse", Float) = 0
		[Toggle(_REPEAT_ON)] _Repeat("Repeat", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		Blend One One , One One
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma shader_feature _REPEAT_ON
		#pragma shader_feature _REVERSE_ON
		#pragma exclude_renderers vulkan xbox360 xboxone ps4 psp2 n3ds wiiu 
		#pragma surface surf Unlit keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _MainTex;
		uniform float _Speed;
		uniform float _TailMax;
		uniform float _Radius;
		uniform float _RoundMin;
		uniform float _ShapeStep;
		uniform float4 _MainColor;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 break270 = ( i.uv_texcoord + -0.5 );
			float mulTime279 = _Time.y * _Speed;
			float temp_output_284_0 = (0.0 + (frac( ( ( atan2( break270.x , break270.y ) / 6.28318548202515 ) + mulTime279 ) ) - _TailMax) * (1.0 - 0.0) / (1.0 - _TailMax));
			#ifdef _REPEAT_ON
				float staticSwitch366 = frac( temp_output_284_0 );
			#else
				float staticSwitch366 = temp_output_284_0;
			#endif
			float temp_output_353_0 = (10.0 + (_Radius - 0.0) * (1.85 - 10.0) / (1.0 - 0.0));
			float temp_output_292_0 = saturate( (0.0 + (( 1.0 - length( (i.uv_texcoord*temp_output_353_0 + ( temp_output_353_0 * -0.5 )) ) ) - _RoundMin) * (1.0 - 0.0) / (0.4026131 - _RoundMin)) );
			#ifdef _REVERSE_ON
				float staticSwitch364 = ( 1.0 - temp_output_292_0 );
			#else
				float staticSwitch364 = temp_output_292_0;
			#endif
			float2 appendResult298 = (float2(saturate( staticSwitch366 ) , staticSwitch364));
			float4 tex2DNode288 = tex2D( _MainTex, appendResult298 );
			float temp_output_3_0_g1 = ( ( ( float4( 1,1,1,0 ) * tex2DNode288 ) * saturate( ( ( tex2DNode288.a + 0.0 ) / 0.4 ) ) ).r - _ShapeStep );
			o.Emission = ( saturate( ( temp_output_3_0_g1 / fwidth( temp_output_3_0_g1 ) ) ) * _MainColor ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17900
500;392;670;522;-10552.3;308.7547;1.6;True;False
Node;AmplifyShaderEditor.CommentaryNode;308;6631.411,403.4211;Inherit;False;3118.863;627.4049;Comment;16;268;266;267;270;277;269;286;276;279;278;280;284;303;285;365;366;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;367;6355.687,-253.3318;Inherit;False;1689.731;507.2371;Comment;10;354;356;355;353;352;343;351;348;337;342;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;266;6681.411,453.421;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;268;6742.331,730.2898;Inherit;False;Constant;_Float3;Float 3;3;0;Create;True;0;0;False;0;-0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;354;6508.882,79.39931;Inherit;False;Constant;_Float0;Float 0;7;0;Create;True;0;0;False;0;1.85;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;267;7045.331,547.2898;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;355;6519.781,10.49932;Inherit;False;Constant;_Float1;Float 1;7;0;Create;True;0;0;False;0;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;356;6405.687,-62.40068;Inherit;False;Property;_Radius;Radius;7;0;Create;True;0;0;False;0;2.297945;0.923;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;353;6841.497,-54.3808;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;270;7258.331,553.2898;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;352;6861.993,138.9052;Inherit;False;Constant;_Float7;Float 7;6;0;Create;True;0;0;False;0;-0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;343;6802.129,-203.3319;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ATan2OpNode;269;7537.809,552.8186;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;286;7548.974,876.044;Inherit;False;Property;_Speed;Speed;5;0;Create;True;0;0;False;0;0;-0.05;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;351;7099.192,-29.22011;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TauNode;277;7613.108,785.3483;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;348;7284.428,-146.3061;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT;3;False;2;FLOAT;-1.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;276;7849.115,551.3224;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;279;7875.642,876.6916;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LengthOpNode;337;7610.21,-146.9484;Inherit;True;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;278;8234.251,550.7651;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;320;8120.515,-186.0538;Inherit;False;1074.487;557.5088;Comment;4;289;291;292;326;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;285;8463.095,785.0218;Inherit;False;Property;_TailMax;Tail-Max;4;0;Create;True;0;0;False;0;0.3409177;0.5;0;0.99;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;280;8574.77,552.8636;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;291;8130.46,-7.275121;Inherit;False;Constant;_MainUVMax;MainUV-Max;6;0;Create;True;0;0;False;0;0.4026131;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;342;7847.418,-146.0946;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;326;8125.48,-89.62064;Inherit;False;Property;_RoundMin;RoundMin;6;0;Create;True;0;0;False;0;0.03323776;0.264;-0.4;0.4;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;284;8840.946,554.6045;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;289;8523.825,-144.0538;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;365;9147.445,655.7494;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;292;8988.634,-141.4704;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;366;9354.015,549.0516;Inherit;False;Property;_Repeat;Repeat;9;0;Create;True;0;0;False;0;0;0;1;True;;Toggle;2;Key0;Key1;Create;False;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;360;9267.791,102.3196;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;364;9562.729,75.19657;Inherit;False;Property;_Reverse;Reverse;8;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;319;9814.073,131.2118;Inherit;False;1477.488;617.0107;Comment;7;318;317;316;322;332;288;298;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SaturateNode;303;9610.115,554.424;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;298;9864.073,215.3521;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;288;10094.73,181.2118;Inherit;True;Property;_MainTex;MainTex;1;0;Create;True;0;0;False;0;-1;None;1f8506c9cd6c8f44db8b7d0cc124dd42;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;317;10576.36,505.4057;Inherit;False;Constant;_Float4;Float 4;8;0;Create;True;0;0;False;0;0.4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;322;10533.88,277.8611;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;316;10792.86,395.1206;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;332;10537.97,164.8966;Inherit;False;2;2;0;COLOR;1,1,1,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;318;11042.2,394.9409;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;323;11387.2,168.3982;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;369;11308.55,48.27017;Inherit;False;Property;_ShapeStep;ShapeStep;3;0;Create;True;0;0;False;0;0.3647059;0.029;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;368;11603.55,87.27017;Inherit;True;Step Antialiasing;-1;;1;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;325;11374.68,384.8722;Inherit;False;Property;_MainColor;MainColor;2;1;[HDR];Create;True;0;0;False;0;1,1,1,0;0.6145058,1.514,0.3896324,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;324;11872.65,228.0176;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;12068.05,155.0571;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;E3D/UI/Ring-1T-Flow-Step;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;7;d3d9;d3d11_9x;d3d11;glcore;gles;gles3;metal;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;244;1;False;-1;4;1;False;-1;1;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;267;0;266;0
WireConnection;267;1;268;0
WireConnection;353;0;356;0
WireConnection;353;3;355;0
WireConnection;353;4;354;0
WireConnection;270;0;267;0
WireConnection;269;0;270;0
WireConnection;269;1;270;1
WireConnection;351;0;353;0
WireConnection;351;1;352;0
WireConnection;348;0;343;0
WireConnection;348;1;353;0
WireConnection;348;2;351;0
WireConnection;276;0;269;0
WireConnection;276;1;277;0
WireConnection;279;0;286;0
WireConnection;337;0;348;0
WireConnection;278;0;276;0
WireConnection;278;1;279;0
WireConnection;280;0;278;0
WireConnection;342;0;337;0
WireConnection;284;0;280;0
WireConnection;284;1;285;0
WireConnection;289;0;342;0
WireConnection;289;1;326;0
WireConnection;289;2;291;0
WireConnection;365;0;284;0
WireConnection;292;0;289;0
WireConnection;366;1;284;0
WireConnection;366;0;365;0
WireConnection;360;0;292;0
WireConnection;364;1;292;0
WireConnection;364;0;360;0
WireConnection;303;0;366;0
WireConnection;298;0;303;0
WireConnection;298;1;364;0
WireConnection;288;1;298;0
WireConnection;322;0;288;4
WireConnection;316;0;322;0
WireConnection;316;1;317;0
WireConnection;332;1;288;0
WireConnection;318;0;316;0
WireConnection;323;0;332;0
WireConnection;323;1;318;0
WireConnection;368;1;369;0
WireConnection;368;2;323;0
WireConnection;324;0;368;0
WireConnection;324;1;325;0
WireConnection;0;2;324;0
ASEEND*/
//CHKSM=B9D7BC5CA9164FF8C0E8E4C440E548C8A5021E58