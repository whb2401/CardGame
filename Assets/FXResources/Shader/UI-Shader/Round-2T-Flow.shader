// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "E3D/UI/Round-2T-Flow"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		[HDR]_MainColor("MainColor", Color) = (1,1,1,0)
		[HDR]_EdgeColor("EdgeColor", Color) = (1,1,1,0)
		_RoundMin("RoundMin", Range( 0 , 0.1)) = 0.04
		_TailMax("Tail-Max", Range( 0 , 0.9)) = 0.3409177
		_Speed("Speed", Range( -2 , 2)) = 0
		_MaskMap("MaskMap", 2D) = "white" {}
		_NoiseTiling("NoiseTiling", Range( 0.01 , 10)) = 3
		_NoiseLevelMin("NoiseLevelMin", Range( -1 , 0)) = -0.6516864
		_NoiseLevelMax("NoiseLevelMax", Range( 0.01 , 1)) = 0.2606649
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha , One One
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma exclude_renderers vulkan xbox360 xboxone ps4 psp2 n3ds wiiu 
		#pragma surface surf Unlit keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _EdgeColor;
		uniform float4 _MainColor;
		uniform sampler2D _MainTex;
		uniform float _Speed;
		uniform float _TailMax;
		uniform float _RoundMin;
		uniform sampler2D _MaskMap;
		uniform float4 _MaskMap_ST;
		uniform float _NoiseTiling;
		uniform float _NoiseLevelMax;
		uniform float _NoiseLevelMin;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 break270 = ( i.uv_texcoord + -0.5 );
			float mulTime279 = _Time.y * _Speed;
			float temp_output_292_0 = saturate( (0.0 + (( saturate( length( saturate( ( abs( ( i.uv_texcoord + -0.5 ) ) + -0.2364886 ) ) ) ) / 5.0 ) - _RoundMin) * (1.0 - 0.0) / (0.0 - _RoundMin)) );
			float2 appendResult298 = (float2(saturate( (0.0 + (frac( ( ( ( atan2( break270.x , break270.y ) / 6.28318548202515 ) + 0.2344178 ) + mulTime279 ) ) - _TailMax) * (1.0 - 0.0) / (1.0 - _TailMax)) ) , temp_output_292_0));
			float4 tex2DNode288 = tex2D( _MainTex, appendResult298 );
			float4 temp_output_323_0 = ( ( 1.0 * tex2DNode288 ) * saturate( ( ( tex2DNode288.a + 0.0 ) / 0.4 ) ) );
			float2 uv_MaskMap = i.uv_texcoord * _MaskMap_ST.xy + _MaskMap_ST.zw;
			float4 smoothstepResult364 = smoothstep( float4( 0,0,0,0 ) , float4( 1,1,1,0 ) , saturate( ( temp_output_323_0 * ( 0.01 + tex2D( _MaskMap, uv_MaskMap ) ) ) ));
			float2 temp_cast_0 = (_NoiseTiling).xx;
			float2 uv_TexCoord352 = i.uv_texcoord * temp_cast_0;
			float2 panner351 = ( 1.1 * _Time.y * float2( 0.4,0.1 ) + uv_TexCoord352);
			float2 panner353 = ( 1.1 * _Time.y * float2( -0.3,-0.2 ) + uv_TexCoord352);
			float4 temp_cast_1 = (_NoiseLevelMax).xxxx;
			float4 temp_cast_2 = (_NoiseLevelMin).xxxx;
			float4 temp_output_366_0 = saturate( ( smoothstepResult364 + ( temp_output_323_0 * saturate( (temp_cast_2 + (( tex2D( _MaskMap, panner351 ) * tex2D( _MaskMap, panner353 ) ) - float4( 0,0,0,0 )) * (float4( 1,1,1,0 ) - temp_cast_2) / (temp_cast_1 - float4( 0,0,0,0 ))) ) ) ) );
			float4 lerpResult344 = lerp( _EdgeColor , _MainColor , temp_output_366_0.r);
			o.Emission = saturate( lerpResult344 ).rgb;
			float4 temp_cast_4 = (0.02).xxxx;
			float4 temp_cast_5 = (0.0).xxxx;
			float3 desaturateInitialColor339 = (float4( 0,0,0,0 ) + (temp_output_366_0 - temp_cast_4) * (float4( 1,1,1,0 ) - float4( 0,0,0,0 )) / (temp_cast_5 - temp_cast_4)).rgb;
			float desaturateDot339 = dot( desaturateInitialColor339, float3( 0.299, 0.587, 0.114 ));
			float3 desaturateVar339 = lerp( desaturateInitialColor339, desaturateDot339.xxx, 1.0 );
			o.Alpha = ( 1.0 - saturate( desaturateVar339 ) ).x;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17900
365;548;1062;456;-10495.88;312.5382;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;308;6086.711,-41.05429;Inherit;False;3118.863;627.4049;Comment;17;268;266;267;270;277;269;275;286;276;279;274;278;280;284;303;307;285;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;265;5915.874,-1132.639;Inherit;False;2126.001;1003.44;Comment;11;253;255;249;245;247;243;242;244;236;235;237;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;266;6136.711,8.945718;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;237;6001.323,-615.2643;Inherit;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;False;0;-0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;268;6197.631,285.8143;Inherit;False;Constant;_Float3;Float 3;3;0;Create;True;0;0;False;0;-0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;235;5965.874,-923.2772;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;267;6500.631,102.8144;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;236;6258.406,-866.9812;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;244;6374.658,-534.3715;Inherit;False;Constant;_Float1;Float 1;1;0;Create;True;0;0;False;0;-0.2364886;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;242;6479.284,-760.4408;Inherit;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.BreakToComponentsNode;270;6713.631,108.8144;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;243;6745.858,-692.1093;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ATan2OpNode;269;6993.109,108.3432;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TauNode;277;7093.379,388.5406;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;247;6977.436,-691.1272;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;275;7253.74,380.743;Inherit;False;Constant;_Float5;Float 5;3;0;Create;True;0;0;False;0;0.2344178;0;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;276;7304.415,106.847;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;286;7252.975,470.3505;Inherit;False;Property;_Speed;Speed;6;0;Create;True;0;0;False;0;0;-0.7;-2;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.LengthOpNode;245;7148.627,-691.1096;Inherit;True;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;274;7608.209,107.3912;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;279;7579.795,475.2863;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;278;7865.903,144.3465;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;320;8124.489,-704.7192;Inherit;False;1448.325;581.2026;Comment;4;292;289;291;326;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;255;7188.948,-449.0811;Inherit;False;Constant;_Round;Round;3;0;Create;True;0;0;False;0;5;5;0;12;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;249;7345.898,-693.152;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;291;8302.93,-386.4094;Inherit;False;Constant;_MainUVMax;MainUV-Max;6;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;280;8108.405,144.3422;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;326;8297.949,-468.7547;Inherit;False;Property;_RoundMin;RoundMin;4;0;Create;True;0;0;False;0;0.04;0.0278;0;0.1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;253;7754.509,-658.4034;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;285;7997.844,381.591;Inherit;False;Property;_TailMax;Tail-Max;5;0;Create;True;0;0;False;0;0.3409177;0.438;0;0.9;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;284;8373.478,169.855;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;289;8696.283,-523.1874;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;303;8866.499,167.9783;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;319;9675.877,-338.0257;Inherit;False;3013.628;788.9601;Comment;19;367;365;366;364;334;378;375;376;377;374;323;318;371;332;288;298;316;317;322;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SaturateNode;292;9161.092,-520.6041;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;298;9834.542,-196.6719;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;360;9404.219,479.3307;Inherit;False;2332.252;765.6941;NoiseMap;10;357;361;359;349;333;353;351;352;356;354;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;356;9475.778,847.1573;Inherit;False;Property;_NoiseTiling;NoiseTiling;8;0;Create;True;0;0;False;0;3;3.71;0.01;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;288;10102.91,-216.1373;Inherit;True;Property;_MainTex;MainTex;1;0;Create;True;0;0;False;0;-1;None;9bf7e1b724664aa4ab275c092903783d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;317;10517.94,95.33881;Inherit;False;Constant;_Float4;Float 4;8;0;Create;True;0;0;False;0;0.4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;352;9846.991,821.5597;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;3,3;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;322;10469.96,-143.3175;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;353;10193.05,941.7397;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.3,-0.2;False;1;FLOAT;1.1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;371;10241.07,-297.1472;Inherit;False;Constant;_Float11;Float 11;12;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;316;10736.03,14.1732;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;351;10203.71,673.3239;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.4,0.1;False;1;FLOAT;1.1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;374;10982.64,99.54801;Inherit;True;Property;_TextureSample1;Texture Sample 1;7;0;Create;True;0;0;False;0;-1;None;cd460ee4ac5c1e746b7a734cc7cc64dd;True;0;False;white;Auto;False;Instance;333;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;332;10488.31,-268.2492;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;377;11201.21,-50.89854;Inherit;False;Constant;_Float12;Float 12;12;0;Create;True;0;0;False;0;0.01;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;349;10505.6,913.7012;Inherit;True;Property;_TextureSample0;Texture Sample 0;7;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Instance;333;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;318;10992.52,14.19334;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;333;10496.33,645.1541;Inherit;True;Property;_MaskMap;MaskMap;7;0;Create;True;0;0;False;0;-1;None;e28dc97a9541e3642a48c0e3886688c5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;361;10843.92,888.5451;Inherit;False;Property;_NoiseLevelMin;NoiseLevelMin;9;0;Create;True;0;0;False;0;-0.6516864;0.1;-1;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;376;11406.05,-27.9065;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;359;10840.55,815.3168;Inherit;False;Property;_NoiseLevelMax;NoiseLevelMax;10;0;Create;True;0;0;False;0;0.2606649;0.2606649;0.01;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;354;10860.08,564.8581;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;323;11291.42,-292.1904;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;357;11210.89,671.8787;Inherit;True;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;375;11655.46,-288.4002;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;378;11889.6,-287.1085;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;367;11747.81,192.6532;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;334;12066.69,169.63;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;364;12074.63,-289.2287;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;379;12414.19,492.6722;Inherit;False;1519.22;323.9142;Comment;6;336;337;335;339;340;341;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;365;12315.7,5.962646;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;336;12464.19,544.7826;Inherit;False;Constant;_RemapMin;RemapMin;8;0;Create;True;0;0;False;0;0.02;0.02;0.01;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;366;12551.63,7.272539;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;337;12468.63,641.9942;Inherit;False;Constant;_Float8;Float 8;8;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;380;12886.39,-409.7042;Inherit;False;984.6484;587.9354;Comment;5;346;345;325;344;347;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TFHCRemapNode;335;12897.76,542.6722;Inherit;True;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;3;COLOR;0,0,0,0;False;4;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;325;12936.39,-359.7042;Inherit;False;Property;_EdgeColor;EdgeColor;3;1;[HDR];Create;True;0;0;False;0;1,1,1,0;0,0.533587,1.059864,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;347;12951.64,-0.7688293;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.ColorNode;345;12945.01,-172.233;Inherit;False;Property;_MainColor;MainColor;2;1;[HDR];Create;True;0;0;False;0;1,1,1,0;1.258125,2.957659,3.111002,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DesaturateOpNode;339;13254.47,563.5864;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;344;13369.21,-136.3851;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;340;13535.74,548.3661;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode;341;13735.41,543.8608;Inherit;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;307;8364.01,388.777;Inherit;False;Constant;_Float6;Float 6;7;0;Create;True;0;0;False;0;0.7176471;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;330;10065.69,-668.8425;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;346;13696.04,-129.4039;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FWidthOpNode;327;9770.75,-763.2995;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;331;9782.555,-508.9349;Inherit;False;Constant;_Float2;Float 2;6;0;Create;True;0;0;False;0;22;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;14267.54,-39.67608;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;E3D/UI/Round-2T-Flow;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;7;d3d9;d3d11_9x;d3d11;glcore;gles;gles3;metal;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;244;10;False;-1;4;1;False;-1;1;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;267;0;266;0
WireConnection;267;1;268;0
WireConnection;236;0;235;0
WireConnection;236;1;237;0
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
WireConnection;278;0;274;0
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
WireConnection;303;0;284;0
WireConnection;292;0;289;0
WireConnection;298;0;303;0
WireConnection;298;1;292;0
WireConnection;288;1;298;0
WireConnection;352;0;356;0
WireConnection;322;0;288;4
WireConnection;353;0;352;0
WireConnection;316;0;322;0
WireConnection;316;1;317;0
WireConnection;351;0;352;0
WireConnection;332;0;371;0
WireConnection;332;1;288;0
WireConnection;349;1;353;0
WireConnection;318;0;316;0
WireConnection;333;1;351;0
WireConnection;376;0;377;0
WireConnection;376;1;374;0
WireConnection;354;0;333;0
WireConnection;354;1;349;0
WireConnection;323;0;332;0
WireConnection;323;1;318;0
WireConnection;357;0;354;0
WireConnection;357;2;359;0
WireConnection;357;3;361;0
WireConnection;375;0;323;0
WireConnection;375;1;376;0
WireConnection;378;0;375;0
WireConnection;367;0;357;0
WireConnection;334;0;323;0
WireConnection;334;1;367;0
WireConnection;364;0;378;0
WireConnection;365;0;364;0
WireConnection;365;1;334;0
WireConnection;366;0;365;0
WireConnection;335;0;366;0
WireConnection;335;1;336;0
WireConnection;335;2;337;0
WireConnection;347;0;366;0
WireConnection;339;0;335;0
WireConnection;344;0;325;0
WireConnection;344;1;345;0
WireConnection;344;2;347;0
WireConnection;340;0;339;0
WireConnection;341;0;340;0
WireConnection;330;0;327;0
WireConnection;330;1;331;0
WireConnection;346;0;344;0
WireConnection;327;0;292;0
WireConnection;0;2;346;0
WireConnection;0;9;341;0
ASEEND*/
//CHKSM=BEF7625D8109AE949973D2CF175966EBBC9BD831