Shader "Custom/WaterEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Strength ("Strength", Range(0,1)) = 0.6
        _Tint ("Tint", Color) = (0.4,0.25,0.1,1)
    }

    SubShader
    {
        Tags { "Queue"="Overlay" }

        Pass
        {
            ZTest Always
            ZWrite Off
            Cull Off

            Stencil
            {
                Ref 1
                Comp Equal
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Strength;
            float4 _Tint;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                col.rgb = lerp(col.rgb, _Tint.rgb, _Strength);

                return col;
            }
            ENDCG
        }
    }
}
