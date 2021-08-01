// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "E3D/UI/Shader-OffsetCilp"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_ScaleX("ScaleX", Range( 0 , 2)) = 1
		_Color("Color", Color) = (0.1838235,1,0.2232252,0)
		_StepAmount("Step-Amount", Range( 0 , 12)) = 4
		_Speed("Speed", Range( 0 , 2)) = 1
		_Max("Max", Range( 1 , 10)) = -2
		_Min("Min", Range( -1 , 1)) = -2
		_ClipStep("ClipStep", Range( -0.5 , 1)) = 0.8
		[Toggle(_ONE_ON)] _One("One", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma shader_feature _ONE_ON
		#pragma surface surf Unlit keepalpha noshadow noambient novertexlights 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color;
		uniform sampler2D _MainTex;
		uniform float _StepAmount;
		uniform float _ScaleX;
		uniform float _Speed;
		uniform float _Min;
		uniform float _Max;
		uniform float _ClipStep;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float StepAmount36 = floor( _StepAmount );
			float2 appendResult39 = (float2(StepAmount36 , 1.0));
			float2 uv_TexCoord34 = i.uv_texcoord * appendResult39;
			float4 tex2DNode1 = tex2D( _MainTex, uv_TexCoord34 );
			float4 Texture93 = tex2DNode1;
			o.Emission = ( _Color * Texture93 ).rgb;
			float temp_output_72_0 = saturate( (_Min + (( 1.0 - saturate( frac( (( ( floor( ( ( i.uv_texcoord * _ScaleX ) * StepAmount36 ) ) / StepAmount36 ).x + 0.0 )*1.0 + ( _Time.y * _Speed )) ) ) ) - 0.3) * (_Max - _Min) / (4.0 - 0.3)) );
			#ifdef _ONE_ON
				float staticSwitch92 = step( _ClipStep , temp_output_72_0 );
			#else
				float staticSwitch92 = temp_output_72_0;
			#endif
			o.Alpha = ( staticSwitch92 * saturate( ( tex2DNode1 * 4.0 ) ) ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17900
592;809;1062;456;-1832.511;-531.9803;1.03864;True;False
Node;AmplifyShaderEditor.CommentaryNode;95;-2919.109,-360.719;Inherit;False;2049.177;544.7556;Gird;11;2;5;6;83;84;85;8;9;31;41;36;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-2869.109,65.44368;Inherit;False;Property;_StepAmount;Step-Amount;4;0;Create;True;0;0;False;0;4;6.04;0;12;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-2543.965,-181.0195;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;5;-2586.246,-310.719;Inherit;False;Property;_ScaleX;ScaleX;2;0;Create;True;0;0;False;0;1;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.FloorOpNode;41;-2539.68,72.49107;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;36;-2356.268,69.03658;Inherit;False;StepAmount;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-2186.699,-180.0479;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;83;-1972.739,-180.5879;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FloorOpNode;84;-1755.223,-178.1913;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;97;-831.5524,176.4632;Inherit;False;2600.163;506.6673;Comment;14;92;86;91;72;53;16;73;71;15;65;66;70;69;67;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;85;-1602.302,-101.2152;Inherit;True;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;69;-682.6424,318.5369;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;67;-784.0724,395.3531;Inherit;False;Property;_Speed;Speed;5;0;Create;True;0;0;False;0;1;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;8;-1380.484,-102.0807;Inherit;True;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;-433.2415,321.0077;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;9;-1104.932,-108.8008;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;66;-252.2614,228.5275;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;65;-19.74662,227.159;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;96;-51.28238,763.8515;Inherit;False;1818.984;510.0469;Comment;9;40;37;39;34;1;60;58;93;61;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SaturateNode;15;189.4436,227.4699;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;37;-1.282353,935.3189;Inherit;False;36;StepAmount;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;40;49.81105,1040.523;Inherit;False;Constant;_Float1;Float 1;3;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;16;374.1434,226.4632;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;71;273.802,446.9547;Inherit;False;Property;_Min;Min;7;0;Create;True;0;0;False;0;-2;-0.45;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;39;282.1152,965.0872;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;73;273.3227,532.5503;Inherit;False;Property;_Max;Max;6;0;Create;True;0;0;False;0;-2;10;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;34;570.7855,938.0114;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;53;642.4866,307.6053;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0.3;False;2;FLOAT;4;False;3;FLOAT;0;False;4;FLOAT;6;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;91;904.3958,548.2198;Inherit;False;Property;_ClipStep;ClipStep;8;0;Create;True;0;0;False;0;0.8;1;-0.5;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;72;1009.799,310.5719;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;907.6765,910.2667;Inherit;True;Property;_MainTex;MainTex;1;0;Create;True;0;0;False;0;-1;None;3f619acf06cde384d8476a342cdab893;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;60;981.0156,1130.713;Inherit;False;Constant;_Float2;Float 2;4;0;Create;True;0;0;False;0;4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;86;1296.618,436.726;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;93;1292.055,813.8515;Inherit;False;Texture;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;1298.76,1020.899;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;94;1912.792,630.2744;Inherit;False;93;Texture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;44;1873.111,442.3521;Inherit;False;Property;_Color;Color;3;0;Create;True;0;0;False;0;0.1838235,1,0.2232252,0;1,0.8258621,0.2573529,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;61;1569.701,1020.138;Inherit;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;92;1557.608,308.4733;Inherit;False;Property;_One;One;9;0;Create;True;0;0;False;0;0;0;1;True;;Toggle;2;Key0;Key1;Create;False;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;2184.524,560.0946;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;74;1887.152,768.7878;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2498.017,579.9073;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;E3D/UI/Shader-OffsetCilp;False;False;False;False;True;True;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;41;0;31;0
WireConnection;36;0;41;0
WireConnection;6;0;2;0
WireConnection;6;1;5;0
WireConnection;83;0;6;0
WireConnection;83;1;36;0
WireConnection;84;0;83;0
WireConnection;85;0;84;0
WireConnection;85;1;36;0
WireConnection;8;0;85;0
WireConnection;70;0;69;0
WireConnection;70;1;67;0
WireConnection;9;0;8;0
WireConnection;66;0;9;0
WireConnection;66;2;70;0
WireConnection;65;0;66;0
WireConnection;15;0;65;0
WireConnection;16;0;15;0
WireConnection;39;0;37;0
WireConnection;39;1;40;0
WireConnection;34;0;39;0
WireConnection;53;0;16;0
WireConnection;53;3;71;0
WireConnection;53;4;73;0
WireConnection;72;0;53;0
WireConnection;1;1;34;0
WireConnection;86;0;91;0
WireConnection;86;1;72;0
WireConnection;93;0;1;0
WireConnection;58;0;1;0
WireConnection;58;1;60;0
WireConnection;61;0;58;0
WireConnection;92;1;72;0
WireConnection;92;0;86;0
WireConnection;42;0;44;0
WireConnection;42;1;94;0
WireConnection;74;0;92;0
WireConnection;74;1;61;0
WireConnection;0;2;42;0
WireConnection;0;9;74;0
ASEEND*/
//CHKSM=D05DA34C12936EA99BB0B331DAABAC99E64EE573