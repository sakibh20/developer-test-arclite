Shader "Custom/Water"
{
    Properties
    {
        _MainTex ("Water Texture", 2D) = "white" {}
        _ScrollSpeed ("Scroll Speed", Vector) = (0.1, 0.0, 0, 0) // X = U speed, Y = V speed
        _TintColor ("Tint Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _ScrollSpeed;
            float4 _TintColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Scroll UV over time
                float2 scrollUV = v.uv + _ScrollSpeed.xy * _Time.y;
                o.uv = TRANSFORM_TEX(scrollUV, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _TintColor;
                return col;
            }
            ENDCG
        }
    }
}

// Note: I used LLM here. I understand shader code, can edit some with research but can't myself write from scratch.
// This is one area I intend to improve next.