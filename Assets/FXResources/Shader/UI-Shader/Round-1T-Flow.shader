// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "E3D/UI/Round-1T-Flow"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		[HDR]_MainColor("MainColor", Color) = (1,1,1,0)
		_TailMax("Tail-Max", Range( 0 , 0.9)) = 0.3409177
		_Speed("Speed", Range( -1 , 1)) = 0
		_RoundMin("RoundMin", Range( 0 , 0.1)) = 0.04
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
		#pragma exclude_renderers vulkan xbox360 xboxone ps4 psp2 n3ds wiiu 
		#pragma surface surf Unlit keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _RoundMin;
		uniform sampler2D _MainTex;
		uniform float _Speed;
		uniform float _TailMax;
		uniform float4 _MainColor;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 temp_output_243_0 = ( abs( ( i.uv_texcoord + -0.5 ) ) + -0.2364886 );
			float temp_output_292_0 = saturate( (0.0 + (( saturate( length( saturate( temp_output_243_0 ) ) ) / 5.0 ) - _RoundMin) * (1.0 - 0.0) / (0.0 - _RoundMin)) );
			float2 break270 = ( i.uv_texcoord + -0.5 );
			float mulTime279 = _Time.y * _Speed;
			float2 appendResult298 = (float2(saturate( (0.0 + (frac( ( ( atan2( break270.x , break270.y ) / 6.28318548202515 ) + mulTime279 ) ) - _TailMax) * (1.0 - 0.0) / (1.0 - _TailMax)) ) , temp_output_292_0));
			float4 tex2DNode288 = tex2D( _MainTex, appendResult298 );
			o.Emission = ( ( ( ( fwidth( temp_output_292_0 ) * 22.0 ) * tex2DNode288 ) * saturate( ( ( tex2DNode288.a + 0.0 ) / 0.4 ) ) ) * _MainColor ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17900
415;577;1062;456;-6468.113;-140.0664;2.144126;True;False
Node;AmplifyShaderEditor.CommentaryNode;265;5915.874,-663.4014;Inherit;False;2552.492;1035.032;Comment;13;235;237;236;242;244;243;247;245;249;255;253;238;239;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;308;6086.711,428.1832;Inherit;False;3118.863;627.4049;Comment;15;268;266;267;270;277;269;286;276;279;278;280;284;303;307;285;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;235;5965.874,-454.0396;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;237;6001.323,-146.0268;Inherit;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;False;0;-0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;266;6136.711,478.1832;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;268;6197.631,755.0519;Inherit;False;Constant;_Float3;Float 3;3;0;Create;True;0;0;False;0;-0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;236;6258.406,-397.7437;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;267;6500.631,572.0519;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;244;6438.284,-27.20335;Inherit;False;Constant;_Float1;Float 1;1;0;Create;True;0;0;False;0;-0.2364886;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;242;6479.284,-291.2033;Inherit;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.BreakToComponentsNode;270;6713.631,578.0519;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;243;6746.858,-212.8718;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ATan2OpNode;269;6993.109,577.5807;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;286;7260.776,841.7886;Inherit;False;Property;_Speed;Speed;4;0;Create;True;0;0;False;0;0;-0.62;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;247;6981.436,-206.5942;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TauNode;277;7093.379,857.7782;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;279;7600.596,845.7244;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LengthOpNode;245;7142.48,-218.1834;Inherit;True;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;276;7304.415,576.0845;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;278;7797.012,584.9843;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;255;7188.948,20.15644;Inherit;False;Constant;_Round;Round;3;0;Create;True;0;0;False;0;5;5;0;12;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;320;8589.131,-193.3589;Inherit;False;1074.487;557.5088;Comment;4;289;291;292;326;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SaturateNode;249;7345.898,-232.5213;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;285;7997.844,850.8286;Inherit;False;Property;_TailMax;Tail-Max;3;0;Create;True;0;0;False;0;0.3409177;0;0;0.9;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;280;8108.405,613.5797;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;253;7754.509,-189.1659;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;291;8599.076,-14.58022;Inherit;False;Constant;_MainUVMax;MainUV-Max;6;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;326;8594.096,-96.92578;Inherit;False;Property;_RoundMin;RoundMin;5;0;Create;True;0;0;False;0;0.04;0.053;0;0.1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;284;8373.478,639.0925;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;289;8992.431,-151.3589;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;292;9457.239,-148.7755;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;303;8904.039,645.3771;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;319;9814.073,131.2118;Inherit;False;1662.612;771.2808;Comment;7;298;288;316;322;317;318;332;;1,1,1,1;0;0
Node;AmplifyShaderEditor.DynamicAppendNode;298;9864.073,215.3521;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;288;10093.73,181.2118;Inherit;True;Property;_MainTex;MainTex;1;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;322;10467.88,286.8611;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;317;10475.95,590.4655;Inherit;False;Constant;_Float4;Float 4;8;0;Create;True;0;0;False;0;0.4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FWidthOpNode;327;9766.259,-148.251;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;331;9769.553,58.2571;Inherit;False;Constant;_Float2;Float 2;6;0;Create;True;0;0;False;0;22;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;330;10104.36,-149.406;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;316;10717.95,453.2722;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;318;11108.9,448.8438;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;332;10538.97,164.8966;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;325;11627.73,406.2721;Inherit;False;Property;_MainColor;MainColor;2;1;[HDR];Create;True;0;0;False;0;1,1,1,0;1.73,1.324345,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;323;11634.79,178.7982;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;239;7331.862,-613.4014;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;238;7049.439,-552.4139;Inherit;True;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;324;11917.24,178.4176;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;307;8364.01,858.0146;Inherit;False;Constant;_Float6;Float 6;7;0;Create;True;0;0;False;0;0.7176471;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;12100.22,133.9451;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;E3D/UI/Round-1T-Flow;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;7;d3d9;d3d11_9x;d3d11;glcore;gles;gles3;metal;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;244;1;False;-1;4;1;False;-1;1;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;236;0;235;0
WireConnection;236;1;237;0
WireConnection;267;0;266;0
WireConnection;267;1;268;0
WireConnection;242;0;236;0
WireConnection;270;0;267;0
WireConnection;243;0;242;0
WireConnection;243;1;244;0
WireConnection;269;0;270;0
WireConnection;269;1;270;1
WireConnection;247;0;243;0
WireConnection;279;0;286;0
WireConnection;245;0;247;0
WireConnection;276;0;269;0
WireConnection;276;1;277;0
WireConnection;278;0;276;0
WireConnection;278;1;279;0
WireConnection;249;0;245;0
WireConnection;280;0;278;0
WireConnection;253;0;249;0
WireConnection;253;1;255;0
WireConnection;284;0;280;0
WireConnection;284;1;285;0
WireConnection;289;0;253;0
WireConnection;289;1;326;0
WireConnection;289;2;291;0
WireConnection;292;0;289;0
WireConnection;303;0;284;0
WireConnection;298;0;303;0
WireConnection;298;1;292;0
WireConnection;288;1;298;0
WireConnection;322;0;288;4
WireConnection;327;0;292;0
WireConnection;330;0;327;0
WireConnection;330;1;331;0
WireConnection;316;0;322;0
WireConnection;316;1;317;0
WireConnection;318;0;316;0
WireConnection;332;0;330;0
WireConnection;332;1;288;0
WireConnection;323;0;332;0
WireConnection;323;1;318;0
WireConnection;239;0;238;0
WireConnection;238;0;243;0
WireConnection;324;0;323;0
WireConnection;324;1;325;0
WireConnection;0;2;324;0
ASEEND*/
//CHKSM=1AA3A5841851AF8EB263AA1623863F2FA5B452A3