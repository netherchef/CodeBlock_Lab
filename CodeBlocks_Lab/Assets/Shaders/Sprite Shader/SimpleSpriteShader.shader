Shader "Unlit/SimpleSpriteShader"
{
    Properties
    {
        _Color("Replacement Color", Color) = (1,1,1,1)
    }

    SubShader
    {   
        Blend SrcAlpha OneMinusSrcAlpha
    
        Pass
        {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos  : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _Color;
            
            v2f vert(appdata IN)
            {
                v2f OUT;

                OUT.pos = UnityObjectToClipPos(IN.vertex);
                OUT.uv = IN.uv;
                
                return OUT;
            }
            
            fixed4 frag(v2f IN) : COLOR
            {
                float4 col = tex2D (_MainTex, IN.uv);
                return float4 (_Color.r, _Color.g, _Color.b, col.a);
            }

        ENDCG
        }
    }
}