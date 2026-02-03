Shader "Custom/FrontWaveCutout"
{
    Properties
    {
        _Color ("Wave Color", Color) = (0.2, 0.5, 0.8, 0.6)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Geometry+1"
            "RenderType"="Transparent"
        }

        Pass
        {
            // WRITE DEPTH (important)
            ZWrite On
            ZTest LEqual

            // Alpha blending
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
