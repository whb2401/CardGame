Shader "bbp/bb_beAtk"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _IsWhite ("IsWhite", Float) = 1
    }
    
    SubShader
    {
        Tags     
        {      
                "Queue"="Transparent"      
                "IgnoreProjector"="True"      
                "RenderType"="Transparent"      
                "PreviewType"="Plane"     
                "CanUseSpriteAtlas"="True"     
        }
                
        // 源rgba*源a + 背景rgba*(1-源A值)   
        Blend SrcAlpha OneMinusSrcAlpha     
        Pass     
        {     
                CGPROGRAM     
                #pragma vertex vert     
                #pragma fragment frag     
                #include "UnityCG.cginc"     
                        
                struct appdata_t     
                {     
                    float4 vertex   : POSITION;     
                    float4 color    : COLOR;     
                    float2 texcoord : TEXCOORD0;     
                };     
        
                struct v2f     
                {     
                    float4 vertex   : SV_POSITION;     
                    fixed4 color    : COLOR;     
                    half2 texcoord  : TEXCOORD0;     
                };     
                
                sampler2D _MainTex; 
                float _IsWhite;  
                float4 _Color; 

                v2f vert(appdata_t IN)     
                {     
                    v2f OUT;     
                    OUT.vertex = UnityObjectToClipPos(IN.vertex);     
                    OUT.texcoord = IN.texcoord;     
                    #ifdef UNITY_HALF_TEXEL_OFFSET     
                    OUT.vertex.xy -= (_ScreenParams.zw-1.0);     
                    #endif     
                    OUT.color = IN.color;     
                    return OUT;  
                }  
                
                fixed4 frag(v2f IN) : SV_Target     
                {     
                    half4 c = tex2D(_MainTex, IN.texcoord); 
                    if (_IsWhite >= 1) {
                        _Color.a *= c.a;
                        return _Color;
                    }
                    return c;
                }                       
                ENDCG     
        }
    }

    FallBack "Unlit/Transparent" 
}
