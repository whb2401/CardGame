// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "E3D/UI/Shader-Radious"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		[HDR]_MainColor("MainColor", Color) = (0.5661765,0.8563896,1,0)
		[HDR]_BackColor("BackColor", Color) = (0.5661765,0.8563896,1,0)
		_StepAmount("StepAmount", Range( 0 , 12)) = 12
		_TailMax("Tail-Max", Range( 0 , 3)) = 3
		_TailMin("Tail-Min", Range( -1 , 1)) = 1
		_Scale("Scale", Range( 0 , 5)) = 3
		_Speed("Speed", Range( -2 , 0)) = -0.2
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
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _BackColor;
		uniform float4 _MainColor;
		uniform sampler2D _MainTex;
		uniform float _Scale;
		uniform float _Speed;
		uniform float _TailMin;
		uniform float _TailMax;
		uniform float _StepAmount;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 temp_cast_0 = (_Scale).xx;
			float2 uv_TexCoord209 = i.uv_texcoord * temp_cast_0;
			float2 break101 = ( i.uv_texcoord + float2( -0.5,-0.5 ) );
			float temp_output_105_0 = ( atan2( break101.x , break101.y ) / 6.28318548202515 );
			float mulTime134 = _Time.y * _Speed;
			float Speed154 = mulTime134;
			float StepAmount169 = floor( _StepAmount );
			float temp_output_184_0 = saturate( ( saturate( (-0.08 + (frac( ( temp_output_105_0 + Speed154 ) ) - _TailMin) * (1.0 - -0.08) / (_TailMax - _TailMin)) ) + step( ( floor( ( frac( ( temp_output_105_0 + ( ( floor( ( Speed154 * StepAmount169 ) ) / StepAmount169 ) + -0.9098657 + -0.007058823 ) ) ) * StepAmount169 ) ) / StepAmount169 ) , 0.01 ) ) );
			float2 lerpResult205 = lerp( i.uv_texcoord , ( uv_TexCoord209 + ( ( _Scale + -1.0 ) * -0.5 ) ) , temp_output_184_0);
			float temp_output_3_0_g1 = ( ( tex2D( _MainTex, lerpResult205 ) * temp_output_184_0 ).r - 0.09539948 );
			float4 lerpResult236 = lerp( _BackColor , _MainColor , saturate( ( temp_output_3_0_g1 / fwidth( temp_output_3_0_g1 ) ) ));
			o.Emission = lerpResult236.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17900
473;693;1062;456;-743.9727;-721.0663;2.67551;True;False
Node;AmplifyShaderEditor.CommentaryNode;197;1391.186,372.0094;Inherit;False;2525.359;598.4568;Polar;16;163;113;115;116;188;187;105;153;154;106;134;102;100;104;103;101;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;196;1406.747,1042.549;Inherit;False;2925.073;531.923;Comment;18;175;182;176;181;179;174;180;164;190;189;173;170;167;169;166;172;171;234;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;153;1977.444,761.0231;Inherit;False;Property;_Speed;Speed;9;0;Create;True;0;0;False;0;-0.2;-0.8;-2;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;171;1542.085,1335.276;Inherit;False;Property;_StepAmount;StepAmount;4;0;Create;True;0;0;False;0;12;12;0;12;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;134;2298.202,763.9828;Inherit;False;1;0;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FloorOpNode;172;1833.738,1340.898;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;154;2516.282,760.0938;Inherit;False;Speed;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;169;1995.606,1335.153;Inherit;False;StepAmount;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;104;1441.186,572.5932;Inherit;False;Constant;_Vector0;Vector 0;10;0;Create;True;0;0;False;0;-0.5,-0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;100;1447.774,422.0094;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;166;2005.239,1215.857;Inherit;False;154;Speed;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;103;1717.748,486.0277;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;167;2260.377,1220.724;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FloorOpNode;170;2449.112,1219.518;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;101;1948.881,457.4327;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleDivideOpNode;173;2623.401,1219.277;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ATan2OpNode;102;2238.724,457.6061;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TauNode;106;2335.852,678.7347;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;189;2486.008,1320.747;Inherit;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;False;0;-0.9098657;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;234;2486.469,1397.568;Inherit;False;Constant;_Float2;Float 2;10;0;Create;True;0;0;False;0;-0.007058823;-0.007058823;-0.1;0.1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;190;2791.551,1260.183;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;105;2527.129,478.3418;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;164;2968.769,1092.787;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;180;3079.573,1366.088;Inherit;False;169;StepAmount;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;174;3204.126,1092.549;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;179;3407.096,1242.859;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;163;2843.265,599.4528;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;188;3032.282,800.4716;Inherit;False;Property;_TailMin;Tail-Min;6;0;Create;True;0;0;False;0;1;-0.42;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;113;3139.613,580.8546;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;224;3284.371,-470.9048;Inherit;False;1447.774;794.2283;Scale;9;210;211;198;212;222;209;223;221;214;;1,1,1,1;0;0
Node;AmplifyShaderEditor.FloorOpNode;181;3578.786,1244.012;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;187;3030.075,881.4661;Inherit;False;Property;_TailMax;Tail-Max;5;0;Create;True;0;0;False;0;3;2.57;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;210;3319.153,-142.9913;Inherit;False;Property;_Scale;Scale;7;0;Create;True;0;0;False;0;3;1;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;115;3382.631,577.2465;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;3.46;False;3;FLOAT;-0.08;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;182;3735.131,1343.506;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;214;3374.426,107.7924;Inherit;False;Constant;_Float5;Float 5;7;0;Create;True;0;0;False;0;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;176;3714.064,1151.402;Inherit;False;Constant;_Float1;Float 1;3;0;Create;True;0;0;False;0;0.01;0.04;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;175;4023.982,1134.136;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;223;3374.308,181.3235;Inherit;False;Constant;_Float7;Float 7;8;0;Create;True;0;0;False;0;-0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;116;3718.545,576.5654;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;195;4372.901,441.091;Inherit;False;676.416;552.4296;Shape Step;2;184;177;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;221;3550.308,57.32351;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;209;3865.712,-165.3647;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;222;3712.308,57.32351;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;177;4422.901,716.8686;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;212;4163.961,35.06533;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SaturateNode;184;4748.647,716.7389;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;198;4159.179,-420.9048;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;233;5230.705,-13.83098;Inherit;False;1185.376;708.1282;Comment;8;112;229;193;183;232;194;235;236;;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;205;5035.971,154.8897;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;112;5280.705,125.8833;Inherit;True;Property;_MainTex;MainTex;1;0;Create;True;0;0;False;0;-1;1697870abd55dce478d6ce5df4d82363;1697870abd55dce478d6ce5df4d82363;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;229;5308.04,612.2972;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;183;5646.814,130.3913;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;193;5559.054,36.16902;Inherit;False;Constant;_Float4;Float 4;6;0;Create;True;0;0;False;0;0.09539948;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;232;5916.688,503.175;Inherit;False;Property;_MainColor;MainColor;2;1;[HDR];Create;True;0;0;False;0;0.5661765,0.8563896,1,0;0.5661765,0.8563896,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;235;5925.025,327.5995;Inherit;False;Property;_BackColor;BackColor;3;1;[HDR];Create;True;0;0;False;0;0.5661765,0.8563896,1,0;0.23,0.23,0.23,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;194;5936.198,107.9908;Inherit;True;Step Antialiasing;-1;;1;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;236;6279.268,327.6574;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;211;4142.858,-126.1711;Inherit;False;Property;_Float3;Float 3;8;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;6581.184,187.6303;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;E3D/UI/Shader-Radious;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;134;0;153;0
WireConnection;172;0;171;0
WireConnection;154;0;134;0
WireConnection;169;0;172;0
WireConnection;103;0;100;0
WireConnection;103;1;104;0
WireConnection;167;0;166;0
WireConnection;167;1;169;0
WireConnection;170;0;167;0
WireConnection;101;0;103;0
WireConnection;173;0;170;0
WireConnection;173;1;169;0
WireConnection;102;0;101;0
WireConnection;102;1;101;1
WireConnection;190;0;173;0
WireConnection;190;1;189;0
WireConnection;190;2;234;0
WireConnection;105;0;102;0
WireConnection;105;1;106;0
WireConnection;164;0;105;0
WireConnection;164;1;190;0
WireConnection;174;0;164;0
WireConnection;179;0;174;0
WireConnection;179;1;180;0
WireConnection;163;0;105;0
WireConnection;163;1;154;0
WireConnection;113;0;163;0
WireConnection;181;0;179;0
WireConnection;115;0;113;0
WireConnection;115;1;188;0
WireConnection;115;2;187;0
WireConnection;182;0;181;0
WireConnection;182;1;180;0
WireConnection;175;0;182;0
WireConnection;175;1;176;0
WireConnection;116;0;115;0
WireConnection;221;0;210;0
WireConnection;221;1;214;0
WireConnection;209;0;210;0
WireConnection;222;0;221;0
WireConnection;222;1;223;0
WireConnection;177;0;116;0
WireConnection;177;1;175;0
WireConnection;212;0;209;0
WireConnection;212;1;222;0
WireConnection;184;0;177;0
WireConnection;205;0;198;0
WireConnection;205;1;212;0
WireConnection;205;2;184;0
WireConnection;112;1;205;0
WireConnection;229;0;184;0
WireConnection;183;0;112;0
WireConnection;183;1;229;0
WireConnection;194;1;193;0
WireConnection;194;2;183;0
WireConnection;236;0;235;0
WireConnection;236;1;232;0
WireConnection;236;2;194;0
WireConnection;0;2;236;0
ASEEND*/
//CHKSM=820DCF5200AB9B4EECAE5E238B0B56CE17BDC8BB