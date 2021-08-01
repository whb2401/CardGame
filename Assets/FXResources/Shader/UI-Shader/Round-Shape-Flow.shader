// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "E3D/UI/Round-Shape-Flow"
{
	Properties
	{
		_RoundRadius("Round-Radius", Range( 0 , 1)) = 0.0997645
		_Round2("Round2", Range( 0.1 , 3)) = 0.33
		_Round("Round", Range( 0 , 12)) = 0.33
		_TailMax("Tail-Max", Range( 0 , 0.9)) = 0.3409177
		_Speed("Speed", Range( -1 , 1)) = 0
		[HDR]_MainColor("MainColor", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		Blend One One
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _RoundRadius;
		uniform float _Round;
		uniform float _Round2;
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
			float temp_output_249_0 = saturate( length( saturate( temp_output_243_0 ) ) );
			float temp_output_3_0_g11 = ( _RoundRadius - ( temp_output_249_0 / _Round ) );
			float temp_output_3_0_g10 = ( _RoundRadius - ( temp_output_249_0 / _Round2 ) );
			float2 break270 = ( i.uv_texcoord + -0.5 );
			float mulTime279 = _Time.y * _Speed;
			o.Emission = ( saturate( ( ( saturate( ( temp_output_3_0_g11 / fwidth( temp_output_3_0_g11 ) ) ) - saturate( ( temp_output_3_0_g10 / fwidth( temp_output_3_0_g10 ) ) ) ) * (0.0 + (frac( ( ( ( atan2( break270.x , break270.y ) / 6.28318548202515 ) + 0.2344178 ) + mulTime279 ) ) - _TailMax) * (1.0 - 0.0) / (1.0 - _TailMax)) ) ) * _MainColor ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17900
690;724;1062;456;-8155.636;81.78363;1.619171;True;False
Node;AmplifyShaderEditor.CommentaryNode;265;5915.874,-663.4014;Inherit;False;2552.492;1035.032;Comment;19;235;237;236;242;244;243;247;245;249;258;255;253;259;250;261;238;262;239;251;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;308;6086.711,428.1832;Inherit;False;2744.323;634.8956;Comment;16;307;284;285;280;278;279;274;276;286;275;269;277;270;267;268;266;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;237;6001.323,-146.0268;Inherit;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;False;0;-0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;235;5965.874,-454.0396;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;268;6197.631,755.0519;Inherit;False;Constant;_Float3;Float 3;3;0;Create;True;0;0;False;0;-0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;266;6136.711,478.1832;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;236;6258.406,-397.7437;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;267;6500.631,572.0519;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.AbsOpNode;242;6479.284,-291.2033;Inherit;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;244;6438.284,-27.20335;Inherit;False;Constant;_Float1;Float 1;1;0;Create;True;0;0;False;0;-0.2364886;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;270;6713.631,578.0519;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;243;6746.858,-212.8718;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ATan2OpNode;269;6993.109,577.5807;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TauNode;277;7093.379,857.7782;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;247;6981.436,-206.5942;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;286;7239.975,940.5881;Inherit;False;Property;_Speed;Speed;5;0;Create;True;0;0;False;0;0;-0.4;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;276;7304.415,576.0845;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LengthOpNode;245;7142.48,-218.1834;Inherit;True;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;275;7253.74,849.9806;Inherit;False;Constant;_Float5;Float 5;3;0;Create;True;0;0;False;0;0.2344178;0;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;274;7608.209,576.6287;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;279;7579.795,944.5238;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;255;7188.948,20.15644;Inherit;False;Property;_Round;Round;3;0;Create;True;0;0;False;0;0.33;0.32;0;12;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;249;7345.898,-232.5213;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;258;7194.258,161.2701;Inherit;False;Property;_Round2;Round2;2;0;Create;True;0;0;False;0;0.33;0.1;0.1;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;251;7200.784,256.6306;Inherit;False;Property;_RoundRadius;Round-Radius;1;0;Create;True;0;0;False;0;0.0997645;0.052;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;278;7865.903,613.584;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;253;7600.156,-262.4821;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;259;7633.741,85.75591;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;250;7923.439,-229.5705;Inherit;True;Step Antialiasing;-1;;11;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;285;7997.844,850.8286;Inherit;False;Property;_TailMax;Tail-Max;4;0;Create;True;0;0;False;0;0.3409177;0.798;0;0.9;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;280;8108.405,613.5797;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;261;8007.017,76.50986;Inherit;True;Step Antialiasing;-1;;10;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;262;8278.476,-66.00124;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;284;8373.478,639.0925;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;281;8867.094,76.69586;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;323;8933.707,310.8236;Inherit;False;Property;_MainColor;MainColor;6;1;[HDR];Create;True;0;0;False;0;0,0,0,0;2.408316,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;324;9105.083,145.7521;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;238;7049.439,-552.4139;Inherit;True;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;239;7331.862,-613.4014;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;307;8364.01,858.0146;Inherit;False;Constant;_Float6;Float 6;7;0;Create;True;0;0;False;0;0.7176471;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;322;9245.742,192.8237;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;9542.953,143.8433;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;E3D/UI/Round-Shape-Flow;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
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
WireConnection;276;0;269;0
WireConnection;276;1;277;0
WireConnection;245;0;247;0
WireConnection;274;0;276;0
WireConnection;274;1;275;0
WireConnection;279;0;286;0
WireConnection;249;0;245;0
WireConnection;278;0;274;0
WireConnection;278;1;279;0
WireConnection;253;0;249;0
WireConnection;253;1;255;0
WireConnection;259;0;249;0
WireConnection;259;1;258;0
WireConnection;250;1;253;0
WireConnection;250;2;251;0
WireConnection;280;0;278;0
WireConnection;261;1;259;0
WireConnection;261;2;251;0
WireConnection;262;0;250;0
WireConnection;262;1;261;0
WireConnection;284;0;280;0
WireConnection;284;1;285;0
WireConnection;281;0;262;0
WireConnection;281;1;284;0
WireConnection;324;0;281;0
WireConnection;238;0;243;0
WireConnection;239;0;238;0
WireConnection;322;0;324;0
WireConnection;322;1;323;0
WireConnection;0;2;322;0
ASEEND*/
//CHKSM=AB584D99B205BCCD14856263F3BF4F2B71BA4183